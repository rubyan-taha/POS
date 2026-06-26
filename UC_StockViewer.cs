using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using GarmentShopPos.Models;

namespace GarmentShopPos
{
    public partial class UC_StockViewer : UserControl
    {
        private List<Product> stockList = new List<Product>();

        public UC_StockViewer()
        {
            InitializeComponent();
        }

        private void UC_StockViewer_Load(object sender, EventArgs e)
        {
            ApplyLanguageTranslation();
        }

        public void RefreshData()
        {
            LoadStock();
        }

        public void LoadStock()
        {
            stockList.Clear();
            string query = "SELECT * FROM products WHERE is_deleted = 0 AND section = @section";

            // Add conditions dynamically
            List<string> conditions = new List<string>();

            // Stock Level
            if (cbStockLevelFilter.SelectedIndex == 1)
            {
                conditions.Add("current_stock > reorder_point");
            }
            else if (cbStockLevelFilter.SelectedIndex == 2)
            {
                conditions.Add("current_stock <= reorder_point");
            }

            // Search Text
            string search = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                conditions.Add("(barcode LIKE @search OR fabric_type LIKE @search OR fabric_material LIKE @search OR color LIKE @search OR suit_type LIKE @search OR print_type LIKE @search OR embroidery_type LIKE @search)");
            }

            // Append conditions to query
            if (conditions.Count > 0)
            {
                query += " AND " + string.Join(" AND ", conditions);
            }

