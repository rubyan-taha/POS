using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using GarmentShopPos.Models;

namespace GarmentShopPos
{
    public partial class UC_POS : UserControl
    {
        private string activeSection = "Gents"; // Default section
        private List<Product> productList = new List<Product>();
        private Product? selectedProduct;

        // Cart Binding List for DataGridView
        private BindingList<CartItemViewModel> cartList = new BindingList<CartItemViewModel>();

        // For receipt printing
        private Order? lastCompletedOrder;
        private List<OrderItem>? lastCompletedOrderItems;

        public UC_POS()
        {
            InitializeComponent();

            // Add scroll margin to ensure the Add to Cart button isn't cut off on small screens
            panelMiddle.AutoScrollMargin = new Size(0, 40);
        }

        private void UC_POS_Load(object sender, EventArgs e)
        {
            activeSection = SessionManager.ActiveShopMode;
            btnGentsSection.Visible = false;
            btnLadiesSection.Visible = false;

            SetupCartGrid();
            SetupDropdowns();
            LoadProducts();
            TranslationHelper.ApplyModernGridStyle(dgvProducts);
            ResetPOS();
            ApplyLanguageTranslation();
            this.ActiveControl = txtBarcodeScan;
        }

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();
            dgvCart.ScrollBars = ScrollBars.Both;

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Fabric", Width = 90, MinimumWidth = 80 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Specs", HeaderText = "Specs", Width = 110, MinimumWidth = 100 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty/Mtr", Width = 60, MinimumWidth = 55 });

            var colPrice = new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Price", Width = 50, MinimumWidth = 45 };
            colPrice.DefaultCellStyle.Format = "'Rs' #,##0.00";
            dgvCart.Columns.Add(colPrice);

            var colTotal = new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Total", Width = 60, MinimumWidth = 55 };
            colTotal.DefaultCellStyle.Format = "'Rs' #,##0.00";
            dgvCart.Columns.Add(colTotal);

            dgvCart.DataSource = cartList;
            TranslationHelper.ApplyModernGridStyle(dgvCart);
        }

        private void SetupDropdowns()
        {
            cbPrintType.FlatStyle = FlatStyle.Flat;
            cbPrintType.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbEmbroideryType.FlatStyle = FlatStyle.Flat;
            cbEmbroideryType.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbPaymentMethod.FlatStyle = FlatStyle.Flat;
            cbPaymentMethod.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);

            // Print Types
            cbPrintType.Items.Clear();
            cbPrintType.Items.AddRange(new string[] { "Digital", "Block", "Screen" });
            cbPrintType.SelectedIndex = 0;

            // Embroidery Types
            cbEmbroideryType.Items.Clear();
            cbEmbroideryType.Items.AddRange(new string[] { "Hand work", "Machine", "Chikan" });
            cbEmbroideryType.SelectedIndex = 0;

