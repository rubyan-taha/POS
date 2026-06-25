namespace GarmentShopPos
{
    partial class UC_POS
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Panel panelReceiptPreview;
        private System.Windows.Forms.Label lblReceiptHeader;
        private System.Windows.Forms.RichTextBox rtbReceipt;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnPrintReceipt;
        private System.Windows.Forms.Button btnClearOrder;
        private System.Windows.Forms.Label lblReceiptHelp;
        private System.Windows.Forms.TextBox txtBarcodeScan;
        private System.Windows.Forms.Button btnDecrementQty;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Button btnGentsSection;
        private System.Windows.Forms.Button btnLadiesSection;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblSelectedProduct;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.GroupBox gbLadiesOptions;
        private System.Windows.Forms.CheckBox chkPrinted;
        private System.Windows.Forms.ComboBox cbPrintType;
        private System.Windows.Forms.CheckBox chkEmbroidered;
        private System.Windows.Forms.ComboBox cbEmbroideryType;
        private System.Windows.Forms.Label lblEmbCharge;
        private System.Windows.Forms.TextBox txtEmbCharge;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ComboBox cbPaymentMethod;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.TextBox txtReceived;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblChangeValue;
        private System.Windows.Forms.Label lblProductDetails;
        private System.Windows.Forms.Label lblCartHeader;
        private System.Windows.Forms.Button btnRemoveCartItem;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtBarcodeScan = new System.Windows.Forms.TextBox();
            this.btnLadiesSection = new System.Windows.Forms.Button();
            this.btnGentsSection = new System.Windows.Forms.Button();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.lblProductDetails = new System.Windows.Forms.Label();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.gbLadiesOptions = new System.Windows.Forms.GroupBox();
            this.txtEmbCharge = new System.Windows.Forms.TextBox();
            this.lblEmbCharge = new System.Windows.Forms.Label();
            this.cbEmbroideryType = new System.Windows.Forms.ComboBox();
            this.chkEmbroidered = new System.Windows.Forms.CheckBox();
            this.cbPrintType = new System.Windows.Forms.ComboBox();
            this.chkPrinted = new System.Windows.Forms.CheckBox();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblSelectedProduct = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.btnRemoveCartItem = new System.Windows.Forms.Button();
            this.btnDecrementQty = new System.Windows.Forms.Button();
            this.lblChangeValue = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.txtReceived = new System.Windows.Forms.TextBox();
            this.lblReceived = new System.Windows.Forms.Label();
            this.cbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblPayment = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.lblCartHeader = new System.Windows.Forms.Label();
            this.panelReceiptPreview = new System.Windows.Forms.Panel();
            this.lblReceiptHeader = new System.Windows.Forms.Label();
            this.rtbReceipt = new System.Windows.Forms.RichTextBox();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this.btnClearOrder = new System.Windows.Forms.Button();
            this.lblReceiptHelp = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.gbLadiesOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.panelRight.SuspendLayout();
            this.panelReceiptPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.dgvProducts);
            this.panelLeft.Controls.Add(this.panelFilter);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeft.Size = new System.Drawing.Size(240, 640);
            this.panelLeft.TabIndex = 0;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.ColumnHeadersHeight = 35;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvProducts.Location = new System.Drawing.Point(10, 90);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowTemplate.Height = 35;
            this.dgvProducts.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(220, 540);
            this.dgvProducts.TabIndex = 1;
            this.dgvProducts.SelectionChanged += new System.EventHandler(this.dgvProducts_SelectionChanged);
            // 
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.txtBarcodeScan);
            this.panelFilter.Controls.Add(this.btnLadiesSection);
            this.panelFilter.Controls.Add(this.btnGentsSection);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(10, 10);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(220, 80);
            this.panelFilter.TabIndex = 0;
            // 
            // txtBarcodeScan
            // 
            this.txtBarcodeScan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBarcodeScan.Location = new System.Drawing.Point(3, 10);
            this.txtBarcodeScan.Name = "txtBarcodeScan";
            this.txtBarcodeScan.PlaceholderText = "🏷️ Scan barcode here...";
            this.txtBarcodeScan.Size = new System.Drawing.Size(214, 27);
            this.txtBarcodeScan.TabIndex = 0;
            this.txtBarcodeScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcodeScan_KeyDown);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(3, 45);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍 Or search manually...";
            this.txtSearch.Size = new System.Drawing.Size(214, 27);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnLadiesSection
            // 
            this.btnLadiesSection.BackColor = System.Drawing.Color.White;
            this.btnLadiesSection.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(219)))), ((int)(((byte)(225)))));
            this.btnLadiesSection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLadiesSection.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLadiesSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnLadiesSection.Location = new System.Drawing.Point(208, 3);
            this.btnLadiesSection.Name = "btnLadiesSection";
            this.btnLadiesSection.Size = new System.Drawing.Size(199, 38);
            this.btnLadiesSection.TabIndex = 1;
            this.btnLadiesSection.Text = "👩 Ladies Section";
            this.btnLadiesSection.UseVisualStyleBackColor = false;
            this.btnLadiesSection.Click += new System.EventHandler(this.btnLadiesSection_Click);
            // 
            // btnGentsSection
            // 
            this.btnGentsSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnGentsSection.FlatAppearance.BorderSize = 0;
            this.btnGentsSection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGentsSection.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGentsSection.ForeColor = System.Drawing.Color.White;
            this.btnGentsSection.Location = new System.Drawing.Point(3, 3);
            this.btnGentsSection.Name = "btnGentsSection";
            this.btnGentsSection.Size = new System.Drawing.Size(199, 38);
            this.btnGentsSection.TabIndex = 0;
            this.btnGentsSection.Text = "👨 Gents Section";
            this.btnGentsSection.UseVisualStyleBackColor = false;
            this.btnGentsSection.Click += new System.EventHandler(this.btnGentsSection_Click);
            // 
            // 
            // panelMiddle
            // 
            this.panelMiddle.BackColor = System.Drawing.Color.White;
            this.panelMiddle.Controls.Add(this.lblProductDetails);
            this.panelMiddle.Controls.Add(this.btnAddToCart);
            this.panelMiddle.Controls.Add(this.gbLadiesOptions);
            this.panelMiddle.Controls.Add(this.numQty);
            this.panelMiddle.Controls.Add(this.lblQty);
            this.panelMiddle.Controls.Add(this.lblSelectedProduct);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMiddle.Location = new System.Drawing.Point(240, 0);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Padding = new System.Windows.Forms.Padding(15);
            this.panelMiddle.Size = new System.Drawing.Size(180, 640);
            this.panelMiddle.AutoScroll = true;
            this.panelMiddle.TabIndex = 1;
            // 
            // lblProductDetails
            // 
            this.lblProductDetails.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblProductDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblProductDetails.Location = new System.Drawing.Point(15, 93);
            this.lblProductDetails.Name = "lblProductDetails";
            this.lblProductDetails.Size = new System.Drawing.Size(150, 75);
            this.lblProductDetails.TabIndex = 6;
            this.lblProductDetails.Text = "Please select a fabric from the left grid.";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnAddToCart.FlatAppearance.BorderSize = 0;
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(15, 513);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(150, 45);
            this.btnAddToCart.TabIndex = 5;
            this.btnAddToCart.Text = "🛒 Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // gbLadiesOptions
            // 
            this.gbLadiesOptions.Controls.Add(this.txtEmbCharge);
            this.gbLadiesOptions.Controls.Add(this.lblEmbCharge);
            this.gbLadiesOptions.Controls.Add(this.cbEmbroideryType);
            this.gbLadiesOptions.Controls.Add(this.chkEmbroidered);
            this.gbLadiesOptions.Controls.Add(this.cbPrintType);
            this.gbLadiesOptions.Controls.Add(this.chkPrinted);
            this.gbLadiesOptions.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbLadiesOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.gbLadiesOptions.Location = new System.Drawing.Point(15, 239);
            this.gbLadiesOptions.Name = "gbLadiesOptions";
            this.gbLadiesOptions.Size = new System.Drawing.Size(150, 255);
            this.gbLadiesOptions.TabIndex = 4;
            this.gbLadiesOptions.TabStop = false;
            this.gbLadiesOptions.Text = "Ladies Design Specs";
            this.gbLadiesOptions.Visible = false;
            // 
            // txtEmbCharge
            // 
            this.txtEmbCharge.Enabled = false;
            this.txtEmbCharge.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtEmbCharge.Location = new System.Drawing.Point(10, 215);
            this.txtEmbCharge.Name = "txtEmbCharge";
            this.txtEmbCharge.Size = new System.Drawing.Size(130, 24);
            this.txtEmbCharge.TabIndex = 5;
            this.txtEmbCharge.Text = "0.00";
            // 
            // lblEmbCharge
            // 
            this.lblEmbCharge.AutoSize = true;
            this.lblEmbCharge.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmbCharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblEmbCharge.Location = new System.Drawing.Point(10, 197);
            this.lblEmbCharge.Name = "lblEmbCharge";
            this.lblEmbCharge.Size = new System.Drawing.Size(126, 15);
            this.lblEmbCharge.TabIndex = 4;
            this.lblEmbCharge.Text = "Embroidery Charge (Rs)";
            // 
            // cbEmbroideryType
            // 
            this.cbEmbroideryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmbroideryType.Enabled = false;
            this.cbEmbroideryType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cbEmbroideryType.FormattingEnabled = true;
            this.cbEmbroideryType.Location = new System.Drawing.Point(10, 161);
            this.cbEmbroideryType.Name = "cbEmbroideryType";
            this.cbEmbroideryType.Size = new System.Drawing.Size(130, 25);
            this.cbEmbroideryType.TabIndex = 3;
            // 
            // chkEmbroidered
            // 
            this.chkEmbroidered.AutoSize = true;
            this.chkEmbroidered.Location = new System.Drawing.Point(10, 134);
            this.chkEmbroidered.Name = "chkEmbroidered";
            this.chkEmbroidered.Size = new System.Drawing.Size(155, 23);
            this.chkEmbroidered.TabIndex = 2;
            this.chkEmbroidered.Text = "Embroidery / Karai?";
            this.chkEmbroidered.UseVisualStyleBackColor = true;
            this.chkEmbroidered.CheckedChanged += new System.EventHandler(this.chkEmbroidered_CheckedChanged);
            // 
            // cbPrintType
            // 
            this.cbPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrintType.Enabled = false;
            this.cbPrintType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cbPrintType.FormattingEnabled = true;
            this.cbPrintType.Location = new System.Drawing.Point(10, 89);
            this.cbPrintType.Name = "cbPrintType";
            this.cbPrintType.Size = new System.Drawing.Size(130, 25);
            this.cbPrintType.TabIndex = 1;
            // 
            // chkPrinted
            // 
            this.chkPrinted.AutoSize = true;
            this.chkPrinted.Location = new System.Drawing.Point(10, 62);
            this.chkPrinted.Name = "chkPrinted";
            this.chkPrinted.Size = new System.Drawing.Size(117, 23);
            this.chkPrinted.TabIndex = 0;
            this.chkPrinted.Text = "Printed Fabric?";
            this.chkPrinted.UseVisualStyleBackColor = true;
            this.chkPrinted.CheckedChanged += new System.EventHandler(this.chkPrinted_CheckedChanged);
            // 
            // numQty
            // 
            this.numQty.DecimalPlaces = 2;
            this.numQty.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numQty.Location = new System.Drawing.Point(15, 196);
            this.numQty.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(150, 27);
            this.numQty.TabIndex = 2;
            this.numQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblQty.Location = new System.Drawing.Point(15, 174);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(150, 19);
            this.lblQty.TabIndex = 1;
            this.lblQty.Text = "Fabric Quantity (Yards/Meters)";
            // 
            // lblSelectedProduct
            // 
            this.lblSelectedProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectedProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblSelectedProduct.Location = new System.Drawing.Point(15, 15);
            this.lblSelectedProduct.Name = "lblSelectedProduct";
            this.lblSelectedProduct.Size = new System.Drawing.Size(150, 72);
            this.lblSelectedProduct.TabIndex = 0;
            this.lblSelectedProduct.Text = "Selected Product";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.btnRemoveCartItem);
            this.panelRight.Controls.Add(this.btnDecrementQty);
            this.panelRight.Controls.Add(this.lblChangeValue);
            this.panelRight.Controls.Add(this.lblChange);
            this.panelRight.Controls.Add(this.txtReceived);
            this.panelRight.Controls.Add(this.lblReceived);
            this.panelRight.Controls.Add(this.cbPaymentMethod);
            this.panelRight.Controls.Add(this.lblPayment);
            this.panelRight.Controls.Add(this.txtDiscount);
            this.panelRight.Controls.Add(this.lblDiscount);
            this.panelRight.Controls.Add(this.lblTotal);
            this.panelRight.Controls.Add(this.lblSubtotal);
            this.panelRight.Controls.Add(this.dgvCart);
            this.panelRight.Controls.Add(this.lblCartHeader);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(690, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(10);
            this.panelRight.Size = new System.Drawing.Size(360, 640);
            this.panelRight.AutoScroll = true;
            this.panelRight.TabIndex = 2;
            // 
            // btnRemoveCartItem
            // 
            this.btnRemoveCartItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveCartItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnRemoveCartItem.FlatAppearance.BorderSize = 0;
            this.btnRemoveCartItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCartItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemoveCartItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveCartItem.Location = new System.Drawing.Point(260, 10);
            this.btnRemoveCartItem.Name = "btnRemoveCartItem";
            this.btnRemoveCartItem.Size = new System.Drawing.Size(90, 25);
            this.btnRemoveCartItem.TabIndex = 13;
            this.btnRemoveCartItem.Text = "🗑️ Remove";
            this.btnRemoveCartItem.UseVisualStyleBackColor = false;
            this.btnRemoveCartItem.Click += new System.EventHandler(this.btnRemoveCartItem_Click);
            // 
            // btnDecrementQty
            // 
            this.btnDecrementQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecrementQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnDecrementQty.FlatAppearance.BorderSize = 0;
            this.btnDecrementQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecrementQty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDecrementQty.ForeColor = System.Drawing.Color.White;
            this.btnDecrementQty.Location = new System.Drawing.Point(165, 10);
            this.btnDecrementQty.Name = "btnDecrementQty";
            this.btnDecrementQty.Size = new System.Drawing.Size(90, 25);
            this.btnDecrementQty.TabIndex = 14;
            this.btnDecrementQty.Text = "➖ Decr Qty";
            this.btnDecrementQty.UseVisualStyleBackColor = false;
            this.btnDecrementQty.Click += new System.EventHandler(this.btnDecrementQty_Click);
            // 
            // lblChangeValue
            // 
            this.lblChangeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChangeValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblChangeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblChangeValue.Location = new System.Drawing.Point(240, 532);
            this.lblChangeValue.Name = "lblChangeValue";
            this.lblChangeValue.Size = new System.Drawing.Size(110, 20);
            this.lblChangeValue.TabIndex = 11;
            this.lblChangeValue.Text = "Rs 0.00";
            this.lblChangeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChange
            // 
            this.lblChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblChange.Location = new System.Drawing.Point(10, 532);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(115, 19);
            this.lblChange.TabIndex = 10;
            this.lblChange.Text = "Change Returned:";
            // 
            // txtReceived
            // 
            this.txtReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceived.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReceived.Location = new System.Drawing.Point(240, 497);
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.Size = new System.Drawing.Size(110, 25);
            this.txtReceived.TabIndex = 9;
            this.txtReceived.Text = "0";
            this.txtReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReceived.TextChanged += new System.EventHandler(this.txtReceived_TextChanged);
            // 
            // lblReceived
            // 
            this.lblReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReceived.AutoSize = true;
            this.lblReceived.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReceived.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblReceived.Location = new System.Drawing.Point(10, 500);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(101, 19);
            this.lblReceived.TabIndex = 8;
            this.lblReceived.Text = "Cash Received:";
            // 
            // cbPaymentMethod
            // 
            this.cbPaymentMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbPaymentMethod.FormattingEnabled = true;
            this.cbPaymentMethod.Location = new System.Drawing.Point(210, 462);
            this.cbPaymentMethod.Name = "cbPaymentMethod";
            this.cbPaymentMethod.Size = new System.Drawing.Size(140, 25);
            this.cbPaymentMethod.TabIndex = 7;
            this.cbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cbPaymentMethod_SelectedIndexChanged);
            // 
            // lblPayment
            // 
            this.lblPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPayment.AutoSize = true;
            this.lblPayment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPayment.Location = new System.Drawing.Point(10, 465);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(116, 19);
            this.lblPayment.TabIndex = 6;
            this.lblPayment.Text = "Payment Method:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiscount.Location = new System.Drawing.Point(240, 396);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(110, 25);
            this.txtDiscount.TabIndex = 5;
            this.txtDiscount.Text = "0";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDiscount.Location = new System.Drawing.Point(10, 399);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(91, 19);
            this.lblDiscount.TabIndex = 4;
            this.lblDiscount.Text = "Discount (Rs):";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.lblTotal.Location = new System.Drawing.Point(10, 429);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(340, 25);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Net Total: Rs 0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblSubtotal.Location = new System.Drawing.Point(10, 370);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(340, 20);
            this.lblSubtotal.TabIndex = 2;
            this.lblSubtotal.Text = "Subtotal: Rs 0.00";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvCart
            // 
            this.dgvCart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AllowUserToResizeRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCart.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCart.ColumnHeadersHeight = 30;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCart.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCart.Location = new System.Drawing.Point(10, 42);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(340, 318);
            this.dgvCart.TabIndex = 1;
            // 
            // lblCartHeader
            // 
            this.lblCartHeader.AutoSize = true;
            this.lblCartHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblCartHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblCartHeader.Location = new System.Drawing.Point(10, 15);
            this.lblCartHeader.Name = "lblCartHeader";
            this.lblCartHeader.Size = new System.Drawing.Size(107, 20);
            this.lblCartHeader.TabIndex = 0;
            this.lblCartHeader.Text = "Checkout Cart";

            // 
            // 
            // panelReceiptPreview
            // 
            this.panelReceiptPreview.BackColor = System.Drawing.Color.White;
            this.panelReceiptPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReceiptPreview.Controls.Add(this.lblReceiptHeader);
            this.panelReceiptPreview.Controls.Add(this.rtbReceipt);
            this.panelReceiptPreview.Controls.Add(this.btnSaveOrder);
            this.panelReceiptPreview.Controls.Add(this.btnPrintReceipt);
            this.panelReceiptPreview.Controls.Add(this.btnClearOrder);
            this.panelReceiptPreview.Controls.Add(this.lblReceiptHelp);
            this.panelReceiptPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelReceiptPreview.Location = new System.Drawing.Point(730, 0);
            this.panelReceiptPreview.Name = "panelReceiptPreview";
            this.panelReceiptPreview.Padding = new System.Windows.Forms.Padding(10);
            this.panelReceiptPreview.Size = new System.Drawing.Size(240, 640);
            this.panelReceiptPreview.AutoScroll = true;
            this.panelReceiptPreview.TabIndex = 3;
            // 
            // lblReceiptHeader
            // 
            this.lblReceiptHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblReceiptHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblReceiptHeader.Location = new System.Drawing.Point(10, 10);
            this.lblReceiptHeader.Name = "lblReceiptHeader";
            this.lblReceiptHeader.Size = new System.Drawing.Size(218, 23);
            this.lblReceiptHeader.TabIndex = 0;
            this.lblReceiptHeader.Text = "📝 Live Receipt Preview";
            this.lblReceiptHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbReceipt
            // 
            this.rtbReceipt.BackColor = System.Drawing.Color.White;
            this.rtbReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbReceipt.Font = new System.Drawing.Font("Courier New", 9F);
            this.rtbReceipt.Location = new System.Drawing.Point(10, 38);
            this.rtbReceipt.Name = "rtbReceipt";
            this.rtbReceipt.ReadOnly = true;
            this.rtbReceipt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbReceipt.Size = new System.Drawing.Size(218, 350);
            this.rtbReceipt.TabIndex = 1;
            this.rtbReceipt.Text = "";
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSaveOrder.FlatAppearance.BorderSize = 0;
            this.btnSaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveOrder.ForeColor = System.Drawing.Color.White;
            this.btnSaveOrder.Location = new System.Drawing.Point(10, 400);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(218, 35);
            this.btnSaveOrder.TabIndex = 2;
            this.btnSaveOrder.Text = "💾 Save Order (No Print)";
            this.btnSaveOrder.UseVisualStyleBackColor = false;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnPrintReceipt.FlatAppearance.BorderSize = 0;
            this.btnPrintReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintReceipt.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnPrintReceipt.ForeColor = System.Drawing.Color.White;
            this.btnPrintReceipt.Location = new System.Drawing.Point(10, 445);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(218, 45);
            this.btnPrintReceipt.TabIndex = 3;
            this.btnPrintReceipt.Text = "🖨️ Print & Save Receipt";
            this.btnPrintReceipt.UseVisualStyleBackColor = false;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // btnClearOrder
            // 
            this.btnClearOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnClearOrder.FlatAppearance.BorderSize = 0;
            this.btnClearOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearOrder.ForeColor = System.Drawing.Color.White;
            this.btnClearOrder.Location = new System.Drawing.Point(10, 500);
            this.btnClearOrder.Name = "btnClearOrder";
            this.btnClearOrder.Size = new System.Drawing.Size(218, 35);
            this.btnClearOrder.TabIndex = 4;
            this.btnClearOrder.Text = "🧹 Clear / Reset POS";
            this.btnClearOrder.UseVisualStyleBackColor = false;
            this.btnClearOrder.Click += new System.EventHandler(this.btnClearOrder_Click);
            // 
            // lblReceiptHelp
            // 
            this.lblReceiptHelp.Font = new System.Drawing.Font("Segoe UI Italic", 8.25F);
            this.lblReceiptHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblReceiptHelp.Location = new System.Drawing.Point(10, 545);
            this.lblReceiptHelp.Name = "lblReceiptHelp";
            this.lblReceiptHelp.Size = new System.Drawing.Size(218, 35);
            this.lblReceiptHelp.TabIndex = 5;
            this.lblReceiptHelp.Text = "* Live receipt updates as you configure items. Save updates database instantly.";
            this.lblReceiptHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_POS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelReceiptPreview);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UC_POS";
            this.Size = new System.Drawing.Size(1050, 640);
            this.Load += new System.EventHandler(this.UC_POS_Load);
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelMiddle.ResumeLayout(false);
            this.panelMiddle.PerformLayout();
            this.gbLadiesOptions.ResumeLayout(false);
            this.gbLadiesOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelReceiptPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
