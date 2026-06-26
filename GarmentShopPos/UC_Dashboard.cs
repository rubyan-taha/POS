using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GarmentShopPos
{
    public partial class UC_Dashboard : UserControl
    {
        public UC_Dashboard()
        {
            InitializeComponent();
            this.Load += UC_Dashboard_Load;
        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
            TranslationHelper.ApplyModernGridStyle(dgvLowStockAlerts);
            TranslationHelper.ApplyModernGridStyle(dgvRecentSales);
        }

        public void RefreshData()
        {
            LoadDashboardData();
        }

        public void LoadDashboardData()
        {
            decimal todayRevenue = 0.00m;
            int todayOrders = 0;
            int lowStockCount = 0;

            DateTime localNow = DateTime.Now;
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.ShopTimeZone);
                localNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            }
            catch { }
            DateTime todayStart = localNow.Date;
            DateTime todayEnd = localNow.Date.AddDays(1).AddTicks(-1);

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();

                    // 1. Today's Revenue & Orders Count
                    string queryToday = @"
                        SELECT COALESCE(SUM(total_amount), 0), COUNT(*) 
                        FROM orders 
                        WHERE order_date >= @todayStart AND order_date <= @todayEnd AND is_refunded = 0;";
                    
                    using (var cmd = new SqlCommand(queryToday, conn))
                    {
                        cmd.Parameters.AddWithValue("@todayStart", todayStart);
                        cmd.Parameters.AddWithValue("@todayEnd", todayEnd);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                todayRevenue = reader.GetDecimal(0);
                                todayOrders = reader.GetInt32(1);
                            }
                        }
                    }

                    // 2. Low Stock Count
                    string queryLowCount = "SELECT COUNT(*) FROM products WHERE current_stock <= reorder_point AND is_deleted = 0 AND section = @section;";
                    using (var cmd = new SqlCommand(queryLowCount, conn))
                    {
                        cmd.Parameters.AddWithValue("@section", SessionManager.ActiveShopMode);
                        lowStockCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Populate Low Stock Alerts DataGridView
                    string queryLowStockList = @"
                        SELECT TOP 30 fabric_type AS 'Fabric Type', 
                               section AS 'Section', 
                               current_stock AS 'Stock', 
                               reorder_point AS 'Limit'
                        FROM products 
                        WHERE current_stock <= reorder_point AND is_deleted = 0 AND section = @section
                        ORDER BY current_stock;";

                    DataTable dtLow = new DataTable();
                    using (var cmd = new SqlCommand(queryLowStockList, conn))
                    {
                        cmd.Parameters.AddWithValue("@section", SessionManager.ActiveShopMode);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtLow);
                        }
                    }
                    dgvLowStockAlerts.DataSource = null;
                    dgvLowStockAlerts.DataSource = dtLow;

                    // 4. Populate Live Combined Activities DataGridView (Logins, Logouts, Sales)
                    string queryRecentSales = @"
                        SELECT TOP 30 
                            ActivityUser AS [User],
                            ActivityDetails AS [Activity Details],
                            CONVERT(VARCHAR(8), ActivityTime, 108) AS [Time]
                        FROM (
                            -- Login / Logout Activities
                            SELECT 
                                u.full_name AS ActivityUser,
                                CONCAT(l.activity_type, ' (', l.shop_mode, ' Shop)') AS ActivityDetails,
                                l.timestamp AS ActivityTime
                            FROM user_activity_logs l
                            JOIN users u ON l.user_id = u.id
                            WHERE l.timestamp >= @todayStart AND l.timestamp <= @todayEnd

                            UNION ALL

                            -- Sales Activities
                            SELECT 
                                u.full_name AS ActivityUser,
                                CONCAT('Sale: Rs ', CAST(CAST(o.total_amount AS DECIMAL(10,2)) AS NVARCHAR(20)), ' (', o.receipt_number, ')') AS ActivityDetails,
                                o.order_date AS ActivityTime
                            FROM orders o
                            JOIN users u ON o.salesman_id = u.id
                            WHERE o.order_date >= @todayStart AND o.order_date <= @todayEnd AND o.is_refunded = 0
                        ) AS CombinedActivities
                        ORDER BY ActivityTime DESC;";

                    DataTable dtRecent = new DataTable();
                    using (var cmd = new SqlCommand(queryRecentSales, conn))
                    {
                        cmd.Parameters.AddWithValue("@todayStart", todayStart);
                        cmd.Parameters.AddWithValue("@todayEnd", todayEnd);

                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtRecent);
                        }
                    }
                    dgvRecentSales.DataSource = null;
                    dgvRecentSales.DataSource = dtRecent;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load dashboard data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Update KPI Displays
            lblDailySalesVal.Text = $"Rs {todayRevenue:N2}";
            lblTodayOrdersVal.Text = todayOrders.ToString();
            lblLowStockVal.Text = lowStockCount.ToString();

            ApplyLanguageTranslation();
        }

        public void ApplyLanguageTranslation()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            if (isUrdu)
            {
                this.RightToLeft = RightToLeft.Yes;

                lblTitle.Text = "ڈیش بورڈ کا جائزہ";
                lblDailySalesTitle.Text = "💵 آج کی کل آمدنی";
                lblTodayOrdersTitle.Text = "🛍️ آج کے آرڈر";
                lblLowStockTitle.Text = "⚠️ کم اسٹاک وارننگ";
                lblLowStockAlerts.Text = "⚠️ انتہائی کم اسٹاک الرٹس";
                lblRecentSales.Text = "🕒 آج کی سرگرمیاں";

                if (dgvLowStockAlerts.Columns.Count >= 4)
                {
                    dgvLowStockAlerts.Columns[0].HeaderText = "کپڑے کی قسم";
                    dgvLowStockAlerts.Columns[1].HeaderText = "سیکشن";
                    dgvLowStockAlerts.Columns[2].HeaderText = "اسٹاک";
                    dgvLowStockAlerts.Columns[3].HeaderText = "حد (لیمیٹ)";
                }

                if (dgvRecentSales.Columns.Count >= 3)
                {
                    dgvRecentSales.Columns[0].HeaderText = "صارف";
                    dgvRecentSales.Columns[1].HeaderText = "تفصیل";
                    dgvRecentSales.Columns[2].HeaderText = "وقت";
                }
            }
            else
            {
                this.RightToLeft = RightToLeft.No;

                lblTitle.Text = "Dashboard Overview";
                lblDailySalesTitle.Text = "💵 Today's Revenue";
                lblTodayOrdersTitle.Text = "🛍️ Today's Orders";
                lblLowStockTitle.Text = "⚠️ Low Stock Warning";
                lblLowStockAlerts.Text = "⚠️ Urgent Low Stock Warnings";
                lblRecentSales.Text = "🕒 Today's Activities";

                if (dgvLowStockAlerts.Columns.Count >= 4)
                {
                    dgvLowStockAlerts.Columns[0].HeaderText = "Fabric Type";
                    dgvLowStockAlerts.Columns[1].HeaderText = "Section";
                    dgvLowStockAlerts.Columns[2].HeaderText = "Stock";
                    dgvLowStockAlerts.Columns[3].HeaderText = "Limit";
                }

                if (dgvRecentSales.Columns.Count >= 3)
                {
                    dgvRecentSales.Columns[0].HeaderText = "User";
                    dgvRecentSales.Columns[1].HeaderText = "Activity Details";
                    dgvRecentSales.Columns[2].HeaderText = "Time";
                }
            }
        }
    }
}