            query += " ORDER BY fabric_type";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@section", SessionManager.ActiveShopMode);
                    if (!string.IsNullOrEmpty(search))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{search}%");
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prod = new Product
                            {
                                Id = reader.GetInt32("id"),
                                Barcode = reader.GetString("barcode"),
                                Section = reader.GetString("section"),
                                FabricType = reader.GetString("fabric_type"),
                                FabricMaterial = reader.IsDBNull(reader.GetOrdinal("fabric_material")) ? null : reader.GetString("fabric_material"),
                                Color = reader.IsDBNull(reader.GetOrdinal("color")) ? null : reader.GetString("color"),
                                IsPrinted = reader.GetByte("is_printed") == 1,
                                PrintType = reader.IsDBNull(reader.GetOrdinal("print_type")) ? null : reader.GetString("print_type"),
                                IsEmbroidered = reader.GetByte("is_embroidered") == 1,
                                EmbroideryType = reader.IsDBNull(reader.GetOrdinal("embroidery_type")) ? null : reader.GetString("embroidery_type"),
                                EmbroideryExtraCharge = reader.GetDecimal("embroidery_extra_charge"),
                                SuitType = reader.IsDBNull(reader.GetOrdinal("suit_type")) ? null : reader.GetString("suit_type"),
                                WholesalePrice = reader.GetDecimal("wholesale_price"),
                                RetailPrice = reader.GetDecimal("retail_price"),
                                CurrentStock = reader.GetDecimal("current_stock"),
                                ReorderPoint = reader.GetDecimal("reorder_point"),
                                IsDeleted = reader.GetByte("is_deleted") == 1
                            };
                            stockList.Add(prod);
                        }
                    }
                }
            }

            dgvStock.DataSource = null;
            dgvStock.DataSource = stockList;

            ConfigureGridColumns();
            CalculateStats();
        }

        private void ConfigureGridColumns()
        {
            TranslationHelper.ApplyModernGridStyle(dgvStock);
            if (dgvStock.Columns.Count > 0)
            {
                // Disable automatic filling of all columns equally
                dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvStock.ScrollBars = ScrollBars.Both;

                // Hide backing fields
                foreach (DataGridViewColumn col in dgvStock.Columns)
                {
                    col.Visible = false;
                }

                bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

                // Show only key columns with explicit minimum widths to prevent header or data truncation
                dgvStock.Columns["Id"].Visible = true;
                dgvStock.Columns["Id"].HeaderText = isUrdu ? "آئی ڈی" : "ID";
                dgvStock.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvStock.Columns["Id"].Width = 55;
                dgvStock.Columns["Id"].MinimumWidth = 45;

                dgvStock.Columns["Barcode"].Visible = true;
                dgvStock.Columns["Barcode"].HeaderText = isUrdu ? "بارکوڈ" : "Barcode";
                dgvStock.Columns["Barcode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvStock.Columns["Barcode"].Width = 110;
                dgvStock.Columns["Barcode"].MinimumWidth = 95;

                dgvStock.Columns["Section"].Visible = true;
                dgvStock.Columns["Section"].HeaderText = isUrdu ? "سیکشن" : "Section";
                dgvStock.Columns["Section"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvStock.Columns["Section"].Width = 85;
                dgvStock.Columns["Section"].MinimumWidth = 75;

                dgvStock.Columns["WholesalePrice"].Visible = true;
                dgvStock.Columns["WholesalePrice"].HeaderText = isUrdu ? "تھوک قیمت" : "Cost Price";
                dgvStock.Columns["WholesalePrice"].DefaultCellStyle.Format = "'Rs' #,##0.00";
                dgvStock.Columns["WholesalePrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvStock.Columns["WholesalePrice"].Width = 105;
                dgvStock.Columns["WholesalePrice"].MinimumWidth = 95;

                dgvStock.Columns["RetailPrice"].Visible = true;
                dgvStock.Columns["RetailPrice"].HeaderText = isUrdu ? "پرچون قیمت" : "Retail Price";
                dgvStock.Columns["RetailPrice"].DefaultCellStyle.Format = "'Rs' #,##0.00";
                dgvStock.Columns["RetailPrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvStock.Columns["RetailPrice"].Width = 105;
                dgvStock.Columns["RetailPrice"].MinimumWidth = 95;

                dgvStock.Columns["CurrentStock"].Visible = true;
                dgvStock.Columns["CurrentStock"].HeaderText = isUrdu ? "موجودہ اسٹاک" : "Stock Qty";
                dgvStock.Columns["CurrentStock"].DefaultCellStyle.Format = "N2";
                dgvStock.Columns["CurrentStock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvStock.Columns["CurrentStock"].Width = 90;
                dgvStock.Columns["CurrentStock"].MinimumWidth = 80;

                dgvStock.Columns["ReorderPoint"].Visible = true;
                dgvStock.Columns["ReorderPoint"].HeaderText = isUrdu ? "ری آرڈر حد" : "Reorder Limit";
                dgvStock.Columns["ReorderPoint"].DefaultCellStyle.Format = "N2";
                dgvStock.Columns["ReorderPoint"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvStock.Columns["ReorderPoint"].Width = 95;
                dgvStock.Columns["ReorderPoint"].MinimumWidth = 85;

                // Let the description column expand to fill the rest of the available grid width
                dgvStock.Columns["ShortName"].Visible = true;
                dgvStock.Columns["ShortName"].HeaderText = isUrdu ? "تفصیل / کپڑا" : "Fabric Details / Specs";
                dgvStock.Columns["ShortName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvStock.Columns["ShortName"].MinimumWidth = 160;
            }
        }

        private void CalculateStats()
        {
            int totalTypes = 0;
            decimal totalStockVal = 0.00m;
            int lowStockCount = 0;

            // Run stats queries
            string query = "SELECT COUNT(*), SUM(current_stock), SUM(CASE WHEN current_stock <= reorder_point THEN 1 ELSE 0 END) FROM products WHERE is_deleted = 0 AND section = @section";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@section", SessionManager.ActiveShopMode);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalTypes = reader.GetInt32(0);
                            totalStockVal = reader.IsDBNull(1) ? 0.00m : reader.GetDecimal(1);
                            lowStockCount = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        }
                    }
                }
            }

            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            if (isUrdu)
            {
                lblTotalProducts.Text = $"کل کپڑوں کی اقسام: {totalTypes}";
                lblTotalStock.Text = $"کل اسٹاک کی لمبائی: {totalStockVal:N2} میٹر/گز";
                lblLowStockCount.Text = $"⚠️ کم اسٹاک انتباہ: {lowStockCount}";
            }
            else
            {
                lblTotalProducts.Text = $"Total Fabric Types: {totalTypes}";
                lblTotalStock.Text = $"Total Stock Length: {totalStockVal:N2} yards/meters";
                lblLowStockCount.Text = $"⚠️ Low Stock Alerts: {lowStockCount}";
            }

            if (lowStockCount > 0)
            {
                lblLowStockCount.ForeColor = Color.FromArgb(219, 68, 85); // High alert red
            }
            else
            {
                lblLowStockCount.ForeColor = Color.FromArgb(76, 175, 80); // Success green
            }
        }

        private void FilterControls_Changed(object sender, EventArgs e)
        {
            LoadStock();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStock();
        }

        private void dgvStock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvStock.Rows[e.RowIndex].DataBoundItem is Product prod)
            {
                if (prod.CurrentStock <= prod.ReorderPoint)
                {
                    // Highlight low stock rows
                    dgvStock.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230); // Soft red
                    dgvStock.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(190, 0, 0); // Dark red
                }
                else
                {
                    // Reset to default
                    dgvStock.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    dgvStock.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }

                // Dynamic formatting for CurrentStock and ReorderPoint if they are box items
                bool isBox = (prod.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             (prod.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;

                if (dgvStock.Columns[e.ColumnIndex].Name == "CurrentStock" || dgvStock.Columns[e.ColumnIndex].Name == "ReorderPoint")
                {
                    if (isBox)
                    {
                        e.Value = string.Format("{0:N0}", e.Value);
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = string.Format("{0:N2}", e.Value);
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        public void ApplyLanguageTranslation()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            
            this.RightToLeft = isUrdu ? RightToLeft.Yes : RightToLeft.No;

            if (isUrdu)
            {
                lblTitle.Text = "اسٹاک انوینٹری لسٹ";
                txtSearch.PlaceholderText = "🔍 تلاش کریں: کپڑا، بارکوڈ، رنگ...";
                btnUpdateProduct.Text = "✏️ اپڈیٹ / حذف کریں";
            }
            else
            {
                lblTitle.Text = "Inventory Stock Viewer";
                txtSearch.PlaceholderText = "🔍 Search by fabric, material, specs...";
                btnUpdateProduct.Text = "✏️ Update / Delete Item";
            }

            int currentIdx = cbStockLevelFilter.SelectedIndex;
            cbStockLevelFilter.Items.Clear();
            if (isUrdu)
            {
                cbStockLevelFilter.Items.AddRange(new string[] { "تمام اسٹاک", "عام اسٹاک", "کم اسٹاک (وارننگ)" });
            }
            else
            {
                cbStockLevelFilter.Items.AddRange(new string[] { "All Stock Levels", "Normal Stock", "Low Stock (Alerts)" });
            }

            if (currentIdx >= 0 && currentIdx < cbStockLevelFilter.Items.Count)
            {
                cbStockLevelFilter.SelectedIndex = currentIdx;
            }
            else
            {
                cbStockLevelFilter.SelectedIndex = 0;
            }

            LoadStock();
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (dgvStock.SelectedRows.Count > 0)
            {
                Product selectedProduct = dgvStock.SelectedRows[0].DataBoundItem as Product;
                if (selectedProduct != null)
                {
                    using (var form = new EditProductForm(selectedProduct))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            LoadStock(); // Refresh stock list after saving or deleting
                        }
                    }
                }
            }
            else
            {
                bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
                MessageBox.Show(
                    isUrdu ? "ترمیم یا حذف کرنے کے لیے براہ کرم فہرست سے ایک پروڈکٹ منتخب کریں۔" : "Please select a product from the list to update or delete.",
                    isUrdu ? "پروڈکٹ منتخب نہیں کی گئی" : "No Product Selected", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}
