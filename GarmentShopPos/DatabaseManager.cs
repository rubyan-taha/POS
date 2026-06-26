using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace GarmentShopPos
{
    public static class DatabaseManager
    {
        private static string connectionString = "";
        private static string dbName = "garment_shop_pos";

        static DatabaseManager()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfiguration config = builder.Build();
                var connStr = config.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(connStr))
                {
                    connectionString = connStr;
                    var cb = new SqlConnectionStringBuilder(connectionString);
                    if (!string.IsNullOrEmpty(cb.InitialCatalog))
                    {
                        dbName = cb.InitialCatalog;
                    }
                }
                else
                {
                    connectionString = $"Server=localhost\\SQLEXPRESS;Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
                }
            }
            catch
            {
                connectionString = $"Server=localhost\\SQLEXPRESS;Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
            }
        }

        public static string GetConnectionString()
        {
            return connectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            // 1. Create database if not exists
            var builder = new SqlConnectionStringBuilder(connectionString);
            string originalDb = builder.InitialCatalog;
            
            // Connect to server (master database) to create the database if not exists
            builder.InitialCatalog = "master";
            string masterConnectionString = builder.ConnectionString;

            using (var masterConn = new SqlConnection(masterConnectionString))
            {
                masterConn.Open();
                string checkDbQuery = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{originalDb}') CREATE DATABASE [{originalDb}];";
                using (var cmd = new SqlCommand(checkDbQuery, masterConn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // Restore original database
            builder.InitialCatalog = originalDb;

            // 2. Connect to the database and create tables
            using (var conn = GetConnection())
            {
                conn.Open();

                // Create Users table
                string createUsersTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='users' AND xtype='U')
                    CREATE TABLE users (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        username NVARCHAR(50) NOT NULL UNIQUE,
                        password_hash NVARCHAR(256) NOT NULL,
                        full_name NVARCHAR(100) NOT NULL,
                        role NVARCHAR(20) NOT NULL,
                        assigned_shop NVARCHAR(20) NOT NULL DEFAULT 'Both'
                    );";
                ExecuteQuery(createUsersTable, conn);

                // Create Products table
                string createProductsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='products' AND xtype='U')
                    CREATE TABLE products (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        barcode NVARCHAR(100) NOT NULL UNIQUE,
                        section NVARCHAR(20) NOT NULL, /* Gents, Ladies */
                        fabric_type NVARCHAR(100) NOT NULL, /* Lawn, Suiting, Georgette, etc. */
                        fabric_material NVARCHAR(100) NULL, /* Cotton, Linen, etc. (for Gents) */
                        color NVARCHAR(50) NULL, /* Blue, Khaki, etc. (for Gents) */
                        is_printed TINYINT NOT NULL DEFAULT 0,
                        print_type NVARCHAR(50) NULL, /* Digital, Block, Screen (for Ladies) */
                        is_embroidered TINYINT NOT NULL DEFAULT 0,
                        embroidery_type NVARCHAR(50) NULL, /* Hand work, Machine, Chikan (for Ladies) */
                        embroidery_extra_charge DECIMAL(10,2) NOT NULL DEFAULT 0.00,
                        suit_type NVARCHAR(50) NULL, /* 3-Piece, 2-Piece, Single (for Ladies) */
                        wholesale_price DECIMAL(10,2) NOT NULL,
                        retail_price DECIMAL(10,2) NOT NULL, /* Price per yard/meter */
                        current_stock DECIMAL(10,2) NOT NULL DEFAULT 0.00, /* yards/meters */
                        reorder_point DECIMAL(10,2) NOT NULL DEFAULT 5.00,
                        is_deleted TINYINT NOT NULL DEFAULT 0
                    );";
                ExecuteQuery(createProductsTable, conn);

                // Run ALTER TABLE if barcode column doesn't exist for users running an existing DB
                string alterProductsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[products]') AND name = N'barcode')
                    BEGIN
                        ALTER TABLE products ADD barcode NVARCHAR(100) NULL;
                        ALTER TABLE products ADD CONSTRAINT UQ_Products_Barcode UNIQUE(barcode);
                    END;";
                ExecuteQuery(alterProductsTable, conn);

                // Migrate and enforce NOT NULL constraint on barcode
                string migrateBarcodeQuery = @"
                    -- Update existing products with NULL or empty barcodes to a simple sample barcode
                    UPDATE products SET barcode = 'BAR-' + CAST(id AS NVARCHAR(10)) WHERE barcode IS NULL OR barcode = '';

                    -- Alter barcode column to NOT NULL if it is currently nullable
                    IF EXISTS (
                        SELECT 1 FROM sys.columns 
                        WHERE object_id = OBJECT_ID(N'[products]') 
                        AND name = N'barcode' 
                        AND is_nullable = 1
                    )
                    BEGIN
                        ALTER TABLE products ALTER COLUMN barcode NVARCHAR(100) NOT NULL;
                    END;";
                ExecuteQuery(migrateBarcodeQuery, conn);

                // Create Customers table
                string createCustomersTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='customers' AND xtype='U')
                    BEGIN
                        CREATE TABLE customers (
                            id INT IDENTITY(1,1) PRIMARY KEY,
                            name NVARCHAR(100) NULL,
                            phone_number NVARCHAR(20) NOT NULL UNIQUE,
                            credit_balance DECIMAL(10,2) NOT NULL DEFAULT 0.00, /* Khata balance */
                            created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                        );
                        CREATE INDEX idx_phone ON customers(phone_number);
                    END;";
                ExecuteQuery(createCustomersTable, conn);

                // Create Orders table
                string createOrdersTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='orders' AND xtype='U')
                    CREATE TABLE orders (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        receipt_number NVARCHAR(50) NOT NULL UNIQUE,
                        customer_id INT NULL,
                        salesman_id INT NOT NULL,
                        order_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        subtotal DECIMAL(10,2) NOT NULL,
                        discount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
                        total_amount DECIMAL(10,2) NOT NULL,
                        payment_method NVARCHAR(20) NOT NULL, /* Cash, Card, Mobile, Credit */
                        amount_paid DECIMAL(10,2) NOT NULL,
                        change_returned DECIMAL(10,2) NOT NULL DEFAULT 0.00,
                        is_refunded TINYINT NOT NULL DEFAULT 0,
                        refund_date DATETIME NULL,
                        FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE SET NULL,
                        FOREIGN KEY (salesman_id) REFERENCES users(id)
                    );";
                ExecuteQuery(createOrdersTable, conn);

                // Create OrderItems table
                string createOrderItemsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='order_items' AND xtype='U')
                    CREATE TABLE order_items (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        order_id INT NOT NULL,
                        product_id INT NOT NULL,
                        quantity DECIMAL(10,2) NOT NULL, /* yards/meters */
                        unit_price DECIMAL(10,2) NOT NULL, /* price per yard/meter */
                        is_printed TINYINT NOT NULL DEFAULT 0,
                        print_type NVARCHAR(50) NULL,
                        is_embroidered TINYINT NOT NULL DEFAULT 0,
                        embroidery_type NVARCHAR(50) NULL,
                        embroidery_extra_charge DECIMAL(10,2) NOT NULL DEFAULT 0.00,
                        total_item_amount DECIMAL(10,2) NOT NULL,
                        FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
                        FOREIGN KEY (product_id) REFERENCES products(id)
                    );";
                ExecuteQuery(createOrderItemsTable, conn);

                // Create WastageLogs table
                string createWastageLogsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='wastage_logs' AND xtype='U')
                    CREATE TABLE wastage_logs (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        product_id INT NOT NULL,
                        quantity DECIMAL(10,2) NOT NULL,
                        reason NVARCHAR(255) NOT NULL,
                        logged_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        logged_by INT NOT NULL,
                        FOREIGN KEY (product_id) REFERENCES products(id),
                        FOREIGN KEY (logged_by) REFERENCES users(id)
                    );";
                ExecuteQuery(createWastageLogsTable, conn);

                // Create PurchaseRecords table
                string createPurchaseRecordsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='purchase_records' AND xtype='U')
                    CREATE TABLE purchase_records (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        product_id INT NOT NULL,
                        quantity DECIMAL(10,2) NOT NULL,
                        supplier_name NVARCHAR(100) NOT NULL,
                        purchase_price DECIMAL(10,2) NOT NULL,
                        purchase_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (product_id) REFERENCES products(id)
                    );";
                ExecuteQuery(createPurchaseRecordsTable, conn);

                // Create UserActivityLogs table
                string createUserActivityLogsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='user_activity_logs' AND xtype='U')
                    CREATE TABLE user_activity_logs (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        user_id INT NOT NULL,
                        activity_type NVARCHAR(50) NOT NULL, /* Login, Logout */
                        shop_mode NVARCHAR(50) NOT NULL, /* Gents, Ladies */
                        timestamp DATETIME NOT NULL,
                        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
                    );";
                ExecuteQuery(createUserActivityLogsTable, conn);

                // Migrate legacy roles in existing databases
                string alterUsersTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[users]') AND name = N'assigned_shop')
                    BEGIN
                        ALTER TABLE users ADD assigned_shop NVARCHAR(20) NOT NULL DEFAULT 'Both';
                    END;";
                ExecuteQuery(alterUsersTable, conn);

                string migrateRoles = @"
                    UPDATE users 
                    SET role = 'Employee' 
                    WHERE role IN ('Cashier', 'Manager', 'Storekeeper') OR role = 'employee';
                    UPDATE users 
                    SET role = 'SuperAdmin', assigned_shop = 'Both' 
                    WHERE username = 'admin';";
                ExecuteQuery(migrateRoles, conn);

                // Create Settings table
                string createSettingsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='settings' AND xtype='U')
                    CREATE TABLE settings (
                        setting_key VARCHAR(100) PRIMARY KEY,
                        setting_value NVARCHAR(500) NOT NULL
                    );";
                ExecuteQuery(createSettingsTable, conn);

                // Seed Default Settings if empty
                SeedDefaultSettings(conn);

                // Seed Default Admin if users empty
                SeedDefaultAdmin(conn);
                
                // Seed Sample Products if products empty
                SeedSampleProducts(conn);
            }
        }

        private static void ExecuteQuery(string query, SqlConnection conn)
        {
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static void LogActivity(int userId, string activityType, string shopMode)
        {
            try
            {
                // Convert UTC to local shop timezone
                DateTime localDate = DateTime.UtcNow;
                try
                {
                    var tz = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.ShopTimeZone);
                    localDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                }
                catch { localDate = DateTime.Now; }

                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO user_activity_logs (user_id, activity_type, shop_mode, timestamp) VALUES (@userId, @type, @shopMode, @timestamp)";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@type", activityType);
                        cmd.Parameters.AddWithValue("@shopMode", shopMode);
                        cmd.Parameters.AddWithValue("@timestamp", localDate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { /* fail silently */ }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static void SeedDefaultAdmin(SqlConnection conn)
        {
            using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM users", conn))
            {
                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                {
                    string adminPassHash = HashPassword("admin");
                    string employeePassHash = HashPassword("employee");

                    using (var insertCmd = new SqlCommand(@"
                        INSERT INTO users (username, password_hash, full_name, role, assigned_shop) VALUES 
                        ('admin', @adminHash, 'Store Owner Admin', 'SuperAdmin', 'Both'),
                        ('employee', @employeeHash, 'Zahid Ali', 'Employee', 'Both');", conn))
                    {
                        insertCmd.Parameters.AddWithValue("@adminHash", adminPassHash);
                        insertCmd.Parameters.AddWithValue("@employeeHash", employeePassHash);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void SeedSampleProducts(SqlConnection conn)
        {
            using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM products", conn))
            {
                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                {
                    using (var insertCmd = new SqlCommand(@"
                        INSERT INTO products 
                        (barcode, section, fabric_type, fabric_material, color, is_printed, print_type, is_embroidered, embroidery_type, embroidery_extra_charge, suit_type, wholesale_price, retail_price, current_stock, reorder_point) 
                        VALUES 
                        ('1001', 'Gents', 'Suiting', 'Terry Cotton', 'Navy Blue', 0, NULL, 0, NULL, 0, NULL, 450.00, 650.00, 120.50, 15.00),
                        ('1002', 'Gents', 'Shirting', 'Linen', 'White', 0, NULL, 0, NULL, 0, NULL, 300.00, 480.00, 85.00, 10.00),
                        ('1003', 'Gents', 'Suiting', 'Wash and Wear', 'Khaki', 0, NULL, 0, NULL, 0, NULL, 380.00, 550.00, 95.00, 12.00),
                        ('2001', 'Ladies', 'Lawn', NULL, NULL, 1, 'Digital', 0, NULL, 0, '3-Piece', 800.00, 1200.00, 150.00, 20.00),
                        ('2002', 'Ladies', 'Georgette', NULL, NULL, 0, NULL, 1, 'Chikan', 1500.00, '3-Piece', 1200.00, 2200.00, 40.00, 8.00),
                        ('2003', 'Ladies', 'Chiffon', NULL, NULL, 1, 'Block', 1, 'Hand work', 2500.00, '2-Piece', 1500.00, 2500.00, 30.00, 5.00),
                        ('2004', 'Ladies', 'Lawn', NULL, NULL, 1, 'Screen', 0, NULL, 0, 'Single', 400.00, 650.00, 75.00, 15.00)
                        ;", conn))
                    {
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void SeedDefaultSettings(SqlConnection conn)
        {
            using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM settings", conn))
            {
                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                {
                    using (var insertCmd = new SqlCommand(@"
                        INSERT INTO settings (setting_key, setting_value) VALUES 
                        ('shop_name', N'GARMENTS STORE'),
                        ('shop_address', N'123 Main Cloth Market, Gents & Ladies'),
                        ('shop_phone', N'+92 300 1234567'),
                        ('shop_timezone', N'Pakistan Standard Time');", conn))
                    {
                        insertCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var insertCmd = new SqlCommand(@"
                        IF NOT EXISTS (SELECT * FROM settings WHERE setting_key = 'shop_timezone')
                        INSERT INTO settings (setting_key, setting_value) VALUES ('shop_timezone', N'Pakistan Standard Time');", conn))
                    {
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void UpdateSetting(string key, string value)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE settings SET setting_value = @value WHERE setting_key = @key";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.Parameters.AddWithValue("@value", value);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            string insertQuery = "INSERT INTO settings (setting_key, setting_value) VALUES (@key, @value)";
                            using (var cmd2 = new SqlCommand(insertQuery, conn))
                            {
                                cmd2.Parameters.AddWithValue("@key", key);
                                cmd2.Parameters.AddWithValue("@value", value);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch { /* fail silently */ }
        }
    }
}
