namespace GarmentShopPos
{
    partial class EditProductForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblFabricType;
        private System.Windows.Forms.TextBox txtFabricType;
        private System.Windows.Forms.Label lblFabricMaterial;
        private System.Windows.Forms.TextBox txtFabricMaterial;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblSuitType;
        private System.Windows.Forms.ComboBox cbSuitType;
        private System.Windows.Forms.CheckBox chkIsPrinted;
        private System.Windows.Forms.Label lblPrintType;
        private System.Windows.Forms.ComboBox cbPrintType;
        private System.Windows.Forms.CheckBox chkIsEmbroidered;
        private System.Windows.Forms.Label lblEmbroideryType;
        private System.Windows.Forms.ComboBox cbEmbroideryType;
        private System.Windows.Forms.Label lblEmbCharge;
        private System.Windows.Forms.TextBox txtEmbCharge;
        
        private System.Windows.Forms.Label lblWholesale;
        private System.Windows.Forms.TextBox txtWholesale;
        private System.Windows.Forms.Label lblRetail;
        private System.Windows.Forms.TextBox txtRetail;
        private System.Windows.Forms.Label lblCurrentStock;
        private System.Windows.Forms.TextBox txtCurrentStock;
        private System.Windows.Forms.Label lblReorder;
        private System.Windows.Forms.TextBox txtReorder;
        
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelTop;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblFabricType = new System.Windows.Forms.Label();
            this.txtFabricType = new System.Windows.Forms.TextBox();
            this.lblFabricMaterial = new System.Windows.Forms.Label();
            this.txtFabricMaterial = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblSuitType = new System.Windows.Forms.Label();
            this.cbSuitType = new System.Windows.Forms.ComboBox();
            this.chkIsPrinted = new System.Windows.Forms.CheckBox();
            this.lblPrintType = new System.Windows.Forms.Label();
            this.cbPrintType = new System.Windows.Forms.ComboBox();
            this.chkIsEmbroidered = new System.Windows.Forms.CheckBox();
            this.lblEmbroideryType = new System.Windows.Forms.Label();
            this.cbEmbroideryType = new System.Windows.Forms.ComboBox();
            this.lblEmbCharge = new System.Windows.Forms.Label();
            this.txtEmbCharge = new System.Windows.Forms.TextBox();
            this.lblWholesale = new System.Windows.Forms.Label();
            this.txtWholesale = new System.Windows.Forms.TextBox();
            this.lblRetail = new System.Windows.Forms.Label();
            this.txtRetail = new System.Windows.Forms.TextBox();
            this.lblCurrentStock = new System.Windows.Forms.Label();
            this.txtCurrentStock = new System.Windows.Forms.TextBox();
            this.lblReorder = new System.Windows.Forms.Label();
            this.txtReorder = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(37)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(760, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(760, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "✏️ Edit Product Details";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblBarcode.Location = new System.Drawing.Point(25, 80);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(95, 17);
            this.lblBarcode.TabIndex = 1;
            this.lblBarcode.Text = "Barcode / SKU:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBarcode.Location = new System.Drawing.Point(25, 102);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(330, 25);
            this.txtBarcode.TabIndex = 2;
            // 
            // lblFabricType
            // 
            this.lblFabricType.AutoSize = true;
            this.lblFabricType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFabricType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblFabricType.Location = new System.Drawing.Point(25, 140);
            this.lblFabricType.Name = "lblFabricType";
            this.lblFabricType.Size = new System.Drawing.Size(183, 17);
            this.lblFabricType.TabIndex = 3;
            this.lblFabricType.Text = "Fabric Type (e.g. Shirting): *";
            // 
            // txtFabricType
            // 
            this.txtFabricType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFabricType.Location = new System.Drawing.Point(25, 162);
            this.txtFabricType.Name = "txtFabricType";
            this.txtFabricType.Size = new System.Drawing.Size(330, 25);
            this.txtFabricType.TabIndex = 4;
            // 
            // lblFabricMaterial
            // 
            this.lblFabricMaterial.AutoSize = true;
            this.lblFabricMaterial.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFabricMaterial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblFabricMaterial.Location = new System.Drawing.Point(25, 200);
            this.lblFabricMaterial.Name = "lblFabricMaterial";
            this.lblFabricMaterial.Size = new System.Drawing.Size(107, 17);
            this.lblFabricMaterial.TabIndex = 5;
            this.lblFabricMaterial.Text = "Fabric Material:";
            // 
            // txtFabricMaterial
            // 
            this.txtFabricMaterial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFabricMaterial.Location = new System.Drawing.Point(25, 222);
            this.txtFabricMaterial.Name = "txtFabricMaterial";
            this.txtFabricMaterial.Size = new System.Drawing.Size(330, 25);
            this.txtFabricMaterial.TabIndex = 6;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblColor.Location = new System.Drawing.Point(25, 260);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(89, 17);
            this.lblColor.TabIndex = 7;
            this.lblColor.Text = "Fabric Color:";
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColor.Location = new System.Drawing.Point(25, 282);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(330, 25);
            this.txtColor.TabIndex = 8;
            // 
            // lblSuitType
            // 
            this.lblSuitType.AutoSize = true;
            this.lblSuitType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSuitType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblSuitType.Location = new System.Drawing.Point(25, 200);
            this.lblSuitType.Name = "lblSuitType";
            this.lblSuitType.Size = new System.Drawing.Size(107, 17);
            this.lblSuitType.TabIndex = 9;
            this.lblSuitType.Text = "Suit Style Type:";
            // 
            // cbSuitType
            // 
            this.cbSuitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSuitType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbSuitType.FormattingEnabled = true;
            this.cbSuitType.Items.AddRange(new object[] { "3-Piece", "2-Piece", "Single" });
            this.cbSuitType.Location = new System.Drawing.Point(25, 222);
            this.cbSuitType.Name = "cbSuitType";
            this.cbSuitType.Size = new System.Drawing.Size(330, 25);
            this.cbSuitType.TabIndex = 10;
            // 
            // chkIsPrinted
            // 
            this.chkIsPrinted.AutoSize = true;
            this.chkIsPrinted.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.chkIsPrinted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.chkIsPrinted.Location = new System.Drawing.Point(25, 260);
            this.chkIsPrinted.Name = "chkIsPrinted";
            this.chkIsPrinted.Size = new System.Drawing.Size(117, 21);
            this.chkIsPrinted.TabIndex = 11;
            this.chkIsPrinted.Text = "Printed Fabric?";
            this.chkIsPrinted.UseVisualStyleBackColor = true;
            this.chkIsPrinted.CheckedChanged += new System.EventHandler(this.chkIsPrinted_CheckedChanged);
            // 
            // lblPrintType
            // 
            this.lblPrintType.AutoSize = true;
            this.lblPrintType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPrintType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPrintType.Location = new System.Drawing.Point(180, 260);
            this.lblPrintType.Name = "lblPrintType";
            this.lblPrintType.Size = new System.Drawing.Size(73, 17);
            this.lblPrintType.TabIndex = 12;
            this.lblPrintType.Text = "Print Type:";
            // 
            // cbPrintType
            // 
            this.cbPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrintType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbPrintType.FormattingEnabled = true;
            this.cbPrintType.Items.AddRange(new object[] { "Digital", "Block", "Screen" });
            this.cbPrintType.Location = new System.Drawing.Point(180, 282);
            this.cbPrintType.Name = "cbPrintType";
            this.cbPrintType.Size = new System.Drawing.Size(175, 25);
            this.cbPrintType.TabIndex = 13;
            // 
            // chkIsEmbroidered
            // 
            this.chkIsEmbroidered.AutoSize = true;
            this.chkIsEmbroidered.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.chkIsEmbroidered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.chkIsEmbroidered.Location = new System.Drawing.Point(25, 320);
            this.chkIsEmbroidered.Name = "chkIsEmbroidered";
            this.chkIsEmbroidered.Size = new System.Drawing.Size(155, 21);
            this.chkIsEmbroidered.TabIndex = 14;
            this.chkIsEmbroidered.Text = "Includes Embroidery?";
            this.chkIsEmbroidered.UseVisualStyleBackColor = true;
            this.chkIsEmbroidered.CheckedChanged += new System.EventHandler(this.chkIsEmbroidered_CheckedChanged);
            // 
            // lblEmbroideryType
            // 
            this.lblEmbroideryType.AutoSize = true;
            this.lblEmbroideryType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEmbroideryType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblEmbroideryType.Location = new System.Drawing.Point(180, 320);
            this.lblEmbroideryType.Name = "lblEmbroideryType";
            this.lblEmbroideryType.Size = new System.Drawing.Size(115, 17);
            this.lblEmbroideryType.TabIndex = 15;
            this.lblEmbroideryType.Text = "Embroidery Type:";
            // 
            // cbEmbroideryType
            // 
            this.cbEmbroideryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmbroideryType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbEmbroideryType.FormattingEnabled = true;
            this.cbEmbroideryType.Items.AddRange(new object[] { "Hand work", "Machine", "Chikan" });
            this.cbEmbroideryType.Location = new System.Drawing.Point(180, 342);
            this.cbEmbroideryType.Name = "cbEmbroideryType";
            this.cbEmbroideryType.Size = new System.Drawing.Size(175, 25);
            this.cbEmbroideryType.TabIndex = 16;
            // 
            // lblEmbCharge
            // 
            this.lblEmbCharge.AutoSize = true;
            this.lblEmbCharge.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEmbCharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblEmbCharge.Location = new System.Drawing.Point(25, 380);
            this.lblEmbCharge.Name = "lblEmbCharge";
            this.lblEmbCharge.Size = new System.Drawing.Size(163, 17);
            this.lblEmbCharge.TabIndex = 17;
            this.lblEmbCharge.Text = "Embroidery Extra Charge:";
            // 
            // txtEmbCharge
            // 
            this.txtEmbCharge.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmbCharge.Location = new System.Drawing.Point(25, 402);
            this.txtEmbCharge.Name = "txtEmbCharge";
            this.txtEmbCharge.Size = new System.Drawing.Size(330, 25);
            this.txtEmbCharge.TabIndex = 18;
            this.txtEmbCharge.Text = "0.00";
            // 
            // lblWholesale
            // 
            this.lblWholesale.AutoSize = true;
            this.lblWholesale.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblWholesale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblWholesale.Location = new System.Drawing.Point(400, 80);
            this.lblWholesale.Name = "lblWholesale";
            this.lblWholesale.Size = new System.Drawing.Size(227, 17);
            this.lblWholesale.TabIndex = 19;
            this.lblWholesale.Text = "Wholesale Price (per yard/meter): *";
            // 
            // txtWholesale
            // 
            this.txtWholesale.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWholesale.Location = new System.Drawing.Point(400, 102);
            this.txtWholesale.Name = "txtWholesale";
            this.txtWholesale.Size = new System.Drawing.Size(330, 25);
            this.txtWholesale.TabIndex = 20;
            // 
            // lblRetail
            // 
            this.lblRetail.AutoSize = true;
            this.lblRetail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblRetail.Location = new System.Drawing.Point(400, 140);
            this.lblRetail.Name = "lblRetail";
            this.lblRetail.Size = new System.Drawing.Size(193, 17);
            this.lblRetail.TabIndex = 21;
            this.lblRetail.Text = "Retail Price (per yard/meter): *";
            // 
            // txtRetail
            // 
            this.txtRetail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRetail.Location = new System.Drawing.Point(400, 162);
            this.txtRetail.Name = "txtRetail";
            this.txtRetail.Size = new System.Drawing.Size(330, 25);
            this.txtRetail.TabIndex = 22;
            // 
            // lblCurrentStock
            // 
            this.lblCurrentStock.AutoSize = true;
            this.lblCurrentStock.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCurrentStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCurrentStock.Location = new System.Drawing.Point(400, 200);
            this.lblCurrentStock.Name = "lblCurrentStock";
            this.lblCurrentStock.Size = new System.Drawing.Size(189, 17);
            this.lblCurrentStock.TabIndex = 23;
            this.lblCurrentStock.Text = "Current Stock (yards/meters):*";
            // 
            // txtCurrentStock
            // 
            this.txtCurrentStock.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCurrentStock.Location = new System.Drawing.Point(400, 222);
            this.txtCurrentStock.Name = "txtCurrentStock";
            this.txtCurrentStock.Size = new System.Drawing.Size(330, 25);
            this.txtCurrentStock.TabIndex = 24;
            // 
            // lblReorder
            // 
            this.lblReorder.AutoSize = true;
            this.lblReorder.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblReorder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblReorder.Location = new System.Drawing.Point(400, 260);
            this.lblReorder.Name = "lblReorder";
            this.lblReorder.Size = new System.Drawing.Size(135, 17);
            this.lblReorder.TabIndex = 25;
            this.lblReorder.Text = "Reorder Alert Limit:*";
            // 
            // txtReorder
            // 
            this.txtReorder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReorder.Location = new System.Drawing.Point(400, 282);
            this.txtReorder.Name = "txtReorder";
            this.txtReorder.Size = new System.Drawing.Size(330, 25);
            this.txtReorder.TabIndex = 26;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(400, 392);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 36);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(515, 392);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 36);
            this.btnDelete.TabIndex = 28;
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(630, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(760, 460);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtReorder);
            this.Controls.Add(this.lblReorder);
            this.Controls.Add(this.txtCurrentStock);
            this.Controls.Add(this.lblCurrentStock);
            this.Controls.Add(this.txtRetail);
            this.Controls.Add(this.lblRetail);
            this.Controls.Add(this.txtWholesale);
            this.Controls.Add(this.lblWholesale);
            this.Controls.Add(this.txtEmbCharge);
            this.Controls.Add(this.lblEmbCharge);
            this.Controls.Add(this.cbEmbroideryType);
            this.Controls.Add(this.lblEmbroideryType);
            this.Controls.Add(this.chkIsEmbroidered);
            this.Controls.Add(this.cbPrintType);
            this.Controls.Add(this.lblPrintType);
            this.Controls.Add(this.chkIsPrinted);
            this.Controls.Add(this.cbSuitType);
            this.Controls.Add(this.lblSuitType);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtFabricMaterial);
            this.Controls.Add(this.lblFabricMaterial);
            this.Controls.Add(this.txtFabricType);
            this.Controls.Add(this.lblFabricType);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update / Delete Product Details";
            this.Load += new System.EventHandler(this.EditProductForm_Load);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
