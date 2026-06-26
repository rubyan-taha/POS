using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GarmentShopPos
{
    public partial class UC_ProductEntry : UserControl
    {
        public UC_ProductEntry()
        {
            InitializeComponent();
        }

        private void UC_ProductEntry_Load(object sender, EventArgs e)
        {
            ApplyLanguageTranslation();
            // Lock tabs based on Active Shop Mode
            if (SessionManager.ActiveShopMode == "Gents")
            {
                tabControlEntry.TabPages.Remove(tabLadies);
            }
            else if (SessionManager.ActiveShopMode == "Ladies")
            {
                tabControlEntry.TabPages.Remove(tabGents);
            }

            // Set up FlatStyle and premium visual background colors
            cbGFabricType.FlatStyle = FlatStyle.Flat;
            cbGFabricType.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbLFabricType.FlatStyle = FlatStyle.Flat;
            cbLFabricType.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbLSuitType.FlatStyle = FlatStyle.Flat;
            cbLSuitType.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbLPrintType.FlatStyle = FlatStyle.Flat;
            cbLPrintType.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbLEmbroideryType.FlatStyle = FlatStyle.Flat;
            cbLEmbroideryType.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);

            // Set up Ladies Suit Type
            cbLSuitType.Items.Clear();
            cbLSuitType.Items.AddRange(new string[] { "3-Piece", "2-Piece", "Single" });
            cbLSuitType.SelectedIndex = 0;

            // Set up Ladies Print Types
            cbLPrintType.Items.Clear();
            cbLPrintType.Items.AddRange(new string[] { "Digital", "Block", "Screen" });
            cbLPrintType.SelectedIndex = 0;

            // Set up Ladies Embroidery Types
            cbLEmbroideryType.Items.Clear();
            cbLEmbroideryType.Items.AddRange(new string[] { "Hand work", "Machine", "Chikan" });
            cbLEmbroideryType.SelectedIndex = 0;

            cbGFabricType.TextChanged += cbGFabricType_TextChanged;
            cbGFabricType.SelectedIndexChanged += cbGFabricType_SelectedIndexChanged;
            cbLFabricType.TextChanged += cbLFabricType_TextChanged;
            cbLFabricType.SelectedIndexChanged += cbLFabricType_SelectedIndexChanged;
        }

        private void chkLPrinted_CheckedChanged(object sender, EventArgs e)
        {
            cbLPrintType.Enabled = chkLPrinted.Checked;
        }

        private void chkLEmbroidered_CheckedChanged(object sender, EventArgs e)
        {
            cbLEmbroideryType.Enabled = chkLEmbroidered.Checked;
            txtLEmbCharge.Enabled = chkLEmbroidered.Checked;
            if (!chkLEmbroidered.Checked)
            {
                txtLEmbCharge.Text = "0.00";
            }
        }

        private void btnSaveGents_Click(object sender, EventArgs e)
        {
            string rawFabric = cbGFabricType.Text.Trim();
            string fabric = TranslationHelper.GetDatabaseFabricType(rawFabric, "Gents");
            string material = txtGMaterial.Text.Trim();
            string color = txtGColor.Text.Trim();
            string barcode = txtGBarcode.Text.Trim();

            if (string.IsNullOrEmpty(fabric) || string.IsNullOrEmpty(material))
            {
                MessageBox.Show("Fabric Type and Material are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(barcode))
            {
                barcode = GenerateUniqueBarcode();
            }
            else
            {
                // Check if duplicate barcode exists
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM products WHERE barcode = @barcode AND is_deleted = 0", conn))
                    {
                        cmd.Parameters.AddWithValue("@barcode", barcode);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("This barcode is already assigned to another product.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            if (!decimal.TryParse(txtGWholesale.Text.Trim(), out decimal wholesale) || wholesale < 0)
            {
                MessageBox.Show("Please enter a valid Wholesale Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGRetail.Text.Trim(), out decimal retail) || retail < 0)
            {
                MessageBox.Show("Please enter a valid Retail Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (wholesale > retail)
            {
                MessageBox.Show("Wholesale price cannot be greater than retail price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGStock.Text.Trim(), out decimal stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid stock level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGReorder.Text.Trim(), out decimal reorder) || reorder < 0)
            {
                MessageBox.Show("Please enter a valid reorder warning point.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string insertQuery = @"
                                INSERT INTO products 
                                (barcode, section, fabric_type, fabric_material, color, is_printed, is_embroidered, wholesale_price, retail_price, current_stock, reorder_point)
                                VALUES (@barcode, 'Gents', @fabric, @material, @color, 0, 0, @wholesale, @retail, @stock, @reorder);
                                SELECT SCOPE_IDENTITY();";

                            int productId = 0;
                            using (var cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@barcode", barcode);
                                cmd.Parameters.AddWithValue("@fabric", fabric);
                                cmd.Parameters.AddWithValue("@material", material);
                                cmd.Parameters.AddWithValue("@color", string.IsNullOrEmpty(color) ? (object)DBNull.Value : color);
                                cmd.Parameters.AddWithValue("@wholesale", wholesale);
                                cmd.Parameters.AddWithValue("@retail", retail);
                                cmd.Parameters.AddWithValue("@stock", stock);
                                cmd.Parameters.AddWithValue("@reorder", reorder);

                                productId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // Log initial purchase record if stock > 0
                            if (stock > 0)
                            {
                                string logPurchaseQuery = @"
                                    INSERT INTO purchase_records (product_id, quantity, supplier_name, purchase_price, purchase_date)
                                    VALUES (@prodId, @qty, 'Initial Setup Stock', @price, CURRENT_TIMESTAMP);";

                                using (var cmd = new SqlCommand(logPurchaseQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@prodId", productId);
                                    cmd.Parameters.AddWithValue("@qty", stock);
                                    cmd.Parameters.AddWithValue("@price", wholesale);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Gents product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearGentsForm();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save product:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveLadies_Click(object sender, EventArgs e)
        {
            string rawFabric = cbLFabricType.Text.Trim();
            string fabric = TranslationHelper.GetDatabaseFabricType(rawFabric, "Ladies");
            string suitStyle = cbLSuitType.Text;
            string barcode = txtLBarcode.Text.Trim();

            if (string.IsNullOrEmpty(fabric))
            {
                MessageBox.Show("Fabric Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(barcode))
            {
                barcode = GenerateUniqueBarcode();
            }
            else
            {
                // Check if duplicate barcode exists
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM products WHERE barcode = @barcode AND is_deleted = 0", conn))
                    {
                        cmd.Parameters.AddWithValue("@barcode", barcode);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("This barcode is already assigned to another product.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            if (!decimal.TryParse(txtLWholesale.Text.Trim(), out decimal wholesale) || wholesale < 0)
            {
                MessageBox.Show("Please enter a valid Wholesale Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtLRetail.Text.Trim(), out decimal retail) || retail < 0)
            {
                MessageBox.Show("Please enter a valid Retail Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (wholesale > retail)
            {
                MessageBox.Show("Wholesale price cannot be greater than retail price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtLStock.Text.Trim(), out decimal stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid stock level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtLReorder.Text.Trim(), out decimal reorder) || reorder < 0)
            {
                MessageBox.Show("Please enter a valid reorder warning point.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal embCharge = 0.00m;
            if (chkLEmbroidered.Checked)
            {
                if (!decimal.TryParse(txtLEmbCharge.Text.Trim(), out embCharge) || embCharge < 0)
                {
                    MessageBox.Show("Please enter a valid embroidery extra charge.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string insertQuery = @"
                                INSERT INTO products 
                                (barcode, section, fabric_type, suit_type, is_printed, print_type, is_embroidered, embroidery_type, embroidery_extra_charge, wholesale_price, retail_price, current_stock, reorder_point)
                                VALUES (@barcode, 'Ladies', @fabric, @suitType, @isPrinted, @printType, @isEmb, @embType, @embCharge, @wholesale, @retail, @stock, @reorder);
                                SELECT SCOPE_IDENTITY();";

                            int productId = 0;
                            using (var cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@barcode", barcode);
                                cmd.Parameters.AddWithValue("@fabric", fabric);
                                cmd.Parameters.AddWithValue("@suitType", suitStyle);
                                cmd.Parameters.AddWithValue("@isPrinted", chkLPrinted.Checked ? 1 : 0);
                                cmd.Parameters.AddWithValue("@printType", chkLPrinted.Checked ? cbLPrintType.Text : (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@isEmb", chkLEmbroidered.Checked ? 1 : 0);
                                cmd.Parameters.AddWithValue("@embType", chkLEmbroidered.Checked ? cbLEmbroideryType.Text : (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@embCharge", embCharge);
                                cmd.Parameters.AddWithValue("@wholesale", wholesale);
                                cmd.Parameters.AddWithValue("@retail", retail);
                                cmd.Parameters.AddWithValue("@stock", stock);
                                cmd.Parameters.AddWithValue("@reorder", reorder);

                                productId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // Log initial purchase record if stock > 0
                            if (stock > 0)
                            {
                                string logPurchaseQuery = @"
                                    INSERT INTO purchase_records (product_id, quantity, supplier_name, purchase_price, purchase_date)
                                    VALUES (@prodId, @qty, 'Initial Setup Stock', @price, CURRENT_TIMESTAMP);";

                                using (var cmd = new SqlCommand(logPurchaseQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@prodId", productId);
                                    cmd.Parameters.AddWithValue("@qty", stock);
                                    cmd.Parameters.AddWithValue("@price", wholesale);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Ladies product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearLadiesForm();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save product:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearGentsForm()
        {
            cbGFabricType.SelectedIndex = -1;
            cbGFabricType.Text = "";
            txtGMaterial.Clear();
            txtGColor.Clear();
            txtGWholesale.Clear();
            txtGRetail.Clear();
            txtGStock.Text = "0";
            txtGReorder.Text = "10";
            txtGBarcode.Clear();
            cbGFabricType.Focus();
        }

        private void ClearLadiesForm()
        {
            cbLFabricType.SelectedIndex = -1;
            cbLFabricType.Text = "";
            cbLSuitType.SelectedIndex = 0;
            chkLPrinted.Checked = false;
            cbLPrintType.SelectedIndex = 0;
            cbLPrintType.Enabled = false;
            chkLEmbroidered.Checked = false;
            cbLEmbroideryType.SelectedIndex = 0;
            cbLEmbroideryType.Enabled = false;
            txtLEmbCharge.Text = "0.00";
            txtLEmbCharge.Enabled = false;
            txtLWholesale.Clear();
            txtLRetail.Clear();
            txtLStock.Text = "0";
            txtLReorder.Text = "10";
            txtLBarcode.Clear();
            cbLFabricType.Focus();
        }

        private void txtGBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtGWholesale.Focus();
            }
        }

        private void txtLBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtLWholesale.Focus();
            }
        }
        private string GenerateUniqueBarcode()
        {
            string newBarcode = "";
            bool isUnique = false;
            while (!isUnique)
            {
                newBarcode = "BAR-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM products WHERE barcode = @barcode AND is_deleted = 0", conn))
                    {
                        cmd.Parameters.AddWithValue("@barcode", newBarcode);
                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            isUnique = true;
                        }
                    }
                }
            }
            return newBarcode;
        }

        public void ApplyLanguageTranslation()
        {
            string oldG = cbGFabricType.Text;
            string oldL = cbLFabricType.Text;

            if (SessionManager.ShopLanguage == "Urdu")
            {
                this.RightToLeft = RightToLeft.Yes;

                cbGFabricType.Items.Clear();
                cbGFabricType.Items.AddRange(TranslationHelper.GentsCategoriesUrdu);
                cbLFabricType.Items.Clear();
                cbLFabricType.Items.AddRange(TranslationHelper.LadiesCategoriesUrdu);

                if (!string.IsNullOrEmpty(oldG))
                    cbGFabricType.Text = TranslationHelper.TranslateFabric(oldG, "Gents");
                if (!string.IsNullOrEmpty(oldL))
                    cbLFabricType.Text = TranslationHelper.TranslateFabric(oldL, "Ladies");

                lblTitle.Text = "نئے کپڑے / اسٹاک کا اندراج";
                tabGents.Text = "👔 مردانہ سیکشن (کپڑے کا اندراج)";
                tabLadies.Text = "👗 زنانہ سیکشن (کپڑے کا اندراج)";

                // Gents fields
                lblGFabricType.Text = "کپڑے کی قسم (مثال: شرٹنگ): *";
                lblGMaterial.Text = "کپڑے کا مواد (مثال: کاٹن):";
                lblGColor.Text = "کپڑے کا رنگ (مثال: نیوی بلیو):";
                lblGWholesale.Text = "تھوک قیمت (ہول سیل): *";
                lblGRetail.Text = "پرچون قیمت (ریٹیل): *";
                lblGStock.Text = "ابتدائی اسٹاک (میٹر): *";
                lblGReorder.Text = "ری آرڈر وارننگ پوائنٹ:*";
                lblGBarcode.Text = "بارکوڈ / SKU:";
                btnSaveGents.Text = "💾 مردانہ کپڑا محفوظ کریں";

                // Ladies fields
                lblLFabricType.Text = "کپڑے کی قسم (مثال: لان، شفون):*";
                lblLSuitType.Text = "سوٹ کی قسم (مثال: تھری پیس):";
                lblLEmbCharge.Text = "کڑھائی کے اضافی چارجز:";
                lblLWholesale.Text = "تھوک قیمت (ہول سیل): *";
                lblLRetail.Text = "پرچون قیمت (ریٹیل): *";
                lblLStock.Text = "ابتدائی اسٹاک (میٹر): *";
                lblLReorder.Text = "ری آرڈر وارننگ پوائنٹ:*";
                lblLBarcode.Text = "بارکوڈ / SKU:";
                chkLEmbroidered.Text = "کڑھائی شامل ہے؟";
                chkLPrinted.Text = "پرنٹڈ کپڑا ہے؟";
                btnSaveLadies.Text = "💾 زنانہ کپڑا محفوظ کریں";
            }
            else
            {
                this.RightToLeft = RightToLeft.No;

                cbGFabricType.Items.Clear();
                cbGFabricType.Items.AddRange(TranslationHelper.GentsCategoriesEnglish);
                cbLFabricType.Items.Clear();
                cbLFabricType.Items.AddRange(TranslationHelper.LadiesCategoriesEnglish);

                if (!string.IsNullOrEmpty(oldG))
                    cbGFabricType.Text = TranslationHelper.GetDatabaseFabricType(oldG, "Gents");
                if (!string.IsNullOrEmpty(oldL))
                    cbLFabricType.Text = TranslationHelper.GetDatabaseFabricType(oldL, "Ladies");

                lblTitle.Text = "Add New Fabric / Stock";
                tabGents.Text = "Gents Section (Men's Fabric)";
                tabLadies.Text = "Ladies Section (Women's Fabric)";

                // Gents fields
                lblGFabricType.Text = "Fabric Type (e.g. Shirting): *";
                lblGMaterial.Text = "Fabric Material (e.g. Cotton):";
                lblGColor.Text = "Fabric Color (e.g. Navy Blue):";
                lblGWholesale.Text = "Wholesale Price (per meter): *";
                lblGRetail.Text = "Retail Price (per meter): *";
                lblGStock.Text = "Initial Stock (meters): *";
                lblGReorder.Text = "Reorder Alert Limit:*";
                lblGBarcode.Text = "Barcode / SKU:";
                btnSaveGents.Text = "💾 Save Gents Product";

                // Ladies fields
                lblLFabricType.Text = "Fabric Type (e.g. Lawn Chiffon):*";
                lblLSuitType.Text = "Suit Style Type (e.g. 3pc):";
                lblLEmbCharge.Text = "Embroidery Extra Charge:";
                lblLWholesale.Text = "Wholesale Price:*";
                lblLRetail.Text = "Retail Price (per meter):*";
                lblLStock.Text = "Initial Stock (meters):*";
                lblLReorder.Text = "Reorder Warning Alert Point:*";
                lblLBarcode.Text = "Barcode / SKU:";
                chkLEmbroidered.Text = "Includes Embroidery?";
                chkLPrinted.Text = "Printed Fabric?";
                btnSaveLadies.Text = "💾 Save Ladies Product";
            }

            UpdateGentsLabelsForBox();
            UpdateLadiesLabelsForBox();
        }

        private void cbGFabricType_TextChanged(object? sender, EventArgs e)
        {
            UpdateGentsLabelsForBox();
        }

        private void cbGFabricType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateGentsLabelsForBox();
        }

        private void cbLFabricType_TextChanged(object? sender, EventArgs e)
        {
            UpdateLadiesLabelsForBox();
        }

        private void cbLFabricType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateLadiesLabelsForBox();
        }

        private void UpdateGentsLabelsForBox()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            bool isBox = cbGFabricType.Text.IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0;

            if (isUrdu)
            {
                lblGWholesale.Text = isBox ? "تھوک قیمت: *" : "تھوک قیمت (ہول سیل): *";
                lblGRetail.Text = isBox ? "پرچون قیمت: *" : "پرچون قیمت (میٹر): *";
                lblGStock.Text = isBox ? "ابتدائی اسٹاک (باکس): *" : "ابتدائی اسٹاک (میٹر): *";
            }
            else
            {
                lblGWholesale.Text = isBox ? "Wholesale Price: *" : "Wholesale Price (per meter): *";
                lblGRetail.Text = isBox ? "Retail Price: *" : "Retail Price (per meter): *";
                lblGStock.Text = isBox ? "Initial Stock (boxes): *" : "Initial Stock (meters): *";
            }
        }

        private void UpdateLadiesLabelsForBox()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            bool isBox = cbLFabricType.Text.IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0;

            if (isUrdu)
            {
                lblLWholesale.Text = isBox ? "تھوک قیمت: *" : "تھوک قیمت (ہول سیل): *";
                lblLRetail.Text = isBox ? "پرچون قیمت: *" : "پرچون قیمت (میٹر): *";
                lblLStock.Text = isBox ? "ابتدائی اسٹاک (باکس): *" : "ابتدائی اسٹاک (میٹر): *";
            }
            else
            {
                lblLWholesale.Text = isBox ? "Wholesale Price: *" : "Wholesale Price:*";
                lblLRetail.Text = isBox ? "Retail Price: *" : "Retail Price (per meter):*";
                lblLStock.Text = isBox ? "Initial Stock (boxes): *" : "Initial Stock (meters):*";
            }
        }
    }
}
