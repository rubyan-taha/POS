using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using GarmentShopPos.Models;

namespace GarmentShopPos
{
    public partial class EditProductForm : Form
    {
        private Product _product;

        public EditProductForm(Product product)
        {
            InitializeComponent();
            _product = product;
        }

        private void EditProductForm_Load(object sender, EventArgs e)
        {
            // Populate common fields
            txtBarcode.Text = _product.Barcode;
            txtFabricType.Text = _product.FabricType;
            txtWholesale.Text = _product.WholesalePrice.ToString("F2");
            txtRetail.Text = _product.RetailPrice.ToString("F2");
            txtCurrentStock.Text = _product.CurrentStock.ToString("F2");
            txtReorder.Text = _product.ReorderPoint.ToString("F2");

            // Setup Section Specific visibility
            if (_product.Section == "Gents")
            {
                // Gents visible, Ladies hidden
                lblFabricMaterial.Visible = true;
                txtFabricMaterial.Visible = true;
                lblColor.Visible = true;
                txtColor.Visible = true;

                lblSuitType.Visible = false;
                cbSuitType.Visible = false;
                chkIsPrinted.Visible = false;
                lblPrintType.Visible = false;
                cbPrintType.Visible = false;
                chkIsEmbroidered.Visible = false;
                lblEmbroideryType.Visible = false;
                cbEmbroideryType.Visible = false;
                lblEmbCharge.Visible = false;
                txtEmbCharge.Visible = false;

                txtFabricMaterial.Text = _product.FabricMaterial ?? "";
                txtColor.Text = _product.Color ?? "";
            }
            else // Ladies
            {
                // Ladies visible, Gents hidden
                lblFabricMaterial.Visible = false;
                txtFabricMaterial.Visible = false;
                lblColor.Visible = false;
                txtColor.Visible = false;

                lblSuitType.Visible = true;
                cbSuitType.Visible = true;
                chkIsPrinted.Visible = true;
                lblPrintType.Visible = true;
                cbPrintType.Visible = true;
                chkIsEmbroidered.Visible = true;
                lblEmbroideryType.Visible = true;
                cbEmbroideryType.Visible = true;
                lblEmbCharge.Visible = true;
                txtEmbCharge.Visible = true;

                // Populate Ladies fields
                if (cbSuitType.Items.Contains(_product.SuitType ?? ""))
                {
                    cbSuitType.SelectedItem = _product.SuitType;
                }
                else
                {
                    cbSuitType.SelectedIndex = 0;
                }

                chkIsPrinted.Checked = _product.IsPrinted;
                cbPrintType.Enabled = _product.IsPrinted;
                if (cbPrintType.Items.Contains(_product.PrintType ?? ""))
                {
                    cbPrintType.SelectedItem = _product.PrintType;
                }
                else
                {
                    cbPrintType.SelectedIndex = 0;
                }

                chkIsEmbroidered.Checked = _product.IsEmbroidered;
                cbEmbroideryType.Enabled = _product.IsEmbroidered;
                txtEmbCharge.Enabled = _product.IsEmbroidered;
                if (cbEmbroideryType.Items.Contains(_product.EmbroideryType ?? ""))
                {
                    cbEmbroideryType.SelectedItem = _product.EmbroideryType;
                }
                else
                {
                    cbEmbroideryType.SelectedIndex = 0;
                }
                txtEmbCharge.Text = _product.EmbroideryExtraCharge.ToString("F2");
            }

            ApplyLanguageTranslation();

            if (_product.CurrentStock > 0)
            {
                btnDelete.Enabled = false;
                btnDelete.Text = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase)
                    ? "🗑️ حذف (صرف 0 اسٹاک پر)"
                    : "🗑️ Delete (Only 0 stock)";
            }
        }

        private void chkIsPrinted_CheckedChanged(object sender, EventArgs e)
        {
            cbPrintType.Enabled = chkIsPrinted.Checked;
        }

