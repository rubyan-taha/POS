using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using GarmentShopPos.Models;

namespace GarmentShopPos
{
    public partial class UC_Audit : UserControl
    {
        private List<Product> products = new List<Product>();
        private Product? selectedRProduct;
        private Product? selectedWProduct;

        private int hoveredTabIndex = -1;

        public UC_Audit()
        {
            InitializeComponent();
            
            // Custom owner-draw tab rendering for modern premium button look
            this.tabControlAudit.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControlAudit.ItemSize = new System.Drawing.Size(250, 36);
            this.tabControlAudit.SizeMode = TabSizeMode.Fixed;
            
            this.tabControlAudit.DrawItem += new DrawItemEventHandler(tabControlAudit_DrawItem);
            this.tabControlAudit.MouseMove += new MouseEventHandler(tabControlAudit_MouseMove);
            this.tabControlAudit.MouseLeave += new EventHandler(tabControlAudit_MouseLeave);
        }

        private void UC_Audit_Load(object sender, EventArgs e)
        {
            dtpLogStart.Value = DateTime.Today.AddDays(-30);
            dtpLogEnd.Value = DateTime.Today;
            ApplyLanguageTranslation();
        }

        public void RefreshData()
        {
            LoadProducts();
            LoadLogs();
        }

        private void LoadProducts()
        {
            products.Clear();
            cbRProduct.Items.Clear();
            cbWProduct.Items.Clear();

            string query = "SELECT * FROM products WHERE is_deleted = 0 AND section = @section ORDER BY fabric_type";
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@section", SessionManager.ActiveShopMode);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prod = new Product
                            {
                                Id = reader.GetInt32("id"),
                                Barcode = reader.IsDBNull(reader.GetOrdinal("barcode")) ? null : reader.GetString("barcode"),
                                Section = reader.GetString("section"),
                                FabricType = reader.GetString("fabric_type"),
                                FabricMaterial = reader.IsDBNull(reader.GetOrdinal("fabric_material")) ? null : reader.GetString("fabric_material"),
                                Color = reader.IsDBNull(reader.GetOrdinal("color")) ? null : reader.GetString("color"),
                                WholesalePrice = reader.GetDecimal("wholesale_price"),
                                RetailPrice = reader.GetDecimal("retail_price"),
                                CurrentStock = reader.GetDecimal("current_stock"),
                                ReorderPoint = reader.GetDecimal("reorder_point"),
                                SuitType = reader.IsDBNull(reader.GetOrdinal("suit_type")) ? null : reader.GetString("suit_type"),
                                IsPrinted = reader.GetByte("is_printed") == 1,
                                PrintType = reader.IsDBNull(reader.GetOrdinal("print_type")) ? null : reader.GetString("print_type"),
                                IsEmbroidered = reader.GetByte("is_embroidered") == 1,
                                EmbroideryType = reader.IsDBNull(reader.GetOrdinal("embroidery_type")) ? null : reader.GetString("embroidery_type"),
                            };
                            products.Add(prod);
                            
                            cbRProduct.Items.Add(prod);
                            cbWProduct.Items.Add(prod);
                        }
                    }
                }
            }

            // DisplayMember binding
            cbRProduct.DisplayMember = "DisplayName";
            cbWProduct.DisplayMember = "DisplayName";

            if (cbRProduct.Items.Count > 0) cbRProduct.SelectedIndex = 0;
            if (cbWProduct.Items.Count > 0) cbWProduct.SelectedIndex = 0;
        }

        private void cbRProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRProduct = cbRProduct.SelectedItem as Product;
            if (selectedRProduct != null)
            {
                bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
                bool isBox = (selectedRProduct.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             (selectedRProduct.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;

                string unit = isUrdu ? (isBox ? "باکس" : "میٹر/گز") : (isBox ? "boxes" : "yards");
                lblRSystemQtyVal.Text = isBox ? $"{selectedRProduct.CurrentStock:N0} {unit}" : $"{selectedRProduct.CurrentStock:N2} {unit}";

                lblRSystemQty.Text = isUrdu ? (isBox ? "سسٹم میں موجود باکس:" : "سسٹم میں موجود میٹر/گز:") : (isBox ? "Boxes in System:" : "Yards in System:");
                lblRAdjustmentQty.Text = isUrdu ? (isBox ? "اصلاحی مقدار (باکس): *" : "اصلاحی مقدار (تبدیلی): *") : (isBox ? "Adjustment Boxes (Count):*" : "Adjustment Yards (Count):*");

                UpdateReconciliationPreview();
            }
        }

        private void cbWProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedWProduct = cbWProduct.SelectedItem as Product;
            if (selectedWProduct != null)
            {
                bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
                bool isBox = (selectedWProduct.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             (selectedWProduct.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;

                string unit = isUrdu ? (isBox ? "باکس" : "میٹر/گز") : (isBox ? "boxes" : "yards");
                lblWSysQtyVal.Text = isBox ? $"{selectedWProduct.CurrentStock:N0} {unit}" : $"{selectedWProduct.CurrentStock:N2} {unit}";

                lblWSysQty.Text = isUrdu ? (isBox ? "سسٹم میں موجود باکس:" : "سسٹم میں موجود میٹر/گز:") : (isBox ? "Boxes in System:" : "Yards in System:");
                lblWQty.Text = isUrdu ? (isBox ? "ضائع یا خراب شدہ باکس: *" : "ضائع یا خراب شدہ میٹر/گز: *") : (isBox ? "Damaged / Lost Boxes:*" : "Damaged / Lost Yards:*");
            }
        }

        private void txtRAdjustmentQty_TextChanged(object sender, EventArgs e)
        {
            UpdateReconciliationPreview();
        }

        private void rbExtra_CheckedChanged(object sender, EventArgs e)
        {
            UpdateReconciliationPreview();
        }

        private void rbDiff_CheckedChanged(object sender, EventArgs e)
        {
            UpdateReconciliationPreview();
        }

        private void UpdateReconciliationPreview()
        {
            if (selectedRProduct == null) return;

            decimal currentStock = selectedRProduct.CurrentStock;
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            bool isBox = (selectedRProduct.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         (selectedRProduct.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;
            string unit = isUrdu ? (isBox ? "باکس" : "میٹر/گز") : (isBox ? "boxes" : "yards");
            
            if (decimal.TryParse(txtRAdjustmentQty.Text.Trim(), out decimal adjustment) && adjustment >= 0)
            {
                decimal newStock = currentStock;
                if (rbExtra.Checked)
                {
                    newStock = currentStock + adjustment;
                    lblRNewStockVal.Text = isBox ? $"{newStock:N0} {unit}" : $"{newStock:N2} {unit}";
                    lblRNewStockVal.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80); // Surplus green
                }
                else if (rbDiff.Checked)
                {
                    newStock = currentStock - adjustment;
                    if (newStock < 0) newStock = 0;
                    lblRNewStockVal.Text = isBox ? $"{newStock:N0} {unit}" : $"{newStock:N2} {unit}";
                    lblRNewStockVal.ForeColor = System.Drawing.Color.FromArgb(219, 68, 85); // Shortage red
                }
                else
                {
                    lblRNewStockVal.Text = isBox ? $"{newStock:N0} {unit}" : $"{newStock:N2} {unit}";
                    lblRNewStockVal.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                lblRNewStockVal.Text = isBox ? $"{currentStock:N0} {unit}" : $"{currentStock:N2} {unit}";
                lblRNewStockVal.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void btnReconcile_Click(object sender, EventArgs e)
        {
            if (selectedRProduct == null) return;

            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            if (!decimal.TryParse(txtRAdjustmentQty.Text.Trim(), out decimal adjustment) || adjustment < 0)
            {
                MessageBox.Show(isUrdu ? "براہ کرم درست اصلاحی مقدار درج کریں۔" : "Please enter a valid adjustment quantity.", isUrdu ? "تصدیقی غلطی" : "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (adjustment == 0)
            {
                MessageBox.Show(isUrdu ? "اصلاحی مقدار صفر ہے۔ کوئی تبدیلی نہیں کی جائے گی۔" : "Adjustment quantity is zero. No change will be made.", isUrdu ? "کوئی تبدیلی نہیں" : "No Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool isBox = (selectedRProduct.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         (selectedRProduct.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;

            if (isBox && adjustment % 1 != 0)
            {
                MessageBox.Show(isUrdu ? "باکس آئٹم کے لیے مقدار صرف مکمل عدد ہونی چاہیے۔" : "For box items, quantity must be a whole number.", isUrdu ? "تصدیقی غلطی" : "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rbExtra.Checked && !rbDiff.Checked)
            {
                MessageBox.Show(isUrdu ? "براہ کرم منتخب کریں کہ آیا یہ سرپلس (+) ہے یا شارٹیج (-)" : "Please select whether this is a Surplus (+) or Shortage (-).", isUrdu ? "تصدیقی غلطی" : "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal newStock = selectedRProduct.CurrentStock;
            decimal diff = 0;
            if (rbExtra.Checked)
            {
                newStock = selectedRProduct.CurrentStock + adjustment;
                diff = adjustment;
            }
            else if (rbDiff.Checked)
            {
                newStock = selectedRProduct.CurrentStock - adjustment;
                if (newStock < 0)
                {
                    string unitName = isUrdu ? (isBox ? "باکس" : "میٹر/گز") : (isBox ? "boxes" : "yards");
                    string stockFormatted = isBox ? $"{selectedRProduct.CurrentStock:N0}" : $"{selectedRProduct.CurrentStock:N2}";
                    MessageBox.Show(isUrdu ? $"اصلاحی مقدار موجودہ اسٹاک لیول ({stockFormatted} {unitName}) سے زیادہ ہے۔ اسٹاک منفی نہیں ہو سکتا۔" : $"Adjustment exceeds current stock level ({stockFormatted} {unitName}). Adjusted stock cannot be negative.", isUrdu ? "تصدیقی غلطی" : "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                diff = -adjustment;
            }

            string unit = isUrdu ? (isBox ? "باکس" : "میٹر/گز") : (isBox ? "boxes" : "yards");
            string formattedAdjustment = isBox ? $"{adjustment:N0}" : $"{adjustment:N2}";
            string formattedNewStock = isBox ? $"{newStock:N0}" : $"{newStock:N2}";

            string confirmMsg = isUrdu 
                ? $"کیا آپ '{selectedRProduct.ShortName}' میں {(rbExtra.Checked ? "+" : "-")}{formattedAdjustment} {unit} کی اصلاح لاگو کرنا چاہتے ہیں؟\nنیا اسٹاک لیول {formattedNewStock} {unit} ہو جائے گا۔"
                : $"Apply adjustment of {(rbExtra.Checked ? "+" : "-")}{formattedAdjustment} {unit} to '{selectedRProduct.ShortName}'?\nNew stock level will be {formattedNewStock} {unit}.";

            var confirm = MessageBox.Show(confirmMsg, isUrdu ? "اصلاح کی تصدیق کریں" : "Confirm Adjustment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            int userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Id : 1;

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Update Product Stock Count
                        string updateStockQuery = "UPDATE products SET current_stock = @stock WHERE id = @id";
                        using (var cmd = new SqlCommand(updateStockQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@stock", newStock);
                            cmd.Parameters.AddWithValue("@id", selectedRProduct.Id);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Log Discrepancy
                        if (diff < 0)
                        {
                            // Log shortage as wastage/loss
                            string insertWastageQuery = @"
                                INSERT INTO wastage_logs (product_id, quantity, reason, logged_at, logged_by)
                                VALUES (@productId, @qty, 'Audit Stock Count Shortage', CURRENT_TIMESTAMP, @userId);";

                            using (var cmd = new SqlCommand(insertWastageQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@productId", selectedRProduct.Id);
                                cmd.Parameters.AddWithValue("@qty", Math.Abs(diff));
                                cmd.Parameters.AddWithValue("@userId", userId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Log surplus as purchase record entry
                            string insertPurchaseQuery = @"
                                INSERT INTO purchase_records (product_id, quantity, supplier_name, purchase_price, purchase_date)
                                VALUES (@productId, @qty, 'Audit Stock Count Surplus Adjustment', @price, CURRENT_TIMESTAMP);";

                            using (var cmd = new SqlCommand(insertPurchaseQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@productId", selectedRProduct.Id);
                                cmd.Parameters.AddWithValue("@qty", diff);
                                cmd.Parameters.AddWithValue("@price", selectedRProduct.WholesalePrice);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show(isUrdu ? "اسٹاک کامیابی سے اپ ڈیٹ ہو گیا ہے!" : "Stock adjusted successfully!", isUrdu ? "کامیابی" : "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtRAdjustmentQty.Clear();
                        rbExtra.Checked = false;
                        rbDiff.Checked = false;
                        LoadProducts();
                        LoadLogs();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(isUrdu ? $"اسٹاک اپ ڈیٹ کرنے میں ناکامی:\n{ex.Message}" : $"Failed to save stock update:\n{ex.Message}", isUrdu ? "خرابی" : "Error Updating Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLogWastage_Click(object sender, EventArgs e)
        {
            if (selectedWProduct == null) return;
            
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            bool isBox = (selectedWProduct.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         (selectedWProduct.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;

            if (!decimal.TryParse(txtWQty.Text.Trim(), out decimal qty) || qty <= 0)
            {
                MessageBox.Show(isUrdu ? "براہ کرم خارج کی گئی درست مقدار درج کریں۔" : "Please enter a valid number of units removed.", isUrdu ? "تصدیق" : "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isBox && qty % 1 != 0)
            {
                MessageBox.Show(isUrdu ? "باکس آئٹم کے لیے مقدار صرف مکمل عدد ہونی چاہیے۔" : "For box items, quantity must be a whole number.", isUrdu ? "تصدیقی غلطی" : "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string unit = isUrdu ? (isBox ? "باکس" : "میٹر/گز") : (isBox ? "boxes" : "yards");
            string formattedQty = isBox ? $"{qty:N0}" : $"{qty:N2}";
            string formattedCurrentStock = isBox ? $"{selectedWProduct.CurrentStock:N0}" : $"{selectedWProduct.CurrentStock:N2}";

            if (qty > selectedWProduct.CurrentStock)
            {
                string lowStockMsg = isUrdu 
                    ? $"خارج کی گئی مقدار ({formattedQty} {unit}) سسٹم میں موجود مقدار ({formattedCurrentStock} {unit}) سے زیادہ نہیں ہوسکتی۔"
                    : $"The quantity removed ({formattedQty} {unit}) cannot be more than what we have in system ({formattedCurrentStock} {unit}).";
                MessageBox.Show(lowStockMsg, isUrdu ? "کافی اسٹاک نہیں ہے" : "Not Enough Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reason = cbWReason.Text;
            string confirmMsg = isUrdu
                ? $"کیا آپ '{selectedWProduct.ShortName}' سے {formattedQty} {unit} خارج کرنا چاہتے ہیں کیونکہ یہ '{reason}' ہے؟"
                : $"Remove {formattedQty} {unit} from '{selectedWProduct.ShortName}' because it is '{reason}'?";

            var confirm = MessageBox.Show(confirmMsg, isUrdu ? "اخراج کی تصدیق کریں" : "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            int userId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Id : 1;

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Decrement Stock
                        string updateStockQuery = "UPDATE products SET current_stock = current_stock - @qty WHERE id = @id";
                        using (var cmd = new SqlCommand(updateStockQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@qty", qty);
                            cmd.Parameters.AddWithValue("@id", selectedWProduct.Id);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Insert Wastage Log
                        string insertWastageQuery = @"
                            INSERT INTO wastage_logs (product_id, quantity, reason, logged_at, logged_by)
                            VALUES (@productId, @qty, @reason, CURRENT_TIMESTAMP, @userId);";

                        using (var cmd = new SqlCommand(insertWastageQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@productId", selectedWProduct.Id);
                            cmd.Parameters.AddWithValue("@qty", qty);
                            cmd.Parameters.AddWithValue("@reason", reason);
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show(isUrdu ? "نقصان دہ/ضائع شدہ اسٹاک درج کر کے سسٹم سے نکال دیا گیا ہے۔" : "Damaged/Lost stock has been recorded and removed from system.", isUrdu ? "اسٹاک خارج ہو گیا" : "Stock Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtWQty.Clear();
                        LoadProducts();
                        LoadLogs();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(isUrdu ? $"نقصان دہ اسٹاک درج کرنے میں ناکامی:\n{ex.Message}" : $"Failed to record damaged stock:\n{ex.Message}", isUrdu ? "خرابی" : "Error Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void rbLogFilter_Changed(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void LoadLogs()
        {
            DataTable dt = new DataTable();
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            DateTime startDate = dtpLogStart.Value.Date;
            DateTime endDate = dtpLogEnd.Value.Date.AddDays(1);

            if (rbShowWastage.Checked)
            {
                string logIdCol = isUrdu ? "لاگ آئی ڈی" : "Log ID";
                string fabricCol = isUrdu ? "کپڑا" : "Fabric";
                string specsCol = isUrdu ? "تفصیلات" : "Specs";
                string qtyCol = isUrdu ? "خارج شدہ مقدار" : "Yards Removed";
                string reasonCol = isUrdu ? "وجہ" : "Reason";
                string dateCol = isUrdu ? "تاریخ" : "Date Logged";
                string userCol = isUrdu ? "درج کنندہ" : "Logged By";

                lblLogsTitle.Text = isUrdu ? "ضائع یا خراب شدہ اسٹاک کا حالیہ لاگ" : "Recent Damaged / Lost Stock Logs";
                string query = $@"
                    SELECT w.id AS [{logIdCol}], p.fabric_type AS [{fabricCol}], 
                           CONCAT(p.section, ' - ', CASE WHEN p.section='Gents' THEN CONCAT(p.fabric_material, ' (', p.color, ')') ELSE p.suit_type END) AS [{specsCol}],
                           w.quantity AS [{qtyCol}], w.reason AS [{reasonCol}], 
                           w.logged_at AS [{dateCol}], u.username AS [{userCol}]
                    FROM wastage_logs w
                    JOIN products p ON w.product_id = p.id
                    JOIN users u ON w.logged_by = u.id
                    WHERE p.section = @section AND w.logged_at >= @startDate AND w.logged_at < @endDate
                    ORDER BY w.logged_at DESC;";

                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@section", SessionManager.ActiveShopMode);
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            else // Show Purchases/Stock In
            {
                string logIdCol = isUrdu ? "لاگ آئی ڈی" : "Log ID";
                string fabricCol = isUrdu ? "کپڑا" : "Fabric";
                string specsCol = isUrdu ? "تفصیلات" : "Specs";
                string qtyCol = isUrdu ? "شامل شدہ مقدار" : "Yards Added";
                string priceCol = isUrdu ? "قیمت فی میٹر/گز" : "Price per Yard";
                string supplierCol = isUrdu ? "فراہم کنندہ" : "Supplier";
                string dateCol = isUrdu ? "تاریخ آمد" : "Date Arrived";

                lblLogsTitle.Text = isUrdu ? "خریداری اور نئے اسٹاک کا حالیہ لاگ" : "Recent New Stock Purchases";
                string query = $@"
                    SELECT r.id AS [{logIdCol}], p.fabric_type AS [{fabricCol}], 
                           CONCAT(p.section, ' - ', CASE WHEN p.section='Gents' THEN CONCAT(p.fabric_material, ' (', p.color, ')') ELSE p.suit_type END) AS [{specsCol}],
                           r.quantity AS [{qtyCol}], r.purchase_price AS [{priceCol}],
                           r.supplier_name AS [{supplierCol}], r.purchase_date AS [{dateCol}]
                    FROM purchase_records r
                    JOIN products p ON r.product_id = p.id
                    WHERE p.section = @section AND r.purchase_date >= @startDate AND r.purchase_date < @endDate
                    ORDER BY r.purchase_date DESC;";

                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@section", SessionManager.ActiveShopMode);
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }

            dgvLogs.DataSource = null;
            dgvLogs.DataSource = dt;
            TranslationHelper.ApplyModernGridStyle(dgvLogs);
            dgvLogs.ScrollBars = ScrollBars.Both;

            if (dgvLogs.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvLogs.Columns)
                {
                    if (col.HeaderText.Contains("Specs") || col.HeaderText.Contains("تفصیلات"))
                    {
                        col.MinimumWidth = 150;
                    }
                    else
                    {
                        col.MinimumWidth = 85;
                    }
                }
            }

            // Format price column if we are showing Purchases
            if (!rbShowWastage.Checked)
            {
                string priceColName = isUrdu ? "قیمت فی میٹر/گز" : "Price per Yard";
                if (dgvLogs.Columns.Contains(priceColName))
                {
                    dgvLogs.Columns[priceColName]!.DefaultCellStyle.Format = "'Rs' #,##0.00";
                }
            }
        }

        public void ApplyLanguageTranslation()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            this.RightToLeft = isUrdu ? RightToLeft.Yes : RightToLeft.No;

            if (isUrdu)
            {
                lblTitle.Text = "اسٹاک آڈٹ اور ضیاع کا لاگ";
                tabReconcile.Text = "کپڑے کے اسٹاک کی پڑتال";
                tabWastage.Text = "نقصان یا چوری شدہ اسٹاک کا اندراج";

                lblLogStart.Text = "شروع تاریخ:";
                lblLogEnd.Text = "تک تاریخ:";

                // Reconciliation labels
                lblRProduct.Text = "کپڑا / سوٹ منتخب کریں: *";
                lblRSystemQty.Text = "سسٹم میں موجود میٹر/گز:";
                lblRAdjustmentQty.Text = "اصلاحی مقدار (تبدیلی): *";
                rbExtra.Text = "اضافی / سرپلس (+)";
                rbDiff.Text = "کمی / شارٹیج (-)";
                lblRNewStock.Text = "نیا متوقع اسٹاک پیش نظارہ:";
                btnReconcile.Text = "💾 اسٹاک کی سطح اپ ڈیٹ کریں";

                // Wastage labels
                lblWProduct.Text = "کپڑا / سوٹ منتخب کریں: *";
                lblWSysQty.Text = "سسٹم میں موجود میٹر/گز:";
                lblWQty.Text = "ضائع یا خراب شدہ میٹر/گز: *";
                lblWReason.Text = "خرابی یا نقصان کی وجہ: *";
                btnLogWastage.Text = "💾 نقصان / ضیاع کا اندراج کریں";

                // Log filters
                rbShowWastage.Text = "ضائع/خراب شدہ لاگ دکھائیں";
                rbShowPurchases.Text = "نئی خریداری کا لاگ دکھائیں";
            }
            else
            {
                lblTitle.Text = "Fabric Stock Check & Damage Logs";
                tabReconcile.Text = "Count Fabric Stock (Check)";
                tabWastage.Text = "Record Damaged / Stolen Stock";

                lblLogStart.Text = "From Date:";
                lblLogEnd.Text = "To Date:";

                // Reconciliation labels
                lblRProduct.Text = "Select Fabric / Suit:*";
                lblRSystemQty.Text = "Yards in System:";
                lblRAdjustmentQty.Text = "Adjustment Yards (Count):*";
                rbExtra.Text = "Extra / Surplus (+)";
                rbDiff.Text = "Diff / Shortage (-)";
                lblRNewStock.Text = "Adjusted Stock Preview:";
                btnReconcile.Text = "💾 Save Audit / Update";

                // Wastage labels
                lblWProduct.Text = "Select Fabric / Suit:*";
                lblWSysQty.Text = "Yards in System:";
                lblWQty.Text = "Damaged / Lost Yards:*";
                lblWReason.Text = "Reason for Damage/Loss:*";
                btnLogWastage.Text = "💾 Save Damage Log";

                // Log filters
                rbShowWastage.Text = "Show Stolen/Damaged Logs";
                rbShowPurchases.Text = "Show New Purchases Logs";
            }
            
            btnSearchLog.Text = isUrdu ? "🔍 تلاش کریں" : "🔍 Search";
            // Reload reasons dropdown
            int currentReasonIdx = cbWReason.SelectedIndex;
            cbWReason.Items.Clear();
            if (isUrdu)
            {
                cbWReason.Items.AddRange(new string[] { 
                    "کپڑا خراب یا پھٹ گیا ہے", 
                    "داغدار یا رنگ خراب ہو گیا ہے", 
                    "بچے ہوئے کپڑے کے ٹکڑے", 
                    "گمشدہ یا چوری شدہ کپڑا", 
                    "خراب مال سپلائر کو واپسی", 
                    "دیگر اسٹاک کا نقصان" 
                });
            }
            else
            {
                cbWReason.Items.AddRange(new string[] { 
                    "Fabric Damaged / Torn", 
                    "Stained / Dirty Color Spot", 
                    "Leftover Fabric Cut Pieces", 
                    "Missing / Stolen Fabric", 
                    "Defective Return to Supplier", 
                    "Other Stock Loss" 
                });
            }
            if (currentReasonIdx >= 0 && currentReasonIdx < cbWReason.Items.Count)
            {
                cbWReason.SelectedIndex = currentReasonIdx;
            }
            else
            {
                cbWReason.SelectedIndex = 0;
            }

            // Reload products and logs
            LoadProducts();
            LoadLogs();
        }

        private void btnSearchLog_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void tabControlAudit_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            TabPage page = tc.TabPages[e.Index];
            
            bool isSelected = tc.SelectedIndex == e.Index;
            bool isHovered = hoveredTabIndex == e.Index;
            
            System.Drawing.Color backColor;
            System.Drawing.Color textColor;
            
            if (isSelected)
            {
                backColor = System.Drawing.Color.FromArgb(88, 86, 214); // Royal purple active button
                textColor = System.Drawing.Color.White;
            }
            else if (isHovered)
            {
                backColor = System.Drawing.Color.FromArgb(235, 235, 250); // Light purple hover color
                textColor = System.Drawing.Color.FromArgb(88, 86, 214);
            }
            else
            {
                backColor = System.Drawing.Color.FromArgb(245, 245, 245); // Light neutral gray background
                textColor = System.Drawing.Color.FromArgb(70, 70, 70);
            }

            using (System.Drawing.SolidBrush backBrush = new System.Drawing.SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }
            
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            if (tc.RightToLeft == RightToLeft.Yes)
            {
                flags |= TextFormatFlags.RightToLeft;
            }
            
            using (System.Drawing.Font font = new System.Drawing.Font(tc.Font.FontFamily, 9.5F, isSelected ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular))
            {
                TextRenderer.DrawText(e.Graphics, page.Text, font, e.Bounds, textColor, flags);
            }
            
            if (isSelected)
            {
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(88, 86, 214), 3))
                {
                    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 2, e.Bounds.Right, e.Bounds.Bottom - 2);
                }
            }
            else
            {
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawRectangle(pen, e.Bounds);
                }
            }
        }

        private void tabControlAudit_MouseMove(object sender, MouseEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            int newHoveredIndex = -1;
            for (int i = 0; i < tc.TabCount; i++)
            {
                if (tc.GetTabRect(i).Contains(e.Location))
                {
                    newHoveredIndex = i;
                    break;
                }
            }
            
            if (newHoveredIndex != hoveredTabIndex)
            {
                hoveredTabIndex = newHoveredIndex;
                tc.Invalidate();
            }
        }

        private void tabControlAudit_MouseLeave(object sender, EventArgs e)
        {
            if (hoveredTabIndex != -1)
            {
                hoveredTabIndex = -1;
                ((TabControl)sender).Invalidate();
            }
        }
    }
}
