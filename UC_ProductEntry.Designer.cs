namespace GarmentShopPos
{
    partial class UC_ProductEntry
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlEntry;
        private System.Windows.Forms.TabPage tabGents;
        private System.Windows.Forms.TabPage tabLadies;
        private System.Windows.Forms.Label lblGFabricType;
        private System.Windows.Forms.ComboBox cbGFabricType;
        private System.Windows.Forms.Label lblGMaterial;
        private System.Windows.Forms.TextBox txtGMaterial;
        private System.Windows.Forms.Label lblGColor;
        private System.Windows.Forms.TextBox txtGColor;
        private System.Windows.Forms.Label lblGWholesale;
        private System.Windows.Forms.TextBox txtGWholesale;
        private System.Windows.Forms.Label lblGRetail;
        private System.Windows.Forms.TextBox txtGRetail;
        private System.Windows.Forms.Label lblGStock;
        private System.Windows.Forms.TextBox txtGStock;
        private System.Windows.Forms.Label lblGReorder;
        private System.Windows.Forms.TextBox txtGReorder;
        private System.Windows.Forms.Button btnSaveGents;
        private System.Windows.Forms.Label lblGBarcode;
        private System.Windows.Forms.TextBox txtGBarcode;
        
        // Ladies controls
        private System.Windows.Forms.Label lblLFabricType;
        private System.Windows.Forms.ComboBox cbLFabricType;
        private System.Windows.Forms.Label lblLSuitType;
        private System.Windows.Forms.ComboBox cbLSuitType;
        private System.Windows.Forms.CheckBox chkLPrinted;
        private System.Windows.Forms.ComboBox cbLPrintType;
        private System.Windows.Forms.CheckBox chkLEmbroidered;
        private System.Windows.Forms.ComboBox cbLEmbroideryType;
        private System.Windows.Forms.Label lblLEmbCharge;
        private System.Windows.Forms.TextBox txtLEmbCharge;
        private System.Windows.Forms.Label lblLWholesale;
        private System.Windows.Forms.TextBox txtLWholesale;
        private System.Windows.Forms.Label lblLRetail;
        private System.Windows.Forms.TextBox txtLRetail;
        private System.Windows.Forms.Label lblLStock;
        private System.Windows.Forms.TextBox txtLStock;
        private System.Windows.Forms.Label lblLReorder;
        private System.Windows.Forms.TextBox txtLReorder;
        private System.Windows.Forms.Button btnSaveLadies;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLBarcode;
        private System.Windows.Forms.TextBox txtLBarcode;

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
            this.tabControlEntry = new System.Windows.Forms.TabControl();
            this.tabGents = new System.Windows.Forms.TabPage();
            this.btnSaveGents = new System.Windows.Forms.Button();
            this.txtGReorder = new System.Windows.Forms.TextBox();
            this.lblGReorder = new System.Windows.Forms.Label();
            this.txtGStock = new System.Windows.Forms.TextBox();
            this.lblGStock = new System.Windows.Forms.Label();
            this.txtGRetail = new System.Windows.Forms.TextBox();
            this.lblGRetail = new System.Windows.Forms.Label();
            this.txtGWholesale = new System.Windows.Forms.TextBox();
            this.lblGWholesale = new System.Windows.Forms.Label();
            this.txtGColor = new System.Windows.Forms.TextBox();
            this.lblGColor = new System.Windows.Forms.Label();
            this.txtGMaterial = new System.Windows.Forms.TextBox();
            this.lblGMaterial = new System.Windows.Forms.Label();
            this.cbGFabricType = new System.Windows.Forms.ComboBox();
            this.lblGFabricType = new System.Windows.Forms.Label();
            this.tabLadies = new System.Windows.Forms.TabPage();
            this.btnSaveLadies = new System.Windows.Forms.Button();
            this.txtLReorder = new System.Windows.Forms.TextBox();
            this.lblLReorder = new System.Windows.Forms.Label();
            this.txtLStock = new System.Windows.Forms.TextBox();
            this.lblLStock = new System.Windows.Forms.Label();
            this.txtLRetail = new System.Windows.Forms.TextBox();
            this.lblLRetail = new System.Windows.Forms.Label();
            this.txtLWholesale = new System.Windows.Forms.TextBox();
            this.lblLWholesale = new System.Windows.Forms.Label();
            this.txtLEmbCharge = new System.Windows.Forms.TextBox();
            this.lblLEmbCharge = new System.Windows.Forms.Label();
            this.cbLEmbroideryType = new System.Windows.Forms.ComboBox();
            this.chkLEmbroidered = new System.Windows.Forms.CheckBox();
            this.cbLPrintType = new System.Windows.Forms.ComboBox();
            this.chkLPrinted = new System.Windows.Forms.CheckBox();
            this.cbLSuitType = new System.Windows.Forms.ComboBox();
            this.lblLSuitType = new System.Windows.Forms.Label();
            this.cbLFabricType = new System.Windows.Forms.ComboBox();
            this.lblLFabricType = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGBarcode = new System.Windows.Forms.Label();
            this.txtGBarcode = new System.Windows.Forms.TextBox();
            this.lblLBarcode = new System.Windows.Forms.Label();
            this.txtLBarcode = new System.Windows.Forms.TextBox();
            this.tabControlEntry.SuspendLayout();
            this.tabGents.SuspendLayout();
            this.tabLadies.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlEntry
            // 
            this.tabControlEntry.Controls.Add(this.tabGents);
            this.tabControlEntry.Controls.Add(this.tabLadies);
            this.tabControlEntry.Font = new System.Drawing.Font("Segoe UI Semibold", 10.25F, System.Drawing.FontStyle.Bold);
            this.tabControlEntry.Location = new System.Drawing.Point(22, 57);
            this.tabControlEntry.Name = "tabControlEntry";
            this.tabControlEntry.SelectedIndex = 0;
            this.tabControlEntry.Size = new System.Drawing.Size(1006, 558);
            this.tabControlEntry.TabIndex = 0;
            // 
            // tabGents
            // 
            this.tabGents.BackColor = System.Drawing.Color.White;
            this.tabGents.Controls.Add(this.btnSaveGents);
            this.tabGents.Controls.Add(this.txtGBarcode);
            this.tabGents.Controls.Add(this.lblGBarcode);
            this.tabGents.Controls.Add(this.txtGReorder);
            this.tabGents.Controls.Add(this.lblGReorder);
            this.tabGents.Controls.Add(this.txtGStock);
            this.tabGents.Controls.Add(this.lblGStock);
            this.tabGents.Controls.Add(this.txtGRetail);
            this.tabGents.Controls.Add(this.lblGRetail);
            this.tabGents.Controls.Add(this.txtGWholesale);
            this.tabGents.Controls.Add(this.lblGWholesale);
            this.tabGents.Controls.Add(this.txtGColor);
            this.tabGents.Controls.Add(this.lblGColor);
            this.tabGents.Controls.Add(this.txtGMaterial);
            this.tabGents.Controls.Add(this.lblGMaterial);
            this.tabGents.Controls.Add(this.cbGFabricType);
            this.tabGents.Controls.Add(this.lblGFabricType);
            this.tabGents.Location = new System.Drawing.Point(4, 28);
            this.tabGents.Name = "tabGents";
            this.tabGents.Padding = new System.Windows.Forms.Padding(30);
            this.tabGents.Size = new System.Drawing.Size(998, 526);
            this.tabGents.TabIndex = 0;
            this.tabGents.Text = "Gents Section (Men\'s Fabric)";
            // 
            // btnSaveGents
            // 
            this.btnSaveGents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnSaveGents.FlatAppearance.BorderSize = 0;
            this.btnSaveGents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveGents.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveGents.ForeColor = System.Drawing.Color.White;
            this.btnSaveGents.Location = new System.Drawing.Point(34, 403);
            this.btnSaveGents.Name = "btnSaveGents";
            this.btnSaveGents.Size = new System.Drawing.Size(200, 45);
            this.btnSaveGents.TabIndex = 14;
            this.btnSaveGents.Text = "💾 Save Gents Product";
            this.btnSaveGents.UseVisualStyleBackColor = false;
            this.btnSaveGents.Click += new System.EventHandler(this.btnSaveGents_Click);
            // 
            this.txtGBarcode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGBarcode.Location = new System.Drawing.Point(593, 298);
            this.txtGBarcode.Name = "txtGBarcode";
            this.txtGBarcode.Size = new System.Drawing.Size(220, 25);
            this.txtGBarcode.TabIndex = 15;
            this.txtGBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGBarcode_KeyDown);
            // 
            // lblGBarcode
            // 
            this.lblGBarcode.AutoSize = true;
            this.lblGBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGBarcode.Location = new System.Drawing.Point(590, 276);
            this.lblGBarcode.Name = "lblGBarcode";
            this.lblGBarcode.Size = new System.Drawing.Size(126, 19);
            this.lblGBarcode.TabIndex = 14;
            this.lblGBarcode.Text = "Barcode / SKU:";
            // 
            // txtGReorder
            // 
            this.txtGReorder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGReorder.Location = new System.Drawing.Point(313, 298);
            this.txtGReorder.Name = "txtGReorder";
            this.txtGReorder.Size = new System.Drawing.Size(220, 25);
            this.txtGReorder.TabIndex = 13;
            this.txtGReorder.Text = "10";
            // 
            // lblGReorder
            // 
            this.lblGReorder.AutoSize = true;
            this.lblGReorder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGReorder.Location = new System.Drawing.Point(310, 276);
            this.lblGReorder.Name = "lblGReorder";
            this.lblGReorder.Size = new System.Drawing.Size(193, 19);
            this.lblGReorder.TabIndex = 12;
            this.lblGReorder.Text = "Reorder Warning Alert Point:*";
            // 
            // txtGStock
            // 
            this.txtGStock.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGStock.Location = new System.Drawing.Point(34, 298);
            this.txtGStock.Name = "txtGStock";
            this.txtGStock.Size = new System.Drawing.Size(220, 25);
            this.txtGStock.TabIndex = 11;
            this.txtGStock.Text = "0";
            // 
            // lblGStock
            // 
            this.lblGStock.AutoSize = true;
            this.lblGStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGStock.Location = new System.Drawing.Point(30, 276);
            this.lblGStock.Name = "lblGStock";
            this.lblGStock.Size = new System.Drawing.Size(197, 19);
            this.lblGStock.TabIndex = 10;
            this.lblGStock.Text = "Initial Stock (meters / yards):*";
            // 
            // txtGRetail
            // 
            this.txtGRetail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGRetail.Location = new System.Drawing.Point(313, 213);
            this.txtGRetail.Name = "txtGRetail";
            this.txtGRetail.Size = new System.Drawing.Size(220, 25);
            this.txtGRetail.TabIndex = 9;
            // 
            // lblGRetail
            // 
            this.lblGRetail.AutoSize = true;
            this.lblGRetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGRetail.Location = new System.Drawing.Point(310, 191);
            this.lblGRetail.Name = "lblGRetail";
            this.lblGRetail.Size = new System.Drawing.Size(198, 19);
            this.lblGRetail.TabIndex = 8;
            this.lblGRetail.Text = "Retail Price (per yard/meter):*";
            // 
            // txtGWholesale
            // 
            this.txtGWholesale.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGWholesale.Location = new System.Drawing.Point(34, 213);
            this.txtGWholesale.Name = "txtGWholesale";
            this.txtGWholesale.Size = new System.Drawing.Size(220, 25);
            this.txtGWholesale.TabIndex = 7;
            // 
            // lblGWholesale
            // 
            this.lblGWholesale.AutoSize = true;
            this.lblGWholesale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGWholesale.Location = new System.Drawing.Point(30, 191);
            this.lblGWholesale.Name = "lblGWholesale";
            this.lblGWholesale.Size = new System.Drawing.Size(117, 19);
            this.lblGWholesale.TabIndex = 6;
            this.lblGWholesale.Text = "Wholesale Price:*";
            // 
            // txtGColor
            // 
            this.txtGColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGColor.Location = new System.Drawing.Point(593, 126);
            this.txtGColor.Name = "txtGColor";
            this.txtGColor.Size = new System.Drawing.Size(220, 25);
            this.txtGColor.TabIndex = 5;
            // 
            // lblGColor
            // 
            this.lblGColor.AutoSize = true;
            this.lblGColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGColor.Location = new System.Drawing.Point(590, 104);
            this.lblGColor.Name = "lblGColor";
            this.lblGColor.Size = new System.Drawing.Size(95, 19);
            this.lblGColor.TabIndex = 4;
            this.lblGColor.Text = "Color / Shade:";
            // 
            // txtGMaterial
            // 
            this.txtGMaterial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGMaterial.Location = new System.Drawing.Point(313, 126);
            this.txtGMaterial.Name = "txtGMaterial";
            this.txtGMaterial.Size = new System.Drawing.Size(220, 25);
            this.txtGMaterial.TabIndex = 3;
            // 
            // lblGMaterial
            // 
            this.lblGMaterial.AutoSize = true;
            this.lblGMaterial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGMaterial.Location = new System.Drawing.Point(310, 104);
            this.lblGMaterial.Name = "lblGMaterial";
            this.lblGMaterial.Size = new System.Drawing.Size(182, 19);
            this.lblGMaterial.TabIndex = 2;
            this.lblGMaterial.Text = "Fabric Material (e.g. Linen):*";
            // 
            // cbGFabricType
            // 
            this.cbGFabricType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbGFabricType.Location = new System.Drawing.Point(34, 126);
            this.cbGFabricType.Name = "cbGFabricType";
            this.cbGFabricType.Size = new System.Drawing.Size(220, 25);
            this.cbGFabricType.TabIndex = 1;
            this.cbGFabricType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGFabricType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGFabricType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            // 
            // lblGFabricType
            // 
            this.lblGFabricType.AutoSize = true;
            this.lblGFabricType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblGFabricType.Location = new System.Drawing.Point(30, 104);
            this.lblGFabricType.Name = "lblGFabricType";
            this.lblGFabricType.Size = new System.Drawing.Size(193, 19);
            this.lblGFabricType.TabIndex = 0;
            this.lblGFabricType.Text = "Fabric Type (e.g. Shirting): *";
            // 
            // tabLadies
            // 
            this.tabLadies.BackColor = System.Drawing.Color.White;
            this.tabLadies.Controls.Add(this.btnSaveLadies);
            this.tabLadies.Controls.Add(this.txtLBarcode);
            this.tabLadies.Controls.Add(this.lblLBarcode);
            this.tabLadies.Controls.Add(this.txtLReorder);
            this.tabLadies.Controls.Add(this.lblLReorder);
            this.tabLadies.Controls.Add(this.txtLStock);
            this.tabLadies.Controls.Add(this.lblLStock);
            this.tabLadies.Controls.Add(this.txtLRetail);
            this.tabLadies.Controls.Add(this.lblLRetail);
            this.tabLadies.Controls.Add(this.txtLWholesale);
            this.tabLadies.Controls.Add(this.lblLWholesale);
            this.tabLadies.Controls.Add(this.txtLEmbCharge);
            this.tabLadies.Controls.Add(this.lblLEmbCharge);
            this.tabLadies.Controls.Add(this.cbLEmbroideryType);
            this.tabLadies.Controls.Add(this.chkLEmbroidered);
            this.tabLadies.Controls.Add(this.cbLPrintType);
            this.tabLadies.Controls.Add(this.chkLPrinted);
            this.tabLadies.Controls.Add(this.cbLSuitType);
            this.tabLadies.Controls.Add(this.lblLSuitType);
            this.tabLadies.Controls.Add(this.cbLFabricType);
            this.tabLadies.Controls.Add(this.lblLFabricType);
            this.tabLadies.Location = new System.Drawing.Point(4, 28);
            this.tabLadies.Name = "tabLadies";
            this.tabLadies.Padding = new System.Windows.Forms.Padding(30);
            this.tabLadies.Size = new System.Drawing.Size(998, 526);
            this.tabLadies.TabIndex = 1;
            this.tabLadies.Text = "Ladies Section (Women\'s Fabric)";
            // 
            // btnSaveLadies
            // 
            this.btnSaveLadies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnSaveLadies.FlatAppearance.BorderSize = 0;
            this.btnSaveLadies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveLadies.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveLadies.ForeColor = System.Drawing.Color.White;
            this.btnSaveLadies.Location = new System.Drawing.Point(34, 427);
            this.btnSaveLadies.Name = "btnSaveLadies";
            this.btnSaveLadies.Size = new System.Drawing.Size(200, 45);
            this.btnSaveLadies.TabIndex = 18;
            this.btnSaveLadies.Text = "💾 Save Ladies Product";
            this.btnSaveLadies.UseVisualStyleBackColor = false;
            this.btnSaveLadies.Click += new System.EventHandler(this.btnSaveLadies_Click);
            // 
            this.txtLBarcode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLBarcode.Location = new System.Drawing.Point(593, 370);
            this.txtLBarcode.Name = "txtLBarcode";
            this.txtLBarcode.Size = new System.Drawing.Size(220, 25);
            this.txtLBarcode.TabIndex = 19;
            this.txtLBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLBarcode_KeyDown);
            // 
            // lblLBarcode
            // 
            this.lblLBarcode.AutoSize = true;
            this.lblLBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLBarcode.Location = new System.Drawing.Point(590, 348);
            this.lblLBarcode.Name = "lblLBarcode";
            this.lblLBarcode.Size = new System.Drawing.Size(126, 19);
            this.lblLBarcode.TabIndex = 18;
            this.lblLBarcode.Text = "Barcode / SKU:";
            // 
            // txtLReorder
            // 
            this.txtLReorder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLReorder.Location = new System.Drawing.Point(313, 370);
            this.txtLReorder.Name = "txtLReorder";
            this.txtLReorder.Size = new System.Drawing.Size(220, 25);
            this.txtLReorder.TabIndex = 17;
            this.txtLReorder.Text = "10";
            // 
            // lblLReorder
            // 
            this.lblLReorder.AutoSize = true;
            this.lblLReorder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLReorder.Location = new System.Drawing.Point(310, 348);
            this.lblLReorder.Name = "lblLReorder";
            this.lblLReorder.Size = new System.Drawing.Size(193, 19);
            this.lblLReorder.TabIndex = 16;
            this.lblLReorder.Text = "Reorder Warning Alert Point:*";
            // 
            // txtLStock
            // 
            this.txtLStock.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLStock.Location = new System.Drawing.Point(34, 370);
            this.txtLStock.Name = "txtLStock";
            this.txtLStock.Size = new System.Drawing.Size(220, 25);
            this.txtLStock.TabIndex = 15;
            this.txtLStock.Text = "0";
            // 
            // lblLStock
            // 
            this.lblLStock.AutoSize = true;
            this.lblLStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLStock.Location = new System.Drawing.Point(30, 348);
            this.lblLStock.Name = "lblLStock";
            this.lblLStock.Size = new System.Drawing.Size(197, 19);
            this.lblLStock.TabIndex = 14;
            this.lblLStock.Text = "Initial Stock (meters / yards):*";
            // 
            // txtLRetail
            // 
            this.txtLRetail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLRetail.Location = new System.Drawing.Point(313, 298);
            this.txtLRetail.Name = "txtLRetail";
            this.txtLRetail.Size = new System.Drawing.Size(220, 25);
            this.txtLRetail.TabIndex = 13;
            // 
            // lblLRetail
            // 
            this.lblLRetail.AutoSize = true;
            this.lblLRetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLRetail.Location = new System.Drawing.Point(310, 276);
            this.lblLRetail.Name = "lblLRetail";
            this.lblLRetail.Size = new System.Drawing.Size(202, 19);
            this.lblLRetail.TabIndex = 12;
            this.lblLRetail.Text = "Retail Price (per meter / yard):*";
            // 
            // txtLWholesale
            // 
            this.txtLWholesale.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLWholesale.Location = new System.Drawing.Point(34, 298);
            this.txtLWholesale.Name = "txtLWholesale";
            this.txtLWholesale.Size = new System.Drawing.Size(220, 25);
            this.txtLWholesale.TabIndex = 11;
            // 
            // lblLWholesale
            // 
            this.lblLWholesale.AutoSize = true;
            this.lblLWholesale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLWholesale.Location = new System.Drawing.Point(30, 276);
            this.lblLWholesale.Name = "lblLWholesale";
            this.lblLWholesale.Size = new System.Drawing.Size(117, 19);
            this.lblLWholesale.TabIndex = 10;
            this.lblLWholesale.Text = "Wholesale Price:*";
            // 
            // txtLEmbCharge
            // 
            this.txtLEmbCharge.Enabled = false;
            this.txtLEmbCharge.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLEmbCharge.Location = new System.Drawing.Point(593, 214);
            this.txtLEmbCharge.Name = "txtLEmbCharge";
            this.txtLEmbCharge.Size = new System.Drawing.Size(220, 25);
            this.txtLEmbCharge.TabIndex = 9;
            this.txtLEmbCharge.Text = "0.00";
            // 
            // lblLEmbCharge
            // 
            this.lblLEmbCharge.AutoSize = true;
            this.lblLEmbCharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLEmbCharge.Location = new System.Drawing.Point(590, 192);
            this.lblLEmbCharge.Name = "lblLEmbCharge";
            this.lblLEmbCharge.Size = new System.Drawing.Size(171, 19);
            this.lblLEmbCharge.TabIndex = 8;
            this.lblLEmbCharge.Text = "Embroidery Extra Charge:";
            // 
            // cbLEmbroideryType
            // 
            this.cbLEmbroideryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLEmbroideryType.Enabled = false;
            this.cbLEmbroideryType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLEmbroideryType.FormattingEnabled = true;
            this.cbLEmbroideryType.Location = new System.Drawing.Point(593, 155);
            this.cbLEmbroideryType.Name = "cbLEmbroideryType";
            this.cbLEmbroideryType.Size = new System.Drawing.Size(220, 25);
            this.cbLEmbroideryType.TabIndex = 7;
            // 
            // chkLEmbroidered
            // 
            this.chkLEmbroidered.AutoSize = true;
            this.chkLEmbroidered.Location = new System.Drawing.Point(593, 126);
            this.chkLEmbroidered.Name = "chkLEmbroidered";
            this.chkLEmbroidered.Size = new System.Drawing.Size(161, 23);
            this.chkLEmbroidered.TabIndex = 6;
            this.chkLEmbroidered.Text = "Includes Embroidery?";
            this.chkLEmbroidered.UseVisualStyleBackColor = true;
            this.chkLEmbroidered.CheckedChanged += new System.EventHandler(this.chkLEmbroidered_CheckedChanged);
            // 
            // cbLPrintType
            // 
            this.cbLPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLPrintType.Enabled = false;
            this.cbLPrintType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLPrintType.FormattingEnabled = true;
            this.cbLPrintType.Location = new System.Drawing.Point(313, 214);
            this.cbLPrintType.Name = "cbLPrintType";
            this.cbLPrintType.Size = new System.Drawing.Size(220, 25);
            this.cbLPrintType.TabIndex = 5;
            // 
            // chkLPrinted
            // 
            this.chkLPrinted.AutoSize = true;
            this.chkLPrinted.Location = new System.Drawing.Point(313, 185);
            this.chkLPrinted.Name = "chkLPrinted";
            this.chkLPrinted.Size = new System.Drawing.Size(123, 23);
            this.chkLPrinted.TabIndex = 4;
            this.chkLPrinted.Text = "Printed Fabric?";
            this.chkLPrinted.UseVisualStyleBackColor = true;
            this.chkLPrinted.CheckedChanged += new System.EventHandler(this.chkLPrinted_CheckedChanged);
            // 
            // cbLSuitType
            // 
            this.cbLSuitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLSuitType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLSuitType.FormattingEnabled = true;
            this.cbLSuitType.Location = new System.Drawing.Point(313, 126);
            this.cbLSuitType.Name = "cbLSuitType";
            this.cbLSuitType.Size = new System.Drawing.Size(220, 25);
            this.cbLSuitType.TabIndex = 3;
            // 
            // lblLSuitType
            // 
            this.lblLSuitType.AutoSize = true;
            this.lblLSuitType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLSuitType.Location = new System.Drawing.Point(310, 104);
            this.lblLSuitType.Name = "lblLSuitType";
            this.lblLSuitType.Size = new System.Drawing.Size(155, 19);
            this.lblLSuitType.TabIndex = 2;
            this.lblLSuitType.Text = "Suit Style Type (e.g. 3pc):";
            // 
            // cbLFabricType
            // 
            this.cbLFabricType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLFabricType.Location = new System.Drawing.Point(34, 126);
            this.cbLFabricType.Name = "cbLFabricType";
            this.cbLFabricType.Size = new System.Drawing.Size(220, 25);
            this.cbLFabricType.TabIndex = 1;
            this.cbLFabricType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbLFabricType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLFabricType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            // 
            // lblLFabricType
            // 
            this.lblLFabricType.AutoSize = true;
            this.lblLFabricType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLFabricType.Location = new System.Drawing.Point(30, 104);
            this.lblLFabricType.Name = "lblLFabricType";
            this.lblLFabricType.Size = new System.Drawing.Size(183, 19);
            this.lblLFabricType.TabIndex = 0;
            this.lblLFabricType.Text = "Fabric Type (e.g. Lawn Chiffon):*";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(206, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Add New Fabric / Stock";
            // 
            // UC_ProductEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabControlEntry);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UC_ProductEntry";
            this.Size = new System.Drawing.Size(1050, 640);
            this.Load += new System.EventHandler(this.UC_ProductEntry_Load);
            this.tabControlEntry.ResumeLayout(false);
            this.tabGents.ResumeLayout(false);
            this.tabGents.PerformLayout();
            this.tabLadies.ResumeLayout(false);
            this.tabLadies.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