        private void chkIsEmbroidered_CheckedChanged(object sender, EventArgs e)
        {
            cbEmbroideryType.Enabled = chkIsEmbroidered.Checked;
            txtEmbCharge.Enabled = chkIsEmbroidered.Checked;
            if (!chkIsEmbroidered.Checked)
            {
                txtEmbCharge.Text = "0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string barcode = txtBarcode.Text.Trim();
            string fabric = txtFabricType.Text.Trim();

            if (string.IsNullOrEmpty(fabric))
            {
                MessageBox.Show("Fabric Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(barcode))
            {
                MessageBox.Show("Barcode is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if duplicate barcode exists on another product
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM products WHERE barcode = @barcode AND id != @id AND is_deleted = 0", conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    cmd.Parameters.AddWithValue("@id", _product.Id);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("This barcode is already assigned to another product.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (!decimal.TryParse(txtWholesale.Text.Trim(), out decimal wholesale) || wholesale < 0)
            {
                MessageBox.Show("Please enter a valid Wholesale Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtRetail.Text.Trim(), out decimal retail) || retail < 0)
            {
                MessageBox.Show("Please enter a valid Retail Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (wholesale > retail)
            {
                MessageBox.Show("Wholesale price cannot be greater than retail price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtCurrentStock.Text.Trim(), out decimal stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid stock level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtReorder.Text.Trim(), out decimal reorder) || reorder < 0)
            {
                MessageBox.Show("Please enter a valid reorder warning point.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal embCharge = 0.00m;
            if (_product.Section == "Ladies" && chkIsEmbroidered.Checked)
            {
                if (!decimal.TryParse(txtEmbCharge.Text.Trim(), out embCharge) || embCharge < 0)
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
                    string query = @"
                        UPDATE products
                        SET barcode = @barcode,
                            fabric_type = @fabric,
                            fabric_material = @material,
                            color = @color,
                            suit_type = @suitType,
                            is_printed = @isPrinted,
                            print_type = @printType,
                            is_embroidered = @isEmb,
                            embroidery_type = @embType,
                            embroidery_extra_charge = @embCharge,
                            wholesale_price = @wholesale,
                            retail_price = @retail,
                            current_stock = @stock,
                            reorder_point = @reorder
                        WHERE id = @id;";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", _product.Id);
                        cmd.Parameters.AddWithValue("@barcode", barcode);
                        cmd.Parameters.AddWithValue("@fabric", fabric);
                        
                        if (_product.Section == "Gents")
                        {
                            cmd.Parameters.AddWithValue("@material", txtFabricMaterial.Text.Trim());
                            cmd.Parameters.AddWithValue("@color", txtColor.Text.Trim());
                            cmd.Parameters.AddWithValue("@suitType", DBNull.Value);
                            cmd.Parameters.AddWithValue("@isPrinted", 0);
                            cmd.Parameters.AddWithValue("@printType", DBNull.Value);
                            cmd.Parameters.AddWithValue("@isEmb", 0);
                            cmd.Parameters.AddWithValue("@embType", DBNull.Value);
                            cmd.Parameters.AddWithValue("@embCharge", 0.00m);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@material", DBNull.Value);
                            cmd.Parameters.AddWithValue("@color", DBNull.Value);
                            cmd.Parameters.AddWithValue("@suitType", cbSuitType.Text);
                            cmd.Parameters.AddWithValue("@isPrinted", chkIsPrinted.Checked ? 1 : 0);
                            cmd.Parameters.AddWithValue("@printType", chkIsPrinted.Checked ? cbPrintType.Text : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@isEmb", chkIsEmbroidered.Checked ? 1 : 0);
                            cmd.Parameters.AddWithValue("@embType", chkIsEmbroidered.Checked ? cbEmbroideryType.Text : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@embCharge", embCharge);
                        }

                        cmd.Parameters.AddWithValue("@wholesale", wholesale);
                        cmd.Parameters.AddWithValue("@retail", retail);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@reorder", reorder);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Product details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save changes:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show($"Are you sure you want to delete this product ({_product.FabricType}) from stock?\nHistorical sales records will not be broken, but it will be hidden from sale screen.", 
                                            "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE products SET is_deleted = 1 WHERE id = @id";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", _product.Id);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Product has been removed from stock.", "Product Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete product:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ApplyLanguageTranslation()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            if (isUrdu)
            {
                this.RightToLeft = RightToLeft.Yes;
                this.Text = "پروڈکٹ کی معلومات میں ترمیم / حذف";
                lblTitle.Text = "✏️ پروڈکٹ کی معلومات میں ترمیم کریں";
                lblBarcode.Text = "بارکوڈ / SKU:";
                lblFabricType.Text = "کپڑے کی قسم (مثال: شرٹنگ): *";
                lblFabricMaterial.Text = "کپڑے کا مواد (مثال: کاٹن):";
                lblColor.Text = "کپڑے کا رنگ (مثال: نیوی بلیو):";
                lblSuitType.Text = "سوٹ کی قسم (مثال: تھری پیس):";
                chkIsPrinted.Text = "پرنٹڈ کپڑا ہے؟";
                lblPrintType.Text = "پرنٹ کی قسم:";
                chkIsEmbroidered.Text = "کڑھائی شامل ہے؟";
                lblEmbroideryType.Text = "کڑھائی کی قسم:";
                lblEmbCharge.Text = "کڑھائی کے اضافی چارجز:";
                lblWholesale.Text = "تھوک قیمت (ہول سیل): *";
                lblRetail.Text = "پرچون قیمت (ریٹیل): *";
                lblCurrentStock.Text = "موجودہ اسٹاک (میٹر): *";
                lblReorder.Text = "ری آرڈر وارننگ حد:*";
                
                btnSave.Text = "💾 محفوظ کریں";
                btnDelete.Text = "🗑️ حذف کریں";
                btnCancel.Text = "❌ منسوخ کریں";
            }
            else
            {
                this.RightToLeft = RightToLeft.No;
                this.Text = "Edit / Delete Product Details";
                lblTitle.Text = "✏️ Edit Product Details";
                lblBarcode.Text = "Barcode / SKU:";
                lblFabricType.Text = "Fabric Type (e.g. Shirting): *";
                lblFabricMaterial.Text = "Fabric Material (e.g. Cotton):";
                lblColor.Text = "Fabric Color (e.g. Navy Blue):";
                lblSuitType.Text = "Suit Style Type (e.g. 3pc):";
                chkIsPrinted.Text = "Printed Fabric?";
                lblPrintType.Text = "Print Type:";
                chkIsEmbroidered.Text = "Includes Embroidery?";
                lblEmbroideryType.Text = "Embroidery Type:";
                lblEmbCharge.Text = "Embroidery Extra Charge:";
                lblWholesale.Text = "Wholesale Price (per meter): *";
                lblRetail.Text = "Retail Price (per meter): *";
                lblCurrentStock.Text = "Current Stock (meters): *";
                lblReorder.Text = "Reorder Warning Point:*";

                btnSave.Text = "💾 Save";
                btnDelete.Text = "🗑️ Delete";
                btnCancel.Text = "❌ Cancel";
            }

            UpdateLabelsForBox();
        }

        private void UpdateLabelsForBox()
        {
            if (_product == null) return;
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            bool isBox = (_product.FabricType ?? "").IndexOf("box", StringComparison.OrdinalIgnoreCase) >= 0;

            if (isBox)
            {
                if (isUrdu)
                {
                    lblWholesale.Text = "تھوک قیمت: *";
                    lblRetail.Text = "پرچون قیمت: *";
                    lblCurrentStock.Text = "موجودہ اسٹاک (باکس): *";
                }
                else
                {
                    lblWholesale.Text = "Wholesale Price: *";
                    lblRetail.Text = "Retail Price: *";
                    lblCurrentStock.Text = "Current Stock (boxes): *";
                }
            }
        }
    }
}