            // Payment Methods
            cbPaymentMethod.Items.Clear();
            cbPaymentMethod.Items.AddRange(new string[] { "Cash", "Card", "Mobile Banking" });
            cbPaymentMethod.SelectedIndex = 0;
        }

        public void RefreshData()
        {
            LoadProducts();
        }

        public void LoadProducts()
        {
            productList.Clear();
            string query = @"SELECT * FROM products 
                             WHERE section = @section AND is_deleted = 0 
                             AND (fabric_type LIKE @search OR fabric_material LIKE @search OR color LIKE @search)";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@section", activeSection);
                    cmd.Parameters.AddWithValue("@search", $"%{txtSearch.Text.Trim()}%");

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
                            productList.Add(prod);
                        }
                    }
                }
            }

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = productList;

            // Hide unnecessary columns for simpler view in POS
            if (dgvProducts.Columns.Count > 0)
            {
                dgvProducts.ScrollBars = ScrollBars.Both;
                foreach (DataGridViewColumn col in dgvProducts.Columns)
                {
                    if (col.Name != "ShortName" && col.Name != "RetailPrice" && col.Name != "CurrentStock")
                    {
                        col.Visible = false;
                    }
                }
                bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
                dgvProducts.Columns["ShortName"].HeaderText = isUrdu ? "مصنوعات کی تفصیل" : "Product Details";
                dgvProducts.Columns["ShortName"].Width = 140;
                dgvProducts.Columns["ShortName"].MinimumWidth = 110;
                dgvProducts.Columns["RetailPrice"].HeaderText = isUrdu ? "قیمت/میٹر" : "Price/Meter";
                dgvProducts.Columns["RetailPrice"].DefaultCellStyle.Format = "'Rs' #,##0.00";
                dgvProducts.Columns["RetailPrice"].Width = 70;
                dgvProducts.Columns["RetailPrice"].MinimumWidth = 65;
                dgvProducts.Columns["CurrentStock"].HeaderText = isUrdu ? "اسٹاک" : "Stock";
                dgvProducts.Columns["CurrentStock"].Width = 70;
                dgvProducts.Columns["CurrentStock"].MinimumWidth = 60;
            }
        }

        private void ResetPOS()
        {
            cartList.Clear();
            txtDiscount.Text = "0";
            txtReceived.Text = "0";
            lblChangeValue.Text = "Rs 0.00";
            cbPaymentMethod.SelectedIndex = 0;
            if (txtBarcodeScan != null)
            {
                txtBarcodeScan.Clear();
                txtBarcodeScan.Focus();
            }
            UpdateTotals();
        }

        private void btnGentsSection_Click(object sender, EventArgs e)
        {
            activeSection = "Gents";
            btnGentsSection.BackColor = Color.FromArgb(88, 86, 214);
            btnGentsSection.ForeColor = Color.White;
            btnGentsSection.FlatAppearance.BorderSize = 0;

            btnLadiesSection.BackColor = Color.White;
            btnLadiesSection.ForeColor = Color.FromArgb(60, 60, 60);
            btnLadiesSection.FlatAppearance.BorderSize = 1;

            gbLadiesOptions.Visible = false;
            LoadProducts();
        }

        private void btnLadiesSection_Click(object sender, EventArgs e)
        {
            activeSection = "Ladies";
            btnLadiesSection.BackColor = Color.FromArgb(88, 86, 214);
            btnLadiesSection.ForeColor = Color.White;
            btnLadiesSection.FlatAppearance.BorderSize = 0;

            btnGentsSection.BackColor = Color.White;
            btnGentsSection.ForeColor = Color.FromArgb(60, 60, 60);
            btnGentsSection.FlatAppearance.BorderSize = 1;

            gbLadiesOptions.Visible = true;
            LoadProducts();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                selectedProduct = dgvProducts.SelectedRows[0].DataBoundItem as Product;
                if (selectedProduct != null)
                {
                    UpdateSelectedProductDetails();
                    
                    if (selectedProduct.Section == "Ladies")
                    {
                        // Set checkboxes based on db details
                        chkPrinted.Checked = selectedProduct.IsPrinted;
                        if (selectedProduct.IsPrinted && !string.IsNullOrEmpty(selectedProduct.PrintType))
                        {
                            cbPrintType.SelectedItem = selectedProduct.PrintType;
                        }
                        chkEmbroidered.Checked = selectedProduct.IsEmbroidered;
                        if (selectedProduct.IsEmbroidered && !string.IsNullOrEmpty(selectedProduct.EmbroideryType))
                        {
                            cbEmbroideryType.SelectedItem = selectedProduct.EmbroideryType;
                            txtEmbCharge.Text = selectedProduct.EmbroideryExtraCharge.ToString("F2");
                        }
                        else
                        {
                            txtEmbCharge.Text = "0.00";
                        }
                    }
                    numQty.Value = GetDefaultQuantityForProduct(selectedProduct);
                }
            }
            else
            {
                selectedProduct = null;
                UpdateSelectedProductDetails();
            }
        }

        private void chkPrinted_CheckedChanged(object sender, EventArgs e)
        {
            cbPrintType.Enabled = chkPrinted.Checked;
        }

        private void chkEmbroidered_CheckedChanged(object sender, EventArgs e)
        {
            cbEmbroideryType.Enabled = chkEmbroidered.Checked;
            txtEmbCharge.Enabled = chkEmbroidered.Checked;
            if (!chkEmbroidered.Checked)
            {
                txtEmbCharge.Text = "0.00";
            }
            else if (selectedProduct != null && selectedProduct.IsEmbroidered)
            {
                txtEmbCharge.Text = selectedProduct.EmbroideryExtraCharge.ToString("F2");
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal qty = numQty.Value;
            if (qty <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (qty > selectedProduct.CurrentStock)
            {
                string msg = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase)
                    ? $"ناکافی اسٹاک۔ دستیاب اسٹاک صرف {selectedProduct.CurrentStock:N2} میٹر ہے۔"
                    : $"Insufficient stock. Available stock is only {selectedProduct.CurrentStock:N2} meters.";
                MessageBox.Show(msg, string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase) ? "کم اسٹاک" : "Low Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate item price
            decimal unitPrice = selectedProduct.RetailPrice;
            decimal embroideryCharge = 0.00m;
            string specs = "";

            if (selectedProduct.Section == "Gents")
            {
                specs = $"{selectedProduct.FabricMaterial} ({selectedProduct.Color})";
            }
            else
            {
                string printStr = chkPrinted.Checked ? $"{cbPrintType.Text} Print" : "Plain";
                string embStr = "";
                if (chkEmbroidered.Checked)
                {
                    embStr = $", {cbEmbroideryType.Text} Embroidery";
                    if (!decimal.TryParse(txtEmbCharge.Text.Trim(), out embroideryCharge))
                    {
                        MessageBox.Show("Please enter a valid embroidery charge.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                specs = $"{selectedProduct.SuitType} - {printStr}{embStr}";
            }

            decimal totalAmount = (unitPrice * qty) + embroideryCharge;

            // Check if item already exists in cart with same configurations
            bool found = false;
            foreach (var item in cartList)
            {
                if (item.ProductId == selectedProduct.Id && item.Specs == specs)
                {
                    if (item.Quantity + qty > selectedProduct.CurrentStock)
                    {
                        MessageBox.Show($"Cannot add more. Cumulative cart quantity ({item.Quantity + qty:N2}) exceeds available stock ({selectedProduct.CurrentStock:N2}).", "Stock Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    item.Quantity += qty;
                    item.EmbroideryCharge += embroideryCharge; // Add up embroidery charges if applicable
                    item.Total = (item.Price * item.Quantity) + item.EmbroideryCharge;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                cartList.Add(new CartItemViewModel
                {
                    ProductId = selectedProduct.Id,
                    Name = selectedProduct.FabricType,
                    Specs = specs,
                    Quantity = qty,
                    Price = unitPrice,
                    EmbroideryCharge = embroideryCharge,
                    Total = totalAmount,
                    Section = selectedProduct.Section,
                    IsPrinted = chkPrinted.Checked,
                    PrintType = chkPrinted.Checked ? cbPrintType.Text : null,
                    IsEmbroidered = chkEmbroidered.Checked,
                    EmbroideryType = chkEmbroidered.Checked ? cbEmbroideryType.Text : null,
                    IsBox = (selectedProduct.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            (selectedProduct.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0
                });
            }

            dgvCart.Refresh();
            UpdateTotals();
        }

        private void btnRemoveCartItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                var selectedItem = dgvCart.SelectedRows[0].DataBoundItem as CartItemViewModel;
                if (selectedItem != null)
                {
                    cartList.Remove(selectedItem);
                    UpdateTotals();
                }
            }
            else
            {
                MessageBox.Show("Please select an item in the cart to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateTotals()
        {
            decimal subtotal = 0.00m;
            foreach (var item in cartList)
            {
                subtotal += item.Total;
            }

            decimal discount = 0.00m;
            decimal.TryParse(txtDiscount.Text.Trim(), out discount);

            decimal netTotal = subtotal - discount;
            if (netTotal < 0) netTotal = 0;

            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            if (isUrdu)
            {
                lblSubtotal.Text = $"کل رقم: Rs {subtotal:N2}";
                lblTotal.Text = $"قابل ادائیگی رقم: Rs {netTotal:N2}";
            }
            else
            {
                lblSubtotal.Text = $"Subtotal: Rs {subtotal:N2}";
                lblTotal.Text = $"Net Total: Rs {netTotal:N2}";
            }

            UpdateChangeCalculation();
            UpdateRealtimeReceipt();
        }

        private void UpdateChangeCalculation()
        {
            decimal netTotal = GetNetTotal();
            decimal received = 0.00m;
            decimal.TryParse(txtReceived.Text.Trim(), out received);

            decimal change = received - netTotal;
            if (change < 0) change = 0;

            lblChangeValue.Text = $"Rs {change:N2}";
        }

        private decimal GetNetTotal()
        {
            decimal subtotal = 0.00m;
            foreach (var item in cartList)
            {
                subtotal += item.Total;
            }

            decimal discount = 0.00m;
            decimal.TryParse(txtDiscount.Text.Trim(), out discount);

            decimal netTotal = subtotal - discount;
            return netTotal < 0 ? 0 : netTotal;
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void txtReceived_TextChanged(object sender, EventArgs e)
        {
            UpdateChangeCalculation();
            UpdateRealtimeReceipt();
        }

        private void cbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblReceived.Visible = true;
            txtReceived.Visible = true;
            lblChange.Visible = true;
            lblChangeValue.Visible = true;
        }

        private void ProcessCheckout(bool shouldPrint)
        {
            if (cartList.Count == 0)
            {
                MessageBox.Show("Cart is empty.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal netTotal = GetNetTotal();
            string paymentMethod = cbPaymentMethod.Text;

            // 1. Validation for cash received
            decimal cashReceived = 0.00m;
            if (paymentMethod == "Cash")
            {
                decimal.TryParse(txtReceived.Text.Trim(), out cashReceived);
                if (cashReceived < netTotal)
                {
                    var result = MessageBox.Show($"Received amount (Rs {cashReceived:N2}) is less than Net Total (Rs {netTotal:N2}). Proceed anyway?", "Underpayment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No) return;
                }
            }
            else if (paymentMethod == "Card" || paymentMethod == "Mobile Banking")
            {
                cashReceived = netTotal; // Cards / Mobile pay exact total
            }

            // 2. Determine timezone-adjusted local time
            DateTime orderDate = DateTime.Now;
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.ShopTimeZone);
                orderDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            }
            catch
            {
                orderDate = DateTime.Now;
            }

            // 3. Process Transaction
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // A. Double check stock in transaction to prevent race conditions
                        foreach (var item in cartList)
                        {
                            using (var checkCmd = new SqlCommand("SELECT current_stock FROM products WITH (UPDLOCK, ROWLOCK) WHERE id = @id", conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@id", item.ProductId);
                                decimal stock = (decimal)checkCmd.ExecuteScalar();
                                if (stock < item.Quantity)
                                {
                                    throw new Exception($"Stock depleted for product ID {item.ProductId}. Available: {stock}. Cart needs: {item.Quantity}.");
                                }
                            }
                        }

                        // B. Create Order
                        string receiptNo = $"REC-{orderDate:yyyyMMddHHmmss}-{new Random().Next(100, 999)}";
                        int salesmanId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Id : 1;
                        decimal subtotal = 0.00m;
                        foreach (var item in cartList) subtotal += item.Total;
                        decimal discount = 0.00m;
                        decimal.TryParse(txtDiscount.Text.Trim(), out discount);
                        decimal changeReturned = (paymentMethod == "Cash") ? Math.Max(0, cashReceived - netTotal) : 0.00m;

                        string insertOrderQuery = @"
                            INSERT INTO orders (receipt_number, customer_id, salesman_id, order_date, subtotal, discount, total_amount, payment_method, amount_paid, change_returned, is_refunded)
                            VALUES (@receiptNo, @customerId, @salesmanId, @orderDate, @subtotal, @discount, @total, @payment, @paid, @change, 0);
                            SELECT SCOPE_IDENTITY();";

                        int orderId = 0;
                        using (var cmd = new SqlCommand(insertOrderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@receiptNo", receiptNo);
                            cmd.Parameters.AddWithValue("@customerId", DBNull.Value);
                            cmd.Parameters.AddWithValue("@salesmanId", salesmanId);
                            cmd.Parameters.AddWithValue("@orderDate", orderDate);
                            cmd.Parameters.AddWithValue("@subtotal", subtotal);
                            cmd.Parameters.AddWithValue("@discount", discount);
                            cmd.Parameters.AddWithValue("@total", netTotal);
                            cmd.Parameters.AddWithValue("@payment", paymentMethod);
                            cmd.Parameters.AddWithValue("@paid", cashReceived);
                            cmd.Parameters.AddWithValue("@change", changeReturned);

                            orderId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // C. Create Order Items & Update Stock
                        lastCompletedOrderItems = new List<OrderItem>();
                        foreach (var item in cartList)
                        {
                            string insertItemQuery = @"
                                INSERT INTO order_items (order_id, product_id, quantity, unit_price, is_printed, print_type, is_embroidered, embroidery_type, embroidery_extra_charge, total_item_amount)
                                VALUES (@orderId, @productId, @qty, @price, @isPrinted, @printType, @isEmb, @embType, @embCharge, @total);";

                            using (var cmd = new SqlCommand(insertItemQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@orderId", orderId);
                                cmd.Parameters.AddWithValue("@productId", item.ProductId);
                                cmd.Parameters.AddWithValue("@qty", item.Quantity);
                                cmd.Parameters.AddWithValue("@price", item.Price);
                                cmd.Parameters.AddWithValue("@isPrinted", item.IsPrinted ? 1 : 0);
                                cmd.Parameters.AddWithValue("@printType", (object?)item.PrintType ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@isEmb", item.IsEmbroidered ? 1 : 0);
                                cmd.Parameters.AddWithValue("@embType", (object?)item.EmbroideryType ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@embCharge", item.EmbroideryCharge);
                                cmd.Parameters.AddWithValue("@total", item.Total);

                                cmd.ExecuteNonQuery();
                            }

                            // Update Stock
                            string updateStockQuery = "UPDATE products SET current_stock = current_stock - @qty WHERE id = @id";
                            using (var cmd = new SqlCommand(updateStockQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@qty", item.Quantity);
                                cmd.Parameters.AddWithValue("@id", item.ProductId);
                                cmd.ExecuteNonQuery();
                            }

                            lastCompletedOrderItems.Add(new OrderItem
                            {
                                ProductId = item.ProductId,
                                FabricType = item.Name,
                                Section = item.Section,
                                Quantity = item.Quantity,
                                UnitPrice = item.Price,
                                IsPrinted = item.IsPrinted,
                                PrintType = item.PrintType,
                                IsEmbroidered = item.IsEmbroidered,
                                EmbroideryType = item.EmbroideryType,
                                EmbroideryExtraCharge = item.EmbroideryCharge,
                                TotalItemAmount = item.Total
                            });
                        }

                        transaction.Commit();

                        // Fetch order details for receipt
                        lastCompletedOrder = new Order
                        {
                            Id = orderId,
                            ReceiptNumber = receiptNo,
                            CustomerId = null,
                            CustomerName = "Walk-in Client",
                            CustomerPhone = string.Empty,
                            SalesmanId = salesmanId,
                            SalesmanName = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.FullName : "Salesman",
                            OrderDate = orderDate,
                            Subtotal = subtotal,
                            Discount = discount,
                            TotalAmount = netTotal,
                            PaymentMethod = paymentMethod,
                            AmountPaid = cashReceived,
                            ChangeReturned = changeReturned
                        };

                        MessageBox.Show("Transaction saved successfully!", "Checkout Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Trigger Receipt Printing Preview conditionally
                        if (shouldPrint)
                        {
                            TriggerReceiptPrint();
                        }

                        // Reload Products (to reflect new stock levels) and reset
                        LoadProducts();
                        ResetPOS();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Checkout failed:\n{ex.Message}", "Database Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtBarcodeScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ScanBarcode(txtBarcodeScan.Text.Trim());
            }
        }

        private void ScanBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)) return;

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    // Search strictly inside the active section
                    string query = "SELECT * FROM products WHERE barcode = @barcode AND section = @section AND is_deleted = 0";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@barcode", barcode);
                        cmd.Parameters.AddWithValue("@section", activeSection);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
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

                                selectedProduct = prod;
                                                                // Show selected product details in middle panel
                                 lblSelectedProduct.Text = selectedProduct.ShortName;
                                 if (string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase))
                                 {
                                     lblProductDetails.Text = $"قیمت: Rs {selectedProduct.RetailPrice:N2}/میٹر\nاسٹاک: {selectedProduct.CurrentStock:N2} میٹر";
                                 }
                                 else
                                 {
                                     lblProductDetails.Text = $"Price: Rs {selectedProduct.RetailPrice:N2}/meter\nStock: {selectedProduct.CurrentStock:N2} m";
                                 }
                                
                                if (selectedProduct.Section == "Ladies")
                                {
                                    gbLadiesOptions.Visible = true;
                                    chkPrinted.Checked = selectedProduct.IsPrinted;
                                    if (selectedProduct.IsPrinted)
                                    {
                                        cbPrintType.Text = selectedProduct.PrintType ?? "Digital";
                                    }
                                    chkEmbroidered.Checked = selectedProduct.IsEmbroidered;
                                    if (selectedProduct.IsEmbroidered)
                                    {
                                        cbEmbroideryType.Text = selectedProduct.EmbroideryType ?? "Machine";
                                        txtEmbCharge.Text = selectedProduct.EmbroideryExtraCharge.ToString("F2");
                                    }
                                }
                                else
                                {
                                    gbLadiesOptions.Visible = false;
                                }

                                 // Set default scanned quantity using helper
                                 numQty.Value = GetDefaultQuantityForProduct(selectedProduct);

                                // Simulate adding to cart
                                btnAddToCart.PerformClick();
                            }
                            else
                            {
                                MessageBox.Show($"Product with barcode '{barcode}' does not exist in this section.", "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Barcode scanning error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtBarcodeScan.Clear();
                txtBarcodeScan.Focus();
            }
        }

        private void btnDecrementQty_Click(object sender, EventArgs e)
        {
            CartItemViewModel? targetItem = null;

            // 1. First, check if selectedProduct is set and exists in the cart
            if (selectedProduct != null)
            {
                foreach (var item in cartList)
                {
                    if (item.ProductId == selectedProduct.Id)
                    {
                        targetItem = item;
                        break;
                    }
                }
            }

            // 2. If not found or selectedProduct is null, fall back to selected row in cart grid
            if (targetItem == null && dgvCart.SelectedRows.Count > 0)
            {
                targetItem = dgvCart.SelectedRows[0].DataBoundItem as CartItemViewModel;
            }

            if (targetItem != null)
            {
                // Decrement quantity by 1 yard/meter
                decimal decrementVal = 1.00m;
                if (targetItem.Quantity <= decrementVal)
                {
                    // If quantity is 1 or less, remove it completely
                    cartList.Remove(targetItem);
                }
                else
                {
                    targetItem.Quantity -= decrementVal;
                    targetItem.Total = (targetItem.Price * targetItem.Quantity) + targetItem.EmbroideryCharge;
                }
                
                dgvCart.Refresh();
                UpdateTotals();
            }
            else
            {
                MessageBox.Show("Please select a product or a cart item to decrement.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            ProcessCheckout(false);
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            ProcessCheckout(true);
        }

        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear this sale?", "Clear Sale", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ResetPOS();
            }
        }

        private void UpdateRealtimeReceipt()
        {
            if (rtbReceipt == null) return;
            
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            if (isUrdu)
            {
                sb.AppendLine($"      👗 {SessionManager.ShopName}");
                sb.AppendLine($" {SessionManager.ShopAddress}");
                sb.AppendLine($"      فون: {SessionManager.ShopPhone}");
                sb.AppendLine("------------------------------------");
                
                DateTime localDate = DateTime.Now;
                try
                {
                    var tz = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.ShopTimeZone);
                    localDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                }
                catch { }
                sb.AppendLine($"تاریخ: {localDate:dd/MM/yyyy mm:hh tt}");
                
                string cashierName = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.FullName : "سیلزمین";
                sb.AppendLine($"کیشیئر: {cashierName}");
                sb.AppendLine("گاہک : عام گاہک");
                
                sb.AppendLine("------------------------------------");
                sb.AppendLine("تفصیلِ کپڑا            مقدار    کل رقم");
                sb.AppendLine("------------------------------------");
                
                decimal subtotal = 0.00m;
                foreach (var item in cartList)
                {
                    string namePart = item.Name;
                    if (namePart.Length > 20) namePart = namePart.Substring(0, 17) + "...";
                    
                    // In Urdu specs, translate print/embroidery types if present
                    string specsUrdu = item.Specs;
                    if (item.Section == "Ladies")
                    {
                        string printDetail = item.IsPrinted 
                            ? $"پرنٹڈ({TranslatePrintType(item.PrintType)})" 
                            : "سادہ";
                        string embDetail = item.IsEmbroidered 
                            ? $"+کڑھائی({TranslateEmbroideryType(item.EmbroideryType)})" 
                            : "";
                        specsUrdu = $"{printDetail}{embDetail}";
                    }
                    else
                    {
                        specsUrdu = "مردانہ کپڑا";
                    }

                    string qtyTotalStr = item.IsBox
                        ? $"{item.Quantity,5:N0}باکس  Rs {item.Total,7:N0}"
                        : $"{item.Quantity,5:N1}میٹر  Rs {item.Total,7:N0}";
                    sb.AppendLine($"{namePart,-20} {qtyTotalStr}");
                    sb.AppendLine($"  [{specsUrdu}]");
                    
                    subtotal += item.Total;
                }
                
                sb.AppendLine("------------------------------------");
                sb.AppendLine($"کل رقم:            Rs {subtotal:N2}");
                
                decimal discount = 0.00m;
                decimal.TryParse(txtDiscount.Text.Trim(), out discount);
                if (discount > 0)
                {
                    sb.AppendLine($"ڈسکاؤنٹ:            Rs {discount:N2}");
                }
                
                decimal netTotal = Math.Max(0, subtotal - discount);
                sb.AppendLine($"خالص رقم:           Rs {netTotal:N2}");
                
                sb.AppendLine("------------------------------------");
                string paymentMethod = cbPaymentMethod.Text;
                string pmUrdu = paymentMethod;
                if (paymentMethod == "Cash") pmUrdu = "نقد (Cash)";
                else if (paymentMethod == "Card") pmUrdu = "کارڈ (Card)";
                else if (paymentMethod == "Mobile Banking") pmUrdu = "موبائل بینکنگ";
                sb.AppendLine($"طریقہ ادائیگی: {pmUrdu}");
                
                if (paymentMethod == "Cash")
                {
                    decimal received = 0.00m;
                    decimal.TryParse(txtReceived.Text.Trim(), out received);
                    sb.AppendLine($"وصول شدہ:            Rs {received:N2}");
                    
                    decimal change = Math.Max(0, received - netTotal);
                    sb.AppendLine($"باقی رقم:              Rs {change:N2}");
                }
                
                sb.AppendLine("------------------------------------");
                sb.AppendLine("      خریداری کا بہت شکریہ!");
                sb.AppendLine("    رسید کے بغیر کوئی واپسی نہیں۔");
            }
            else
            {
                sb.AppendLine($"      👗 {SessionManager.ShopName}");
                sb.AppendLine($" {SessionManager.ShopAddress}");
                sb.AppendLine($"      Phone: {SessionManager.ShopPhone}");
                sb.AppendLine("------------------------------------");
                
                DateTime localDate = DateTime.Now;
                try
                {
                    var tz = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.ShopTimeZone);
                    localDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                }
                catch { }
                sb.AppendLine($"Date: {localDate:dd/MM/yyyy mm:hh tt}");
                
                string cashierName = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.FullName : "Salesman";
                sb.AppendLine($"Cashier: {cashierName}");
                sb.AppendLine("Client : Walk-in Client");
                
                sb.AppendLine("------------------------------------");
                sb.AppendLine("Item Description       Qty    Total");
                sb.AppendLine("------------------------------------");
                
                decimal subtotal = 0.00m;
                foreach (var item in cartList)
                {
                    string namePart = item.Name;
                    if (namePart.Length > 20) namePart = namePart.Substring(0, 17) + "...";
                    
                    string qtyTotalStr = item.IsBox
                        ? $"{item.Quantity,5:N0} box  Rs {item.Total,7:N0}"
                        : $"{item.Quantity,5:N1}m  Rs {item.Total,7:N0}";
                    sb.AppendLine($"{namePart,-20} {qtyTotalStr}");
                    sb.AppendLine($"  [{item.Specs}]");
                    
                    subtotal += item.Total;
                }
                
                sb.AppendLine("------------------------------------");
                sb.AppendLine($"Subtotal:            Rs {subtotal:N2}");
                
                decimal discount = 0.00m;
                decimal.TryParse(txtDiscount.Text.Trim(), out discount);
                if (discount > 0)
                {
                    sb.AppendLine($"Discount:            Rs {discount:N2}");
                }
                
                decimal netTotal = Math.Max(0, subtotal - discount);
                sb.AppendLine($"Net Total:           Rs {netTotal:N2}");
                
                sb.AppendLine("------------------------------------");
                string paymentMethod = cbPaymentMethod.Text;
                sb.AppendLine($"Payment Method: {paymentMethod}");
                
                if (paymentMethod == "Cash")
                {
                    decimal received = 0.00m;
                    decimal.TryParse(txtReceived.Text.Trim(), out received);
                    sb.AppendLine($"Received:            Rs {received:N2}");
                    
                    decimal change = Math.Max(0, received - netTotal);
                    sb.AppendLine($"Change:              Rs {change:N2}");
                }
                
                sb.AppendLine("------------------------------------");
                sb.AppendLine("   Thank you for shopping with us!");
                sb.AppendLine("     No return without receipt.");
            }
            
            rtbReceipt.Text = sb.ToString();
        }

        private void TriggerReceiptPrint()
        {
            if (lastCompletedOrder == null) return;

            // Trigger beautiful standard Windows Print Preview
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintReceiptPage);
            
            // Set margins and paper width (standard 80mm thermal receipt is about 3 inches, i.e., 300 hundredths of an inch)
            pd.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            
            // Show print preview dialog
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.Width = 400;
            ppd.Height = 600;
            ppd.Text = $"Receipt Preview - {lastCompletedOrder.ReceiptNumber}";
            ppd.ShowDialog();
        }

        private void PrintReceiptPage(object sender, PrintPageEventArgs e)
        {
            if (lastCompletedOrder == null || lastCompletedOrderItems == null || e.Graphics == null) return;

            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            Graphics g = e.Graphics;
            string fontName = isUrdu ? "Segoe UI" : "Courier New";
            Font fontRegular = new Font(fontName, 9);
            Font fontBold = new Font(fontName, 9, FontStyle.Bold);
            Font fontTitle = new Font(fontName, 14, FontStyle.Bold);
            
            int y = 20;
            int margin = 10;
            int width = 280; // Approximate print area width for 80mm thermal receipt

            // Store Name & Info
            g.DrawString($"👗 {SessionManager.ShopName}", fontTitle, Brushes.Black, new RectangleF(margin, y, width, 25), new StringFormat { Alignment = StringAlignment.Center });
            y += 25;
            g.DrawString(SessionManager.ShopAddress, fontRegular, Brushes.Black, new RectangleF(margin, y, width, 15), new StringFormat { Alignment = StringAlignment.Center });
            y += 15;
            
            string labelPhone = isUrdu ? "فون" : "Phone";
            g.DrawString($"{labelPhone}: {SessionManager.ShopPhone}", fontRegular, Brushes.Black, new RectangleF(margin, y, width, 15), new StringFormat { Alignment = StringAlignment.Center });
            y += 25;

            // Separator
            g.DrawString("----------------------------------", fontRegular, Brushes.Black, margin, y);
            y += 15;

            // Date Conversion
            DateTime orderLocalDate = lastCompletedOrder.OrderDate;
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.ShopTimeZone);
                DateTime utcTime = lastCompletedOrder.OrderDate.Kind == DateTimeKind.Utc 
                    ? lastCompletedOrder.OrderDate 
                    : lastCompletedOrder.OrderDate.ToUniversalTime();
                orderLocalDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);
            }
            catch { }

            // Order Metadata
            string labelReceipt = isUrdu ? "رسید" : "Receipt";
            string labelDate = isUrdu ? "تاریخ" : "Date";
            string labelCashier = isUrdu ? "کیشیئر" : "Cashier";
            string labelClient = isUrdu ? "گاہک" : "Client";

            g.DrawString($"{labelReceipt}: {lastCompletedOrder.ReceiptNumber}", fontBold, Brushes.Black, margin, y);
            y += 15;
            g.DrawString($"{labelDate}   : {orderLocalDate:dd/MM/yyyy mm:hh tt}", fontRegular, Brushes.Black, margin, y);
            y += 15;
            g.DrawString($"{labelCashier}: {lastCompletedOrder.SalesmanName}", fontRegular, Brushes.Black, margin, y);
            y += 15;

            if (!string.IsNullOrEmpty(lastCompletedOrder.CustomerPhone))
            {
                g.DrawString($"{labelClient} : {lastCompletedOrder.CustomerName} ({lastCompletedOrder.CustomerPhone})", fontRegular, Brushes.Black, margin, y);
                y += 15;
            }
            
            // Separator
            g.DrawString("----------------------------------", fontRegular, Brushes.Black, margin, y);
            y += 15;

            // Items Table Header
            string labelHeader = isUrdu ? "تفصیلِ کپڑا                 مقدار  قیمت" : "Item Description        Qty  Price";
            g.DrawString(labelHeader, fontBold, Brushes.Black, margin, y);
            y += 15;
            g.DrawString("----------------------------------", fontRegular, Brushes.Black, margin, y);
            y += 12;

            // Loop items
            foreach (var item in lastCompletedOrderItems)
            {
                // Description line
                g.DrawString($"{item.FabricType}", fontBold, Brushes.Black, margin, y);
                
                // Qty & Price aligned right
                bool isBox = (item.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             (item.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;
                string qtyPrice = isUrdu 
                    ? (isBox ? $"{item.Quantity:N0}باکس x {item.UnitPrice:N0}" : $"{item.Quantity:N1}میٹر x {item.UnitPrice:N0}")
                    : (isBox ? $"{item.Quantity,5:N0} box x {item.UnitPrice,5:N0}" : $"{item.Quantity,5:N1}m x {item.UnitPrice,5:N0}");
                g.DrawString(qtyPrice, fontRegular, Brushes.Black, margin + 160, y);
                y += 15;

                // Specs / Design line
                if (item.Section == "Gents")
                {
                    string gentsLabel = isUrdu ? "  [مردانہ سوٹنگ]" : "  [Gents Suiting]";
                    g.DrawString(gentsLabel, fontRegular, Brushes.DarkGray, margin, y);
                    y += 15;
                }
                else
                {
                    string printDetail = item.IsPrinted 
                        ? (isUrdu ? $"پرنٹڈ({TranslatePrintType(item.PrintType)})" : $"Printed({item.PrintType})") 
                        : (isUrdu ? "سادہ" : "Plain");
                    string embDetail = item.IsEmbroidered 
                        ? (isUrdu ? $"+کڑھائی({TranslateEmbroideryType(item.EmbroideryType)})" : $"+Emb({item.EmbroideryType})") 
                        : "";
                    g.DrawString($"  [{printDetail}{embDetail}]", fontRegular, Brushes.DarkGray, margin, y);

                    if (item.IsEmbroidered && item.EmbroideryExtraCharge > 0)
                    {
                        string embChgLabel = isUrdu ? $"+کڑھائی خرچ: Rs {item.EmbroideryExtraCharge:N0}" : $"+Emb Chg: Rs {item.EmbroideryExtraCharge:N0}";
                        g.DrawString(embChgLabel, fontRegular, Brushes.DarkGray, margin + 140, y);
                    }
                    y += 15;
                }

                // Total line
                string totalLabel = isUrdu ? "  کل رقم:" : "  Total:";
                g.DrawString($"{totalLabel} Rs {item.TotalItemAmount:N2}", fontRegular, Brushes.Black, margin, y);
                y += 15;
            }

            // Separator
            g.DrawString("----------------------------------", fontRegular, Brushes.Black, margin, y);
            y += 15;

            // Totals
            string labelSubtotal = isUrdu ? "سب ٹوٹل" : "Subtotal";
            string labelDiscount = isUrdu ? "ڈسکاؤنٹ" : "Discount";
            string labelNetTotal = isUrdu ? "خالص رقم" : "Net Total";

            g.DrawString($"{labelSubtotal} : Rs {lastCompletedOrder.Subtotal:N2}", fontRegular, Brushes.Black, margin + 80, y);
            y += 15;
            if (lastCompletedOrder.Discount > 0)
            {
                g.DrawString($"{labelDiscount} : Rs {lastCompletedOrder.Discount:N2}", fontRegular, Brushes.Black, margin + 80, y);
                y += 15;
            }
            g.DrawString($"{labelNetTotal}: Rs {lastCompletedOrder.TotalAmount:N2}", fontBold, Brushes.Black, margin + 80, y);
            y += 20;

            // Payment summary
            string labelPayMethod = isUrdu ? "ادائیگی" : "Pay Method";
            string pmName = lastCompletedOrder.PaymentMethod;
            if (isUrdu)
            {
                if (pmName == "Cash") pmName = "نقد (Cash)";
                else if (pmName == "Card") pmName = "کارڈ (Card)";
                else if (pmName == "Mobile Banking") pmName = "موبائل بینکنگ";
            }
            g.DrawString($"{labelPayMethod}: {pmName}", fontRegular, Brushes.Black, margin, y);
            y += 15;

            if (lastCompletedOrder.PaymentMethod == "Cash")
            {
                string labelReceived = isUrdu ? "وصول" : "Received";
                string labelChange = isUrdu ? "باقی" : "Change";

                g.DrawString($"{labelReceived}  : Rs {lastCompletedOrder.AmountPaid:N2}", fontRegular, Brushes.Black, margin, y);
                y += 15;
                g.DrawString($"{labelChange}    : Rs {lastCompletedOrder.ChangeReturned:N2}", fontBold, Brushes.Black, margin, y);
                y += 15;
            }

            // Separator
            g.DrawString("----------------------------------", fontRegular, Brushes.Black, margin, y);
            y += 15;

            // Footer
            string footer1 = isUrdu ? "خریداری کا بہت شکریہ!" : "Thank you for shopping with us!";
            string footer2 = isUrdu ? "رسید کے بغیر کوئی واپسی ممکن نہیں۔" : "No return without receipt. Exchange";
            string footer3 = isUrdu ? "تبدیلی 7 دن کے اندر ممکن ہے، بشرطیکہ" : "possible within 7 days. Fabric must";
            string footer4 = isUrdu ? "کپڑا کٹا یا دھلا ہوا نہ ہو۔" : "not be cut or washed.";

            g.DrawString(footer1, fontBold, Brushes.Black, new RectangleF(margin, y, width, 15), new StringFormat { Alignment = StringAlignment.Center });
            y += 15;
            g.DrawString(footer2, fontRegular, Brushes.Black, new RectangleF(margin, y, width, 15), new StringFormat { Alignment = StringAlignment.Center });
            y += 15;
            g.DrawString(footer3, fontRegular, Brushes.Black, new RectangleF(margin, y, width, 15), new StringFormat { Alignment = StringAlignment.Center });
            y += 15;
            g.DrawString(footer4, fontRegular, Brushes.Black, new RectangleF(margin, y, width, 15), new StringFormat { Alignment = StringAlignment.Center });
        }

        public void ApplyLanguageTranslation()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            if (isUrdu)
            {
                this.RightToLeft = RightToLeft.Yes;
                
                txtBarcodeScan.PlaceholderText = "🔍 بارکوڈ اسکین کریں...";
                txtSearch.PlaceholderText = "🔍 دستی تلاش کریں...";

                // Labels
                lblCartHeader.Text = "خریداری کی ٹوکری";
                lblQty.Text = "مقدار/میٹر:";
                lblEmbCharge.Text = "کڑھائی چارجز:";
                lblDiscount.Text = "چھوٹ (ڈسکاؤنٹ):";
                lblPayment.Text = "طریقہ ادائیگی:";
                lblReceived.Text = "وصول شدہ رقم:";
                lblChange.Text = "باقی رقم:";
                lblReceiptHeader.Text = "رسید کا جائزہ";
                lblReceiptHelp.Text = "ٹوکری میں شامل کرنے سے پہلے پرنٹ/کڑھائی کے اختیارات منتخب کریں۔";
                
                // GroupBox
                gbLadiesOptions.Text = "زنانہ اختیارات";
                chkPrinted.Text = "پرنٹڈ";
                chkEmbroidered.Text = "کڑھائی شدہ";

                // Buttons
                btnDecrementQty.Text = "کم کریں (-1 میٹر)";
                btnRemoveCartItem.Text = "حذف کریں";
                btnAddToCart.Text = "ٹوکری میں شامل کریں 🛒";
                btnSaveOrder.Text = "💾 آرڈر محفوظ کریں";
                btnPrintReceipt.Text = "🖨️ رسید پرنٹ کریں";
                btnClearOrder.Text = "🗑️ صاف کریں";

                // Grid Columns headers if setup
                if (dgvCart.Columns.Count >= 5)
                {
                    dgvCart.Columns[0].HeaderText = "کپڑا";
                    dgvCart.Columns[1].HeaderText = "تفصیل";
                    dgvCart.Columns[2].HeaderText = "مقدار";
                    dgvCart.Columns[3].HeaderText = "قیمت";
                    dgvCart.Columns[4].HeaderText = "کل رقم";
                }
            }
            else
            {
                this.RightToLeft = RightToLeft.No;

                txtBarcodeScan.PlaceholderText = "🔍 Scan barcode here...";
                txtSearch.PlaceholderText = "🔍 Search manually...";

                // Labels
                lblCartHeader.Text = "Shopping Cart";
                lblQty.Text = "Qty/Meter:";
                lblEmbCharge.Text = "Emb. Charge:";
                lblDiscount.Text = "Discount:";
                lblPayment.Text = "Payment Method:";
                lblReceived.Text = "Received Amount:";
                lblChange.Text = "Change Returned:";
                lblReceiptHeader.Text = "Receipt Preview";
                lblReceiptHelp.Text = "Select print type/embroidery before adding to cart.";

                // GroupBox
                gbLadiesOptions.Text = "Ladies Customizations";
                chkPrinted.Text = "Printed";
                chkEmbroidered.Text = "Embroidered";

                // Buttons
                btnDecrementQty.Text = "Decrement (1m)";
                btnRemoveCartItem.Text = "Remove";
                btnAddToCart.Text = "Add to Cart 🛒";
                btnSaveOrder.Text = "💾 Save Order";
                btnPrintReceipt.Text = "🖨️ Print Receipt";
                btnClearOrder.Text = "🗑️ Clear";

                // Grid Columns headers if setup
                if (dgvCart.Columns.Count >= 5)
                {
                    dgvCart.Columns[0].HeaderText = "Fabric";
                    dgvCart.Columns[1].HeaderText = "Specs";
                    dgvCart.Columns[2].HeaderText = "Qty/Mtr";
                    dgvCart.Columns[3].HeaderText = "Price";
                    dgvCart.Columns[4].HeaderText = "Total";
                }
            }
            
            // Refresh details & grid column headers
            UpdateSelectedProductDetails();
            LoadProducts();
            
            // Refresh grid
            dgvCart.Refresh();

            // Refresh receipt preview
            UpdateRealtimeReceipt();
        }

        private void UpdateSelectedProductDetails()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            
            if (selectedProduct != null)
            {
                lblSelectedProduct.Text = selectedProduct.ShortName;
                bool isBox = (selectedProduct.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             (selectedProduct.FabricType ?? "").IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0;

                if (isBox)
                {
                    lblQty.Text = isUrdu ? "تعداد/باکس:" : "Qty/Box:";
                }
                else
                {
                    lblQty.Text = isUrdu ? "مقدار/میٹر:" : "Qty/Meter:";
                }
                
                if (selectedProduct.Section == "Gents")
                {
                    if (isUrdu)
                    {
                        string unitStr = isBox ? "باکس" : "میٹر";
                        lblProductDetails.Text = $"کپڑے کا مواد: {selectedProduct.FabricMaterial}\nرنگ: {selectedProduct.Color}\nتھوک قیمت: Rs {selectedProduct.WholesalePrice:N2}\nپرچون قیمت: Rs {selectedProduct.RetailPrice:N2}/{unitStr}";
                    }
                    else
                    {
                        string unitStr = isBox ? "box" : "meter";
                        lblProductDetails.Text = $"Material: {selectedProduct.FabricMaterial}\nColor: {selectedProduct.Color}\nWholesale: Rs {selectedProduct.WholesalePrice:N2}\nRetail Price: Rs {selectedProduct.RetailPrice:N2}/{unitStr}";
                    }
                }
                else
                {
                    if (isUrdu)
                    {
                        string unitStr = isBox ? "باکس" : "میٹر";
                        string printedStr = selectedProduct.IsPrinted ? $"ہاں ({TranslatePrintType(selectedProduct.PrintType)})" : "نہیں";
                        string embStr = selectedProduct.IsEmbroidered ? $"ہاں ({TranslateEmbroideryType(selectedProduct.EmbroideryType)})" : "نہیں";
                        lblProductDetails.Text = $"سوٹ کی قسم: {selectedProduct.SuitType}\nپرچون قیمت: Rs {selectedProduct.RetailPrice:N2}/{unitStr}\nپرنتڈ: {printedStr}\nکڑھائی شدہ: {embStr}";
                    }
                    else
                    {
                        string unitStr = isBox ? "box" : "meter";
                        lblProductDetails.Text = $"Suit Type: {selectedProduct.SuitType}\nRetail Price: Rs {selectedProduct.RetailPrice:N2}/{unitStr}\nPrinted: {(selectedProduct.IsPrinted ? "Yes (" + selectedProduct.PrintType + ")" : "No")}\nEmbroidered: {(selectedProduct.IsEmbroidered ? "Yes (" + selectedProduct.EmbroideryType + ")" : "No")}";
                    }
                }

                // Dynamically reposition elements in panelMiddle to avoid overlaps because lblProductDetails can expand
                int currentY = lblProductDetails.Bottom + 15;
                
                lblQty.Location = new Point(15, currentY);
                currentY += lblQty.Height + 5;
                
                numQty.Location = new Point(15, currentY);
                currentY += numQty.Height + 15;

                if (string.Equals(activeSection, "Gents", StringComparison.OrdinalIgnoreCase))
                {
                    gbLadiesOptions.Visible = false;
                    btnAddToCart.Location = new Point(15, currentY);
                }
                else
                {
                    gbLadiesOptions.Visible = true;
                    gbLadiesOptions.Location = new Point(15, currentY);
                    btnAddToCart.Location = new Point(15, gbLadiesOptions.Bottom + 15);
                }
            }
            else
            {
                lblSelectedProduct.Text = isUrdu ? "کوئی انتخاب نہیں" : "No Selection";
                lblProductDetails.Text = isUrdu ? "براہ کرم بائیں گرڈ سے کپڑا منتخب کریں۔" : "Please select a fabric from the left grid.";
                lblQty.Text = isUrdu ? "مقدار/میٹر:" : "Qty/Meter:";
                
                // Hide or reset positions
                gbLadiesOptions.Visible = false;
            }



        }

        private string TranslatePrintType(string? printType)
        {
            if (string.IsNullOrEmpty(printType)) return "";
            switch (printType.ToLower())
            {
                case "digital": return "ڈیجیٹل";
                case "block": return "بلاک";
                case "screen": return "اسکرین";
                default: return printType;
            }
        }

        private string TranslateEmbroideryType(string? embType)
        {
            if (string.IsNullOrEmpty(embType)) return "";
            switch (embType.ToLower())
            {
                case "hand work": return "ہاتھ کا کام";
                case "machine": return "مشین";
                case "chikan": return "چکن کاری";
                default: return embType;
            }
        }

        private decimal GetDefaultQuantityForProduct(Product? prod)
        {
            if (prod == null) return 1.00m;
            string fType = prod.FabricType ?? "";
            
            // Box design check
            if (fType.IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0 || fType.IndexOf("باکس", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return 1.00m;
            }

            if (string.Equals(prod.Section, "Gents", StringComparison.OrdinalIgnoreCase))
            {
                if (fType.IndexOf("cotton", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 4.50m;
                }
                if (fType.IndexOf("wash", StringComparison.OrdinalIgnoreCase) >= 0 || fType.IndexOf("wear", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 4.50m;
                }
                if (fType.IndexOf("mix", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 4.00m;
                }
            }
            else if (string.Equals(prod.Section, "Ladies", StringComparison.OrdinalIgnoreCase))
            {
                if (fType.IndexOf("dupatta", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 2.50m;
                }
                if (fType.IndexOf("shawl", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 2.50m;
                }
                
                string suitType = prod.SuitType ?? "";
                if (suitType.IndexOf("3-Piece", StringComparison.OrdinalIgnoreCase) >= 0 || suitType.IndexOf("3", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 7.50m;
                }
                if (suitType.IndexOf("2-Piece", StringComparison.OrdinalIgnoreCase) >= 0 || suitType.IndexOf("2", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 5.00m;
                }
            }
            return 1.00m;
        }
    }

    // Helper Cart View Model
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specs { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal EmbroideryCharge { get; set; }
        public decimal Total { get; set; }
        public string Section { get; set; } = "Gents";
        public bool IsPrinted { get; set; }
        public string? PrintType { get; set; }
        public bool IsEmbroidered { get; set; }
        public string? EmbroideryType { get; set; }
        public bool IsBox { get; set; }
    }
}
