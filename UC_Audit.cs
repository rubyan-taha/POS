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
                lblRSystemQtyVal.Text = $"{selectedRProduct.CurrentStock:N2} yards";
                UpdateReconciliationPreview();
            }
        }

        private void cbWProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedWProduct = cbWProduct.SelectedItem as Product;
            if (selectedWProduct != null)
            {
                lblWSysQtyVal.Text = $"{selectedWProduct.CurrentStock:N2} yards";
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
            
            if (decimal.TryParse(txtRAdjustmentQty.Text.Trim(), out decimal adjustment) && adjustment >= 0)
            {
                decimal newStock = currentStock;
                if (rbExtra.Checked)
                {
                    newStock = currentStock + adjustment;
                    lblRNewStockVal.Text = $"{newStock:N2} yards";
                    lblRNewStockVal.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80); // Surplus green
                }
                else if (rbDiff.Checked)
                {
                    newStock = currentStock - adjustment;
                    if (newStock < 0) newStock = 0;
                    lblRNewStockVal.Text = $"{newStock:N2} yards";
                    lblRNewStockVal.ForeColor = System.Drawing.Color.FromArgb(219, 68, 85); // Shortage red
                }
                else
                {
                    lblRNewStockVal.Text = $"{newStock:N2} yards";
                    lblRNewStockVal.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                lblRNewStockVal.Text = $"{currentStock:N2} yards";
                lblRNewStockVal.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void btnReconcile_Click(object sender, EventArgs e)
        {
            if (selectedRProduct == null) return;

            if (!decimal.TryParse(txtRAdjustmentQty.Text.Trim(), out decimal adjustment) || adjustment < 0)
            {
                MessageBox.Show("Please enter a valid adjustment quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (adjustment == 0)
            {
                MessageBox.Show("Adjustment quantity is zero. No change will be made.", "No Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!rbExtra.Checked && !rbDiff.Checked)
            {
                MessageBox.Show("Please select whether this is a Surplus (+) or Shortage (-).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show($"Adjustment exceeds current stock level ({selectedRProduct.CurrentStock:N2} yards). Adjusted stock cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                diff = -adjustment;
            }

            var confirm = MessageBox.Show($"Apply adjustment of {(rbExtra.Checked ? "+" : "-")}{adjustment:N2} yards to '{selectedRProduct.ShortName}'?\nNew stock level will be {newStock:N2} yards.", "Confirm Adjustment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        MessageBox.Show("Stock adjusted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtRAdjustmentQty.Clear();
                        rbExtra.Checked = false;
                        rbDiff.Checked = false;
                        LoadProducts();
                        LoadLogs();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Failed to save stock update:\n{ex.Message}", "Error Updating Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLogWastage_Click(object sender, EventArgs e)
        {
            if (selectedWProduct == null) return;

            if (!decimal.TryParse(txtWQty.Text.Trim(), out decimal qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid number of yards removed.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (qty > selectedWProduct.CurrentStock)
            {
                MessageBox.Show($"The yards removed ({qty:N2}) cannot be more than what we have in system ({selectedWProduct.CurrentStock:N2}).", "Not Enough Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reason = cbWReason.Text;
            var confirm = MessageBox.Show($"Remove {qty:N2} yards from '{selectedWProduct.ShortName}' because it is '{reason}'?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Damaged/Lost stock has been recorded and removed from system.", "Stock Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtWQty.Clear();
                        LoadProducts();
                        LoadLogs();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Failed to record damaged stock:\n{ex.Message}", "Error Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
