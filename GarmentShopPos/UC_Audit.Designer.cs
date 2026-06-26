namespace GarmentShopPos
{
    partial class UC_Audit
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlAudit;
        private System.Windows.Forms.TabPage tabReconcile;
        private System.Windows.Forms.TabPage tabWastage;
        
        // Reconcile Tab Controls
        private System.Windows.Forms.Label lblRProduct;
        private System.Windows.Forms.ComboBox cbRProduct;
        private System.Windows.Forms.Label lblRSystemQty;
        private System.Windows.Forms.Label lblRSystemQtyVal;
        private System.Windows.Forms.Label lblRAdjustmentQty;
        private System.Windows.Forms.TextBox txtRAdjustmentQty;
        private System.Windows.Forms.Label lblRNewStock;
        private System.Windows.Forms.Label lblRNewStockVal;
        private System.Windows.Forms.RadioButton rbExtra;
        private System.Windows.Forms.RadioButton rbDiff;
        private System.Windows.Forms.Button btnReconcile;
        
        // Wastage Tab Controls
        private System.Windows.Forms.Label lblWProduct;
        private System.Windows.Forms.ComboBox cbWProduct;
        private System.Windows.Forms.Label lblWSysQty;
        private System.Windows.Forms.Label lblWSysQtyVal;
        private System.Windows.Forms.Label lblWQty;
        private System.Windows.Forms.TextBox txtWQty;
        private System.Windows.Forms.Label lblWReason;
        private System.Windows.Forms.ComboBox cbWReason;
        private System.Windows.Forms.Button btnLogWastage;
        
        // Grid View
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Label lblLogsTitle;
        private System.Windows.Forms.RadioButton rbShowWastage;
        private System.Windows.Forms.RadioButton rbShowPurchases;
        private System.Windows.Forms.DateTimePicker dtpLogStart;
        private System.Windows.Forms.DateTimePicker dtpLogEnd;
        private System.Windows.Forms.Label lblLogStart;
        private System.Windows.Forms.Label lblLogEnd;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSearchLog;

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
            this.tabControlAudit = new System.Windows.Forms.TabControl();
            this.tabReconcile = new System.Windows.Forms.TabPage();
            this.btnReconcile = new System.Windows.Forms.Button();
            this.lblRNewStockVal = new System.Windows.Forms.Label();
            this.lblRNewStock = new System.Windows.Forms.Label();
            this.txtRAdjustmentQty = new System.Windows.Forms.TextBox();
            this.lblRAdjustmentQty = new System.Windows.Forms.Label();
            this.rbExtra = new System.Windows.Forms.RadioButton();
            this.rbDiff = new System.Windows.Forms.RadioButton();
            this.cbRProduct = new System.Windows.Forms.ComboBox();
            this.lblRProduct = new System.Windows.Forms.Label();
            this.lblRSystemQtyVal = new System.Windows.Forms.Label();
            this.lblRSystemQty = new System.Windows.Forms.Label();
            this.tabWastage = new System.Windows.Forms.TabPage();
            this.btnLogWastage = new System.Windows.Forms.Button();
            this.cbWReason = new System.Windows.Forms.ComboBox();
            this.lblWReason = new System.Windows.Forms.Label();
            this.txtWQty = new System.Windows.Forms.TextBox();
            this.lblWQty = new System.Windows.Forms.Label();
            this.lblWSysQtyVal = new System.Windows.Forms.Label();
            this.lblWSysQty = new System.Windows.Forms.Label();
            this.cbWProduct = new System.Windows.Forms.ComboBox();
            this.lblWProduct = new System.Windows.Forms.Label();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.lblLogsTitle = new System.Windows.Forms.Label();
            this.rbShowWastage = new System.Windows.Forms.RadioButton();
            this.rbShowPurchases = new System.Windows.Forms.RadioButton();
            this.dtpLogStart = new System.Windows.Forms.DateTimePicker();
            this.dtpLogEnd = new System.Windows.Forms.DateTimePicker();
            this.lblLogStart = new System.Windows.Forms.Label();
            this.lblLogEnd = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSearchLog = new System.Windows.Forms.Button();
            this.tabControlAudit.SuspendLayout();
            this.tabReconcile.SuspendLayout();
            this.tabWastage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAudit
            // 
            this.tabControlAudit.Controls.Add(this.tabReconcile);
            this.tabControlAudit.Controls.Add(this.tabWastage);
            this.tabControlAudit.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.tabControlAudit.Location = new System.Drawing.Point(22, 67);
            this.tabControlAudit.Name = "tabControlAudit";
            this.tabControlAudit.SelectedIndex = 0;
            this.tabControlAudit.Size = new System.Drawing.Size(1006, 252);
            this.tabControlAudit.TabIndex = 0;
            // 
            // tabReconcile
            // 
            this.tabReconcile.BackColor = System.Drawing.Color.White;
            this.tabReconcile.Controls.Add(this.rbDiff);
            this.tabReconcile.Controls.Add(this.rbExtra);
            this.tabReconcile.Controls.Add(this.btnReconcile);
            this.tabReconcile.Controls.Add(this.lblRNewStockVal);
            this.tabReconcile.Controls.Add(this.lblRNewStock);
            this.tabReconcile.Controls.Add(this.txtRAdjustmentQty);
            this.tabReconcile.Controls.Add(this.lblRAdjustmentQty);
            this.tabReconcile.Controls.Add(this.lblRSystemQtyVal);
            this.tabReconcile.Controls.Add(this.lblRSystemQty);
            this.tabReconcile.Controls.Add(this.cbRProduct);
            this.tabReconcile.Controls.Add(this.lblRProduct);
            this.tabReconcile.Location = new System.Drawing.Point(4, 26);
            this.tabReconcile.Name = "tabReconcile";
            this.tabReconcile.Padding = new System.Windows.Forms.Padding(20);
            this.tabReconcile.Size = new System.Drawing.Size(998, 222);
            this.tabReconcile.TabIndex = 0;
            this.tabReconcile.Text = "Count Fabric Stock (Check)";
            // 
            // btnReconcile
            // 
            this.btnReconcile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnReconcile.FlatAppearance.BorderSize = 0;
            this.btnReconcile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReconcile.ForeColor = System.Drawing.Color.White;
            this.btnReconcile.Location = new System.Drawing.Point(620, 107);
            this.btnReconcile.Name = "btnReconcile";
            this.btnReconcile.Size = new System.Drawing.Size(180, 36);
            this.btnReconcile.TabIndex = 8;
            this.btnReconcile.Text = "📝 Update Stock Record";
            this.btnReconcile.UseVisualStyleBackColor = false;
            this.btnReconcile.Click += new System.EventHandler(this.btnReconcile_Click);
            // 
            // lblRNewStockVal
            // 
            this.lblRNewStockVal.AutoSize = true;
            this.lblRNewStockVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRNewStockVal.ForeColor = System.Drawing.Color.Black;
            this.lblRNewStockVal.Location = new System.Drawing.Point(410, 112);
            this.lblRNewStockVal.Name = "lblRNewStockVal";
            this.lblRNewStockVal.Size = new System.Drawing.Size(38, 20);
            this.lblRNewStockVal.TabIndex = 7;
            this.lblRNewStockVal.Text = "0.00 yards";
            // 
            // lblRNewStock
            // 
            this.lblRNewStock.AutoSize = true;
            this.lblRNewStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblRNewStock.Location = new System.Drawing.Point(407, 90);
            this.lblRNewStock.Name = "lblRNewStock";
            this.lblRNewStock.Size = new System.Drawing.Size(158, 19);
            this.lblRNewStock.TabIndex = 6;
            this.lblRNewStock.Text = "Adjusted Stock Preview:";
            // 
            // txtRAdjustmentQty
            // 
            this.txtRAdjustmentQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRAdjustmentQty.Location = new System.Drawing.Point(23, 112);
            this.txtRAdjustmentQty.Name = "txtRAdjustmentQty";
            this.txtRAdjustmentQty.Size = new System.Drawing.Size(160, 25);
            this.txtRAdjustmentQty.TabIndex = 5;
            this.txtRAdjustmentQty.TextChanged += new System.EventHandler(this.txtRAdjustmentQty_TextChanged);
            // 
            // lblRAdjustmentQty
            // 
            this.lblRAdjustmentQty.AutoSize = true;
            this.lblRAdjustmentQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblRAdjustmentQty.Location = new System.Drawing.Point(20, 90);
            this.lblRAdjustmentQty.Name = "lblRAdjustmentQty";
            this.lblRAdjustmentQty.Size = new System.Drawing.Size(175, 19);
            this.lblRAdjustmentQty.TabIndex = 4;
            this.lblRAdjustmentQty.Text = "Adjustment Yards (Count):*";
            // 
            // rbExtra
            // 
            this.rbExtra.AutoSize = true;
            this.rbExtra.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.rbExtra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.rbExtra.Location = new System.Drawing.Point(200, 98);
            this.rbExtra.Name = "rbExtra";
            this.rbExtra.Size = new System.Drawing.Size(136, 21);
            this.rbExtra.TabIndex = 9;
            this.rbExtra.TabStop = true;
            this.rbExtra.Text = "Extra / Surplus (+)";
            this.rbExtra.UseVisualStyleBackColor = true;
            this.rbExtra.CheckedChanged += new System.EventHandler(this.rbExtra_CheckedChanged);
            // 
            // rbDiff
            // 
            this.rbDiff.AutoSize = true;
            this.rbDiff.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.rbDiff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.rbDiff.Location = new System.Drawing.Point(200, 122);
            this.rbDiff.Name = "rbDiff";
            this.rbDiff.Size = new System.Drawing.Size(133, 21);
            this.rbDiff.TabIndex = 10;
            this.rbDiff.TabStop = true;
            this.rbDiff.Text = "Diff / Shortage (-)";
            this.rbDiff.UseVisualStyleBackColor = true;
            this.rbDiff.CheckedChanged += new System.EventHandler(this.rbDiff_CheckedChanged);
            // 
            // lblRSystemQtyVal
            // 
            this.lblRSystemQtyVal.AutoSize = true;
            this.lblRSystemQtyVal.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblRSystemQtyVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.lblRSystemQtyVal.Location = new System.Drawing.Point(743, 40);
            this.lblRSystemQtyVal.Name = "lblRSystemQtyVal";
            this.lblRSystemQtyVal.Size = new System.Drawing.Size(38, 20);
            this.lblRSystemQtyVal.TabIndex = 3;
            this.lblRSystemQtyVal.Text = "0.00";
            // 
            // lblRSystemQty
            // 
            this.lblRSystemQty.AutoSize = true;
            this.lblRSystemQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblRSystemQty.Location = new System.Drawing.Point(616, 41);
            this.lblRSystemQty.Name = "lblRSystemQty";
            this.lblRSystemQty.Size = new System.Drawing.Size(110, 19);
            this.lblRSystemQty.TabIndex = 2;
            this.lblRSystemQty.Text = "Yards in System:";
            // 
            // cbRProduct
            // 
            this.cbRProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbRProduct.FormattingEnabled = true;
            this.cbRProduct.Location = new System.Drawing.Point(23, 38);
            this.cbRProduct.Name = "cbRProduct";
            this.cbRProduct.Size = new System.Drawing.Size(560, 25);
            this.cbRProduct.TabIndex = 1;
            this.cbRProduct.SelectedIndexChanged += new System.EventHandler(this.cbRProduct_SelectedIndexChanged);
            // 
            // lblRProduct
            // 
            this.lblRProduct.AutoSize = true;
            this.lblRProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblRProduct.Location = new System.Drawing.Point(20, 16);
            this.lblRProduct.Name = "lblRProduct";
            this.lblRProduct.Size = new System.Drawing.Size(133, 19);
            this.lblRProduct.TabIndex = 0;
            this.lblRProduct.Text = "Select Fabric / Suit:*";
            // 
            // tabWastage
            // 
            this.tabWastage.BackColor = System.Drawing.Color.White;
            this.tabWastage.Controls.Add(this.btnLogWastage);
            this.tabWastage.Controls.Add(this.cbWReason);
            this.tabWastage.Controls.Add(this.lblWReason);
            this.tabWastage.Controls.Add(this.txtWQty);
            this.tabWastage.Controls.Add(this.lblWQty);
            this.tabWastage.Controls.Add(this.lblWSysQtyVal);
            this.tabWastage.Controls.Add(this.lblWSysQty);
            this.tabWastage.Controls.Add(this.cbWProduct);
            this.tabWastage.Controls.Add(this.lblWProduct);
            this.tabWastage.Location = new System.Drawing.Point(4, 26);
            this.tabWastage.Name = "tabWastage";
            this.tabWastage.Padding = new System.Windows.Forms.Padding(20);
            this.tabWastage.Size = new System.Drawing.Size(998, 222);
            this.tabWastage.TabIndex = 1;
            this.tabWastage.Text = "Record Damaged / Stolen Stock";
            // 
            // btnLogWastage
            // 
            this.btnLogWastage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.btnLogWastage.FlatAppearance.BorderSize = 0;
            this.btnLogWastage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogWastage.ForeColor = System.Drawing.Color.White;
            this.btnLogWastage.Location = new System.Drawing.Point(620, 110);
            this.btnLogWastage.Name = "btnLogWastage";
            this.btnLogWastage.Size = new System.Drawing.Size(180, 36);
            this.btnLogWastage.TabIndex = 8;
            this.btnLogWastage.Text = "🗑️ Save Damaged / Lost Yards";
            this.btnLogWastage.UseVisualStyleBackColor = false;
            this.btnLogWastage.Click += new System.EventHandler(this.btnLogWastage_Click);
            // 
            // cbWReason
            // 
            this.cbWReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWReason.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbWReason.FormattingEnabled = true;
            this.cbWReason.Location = new System.Drawing.Point(313, 115);
            this.cbWReason.Name = "cbWReason";
            this.cbWReason.Size = new System.Drawing.Size(270, 25);
            this.cbWReason.TabIndex = 7;
            // 
            // lblWReason
            // 
            this.lblWReason.AutoSize = true;
            this.lblWReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblWReason.Location = new System.Drawing.Point(310, 93);
            this.lblWReason.Name = "lblWReason";
            this.lblWReason.Size = new System.Drawing.Size(155, 19);
            this.lblWReason.TabIndex = 6;
            this.lblWReason.Text = "Reason for Damage/Loss:*";
            // 
            // txtWQty
            // 
            this.txtWQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWQty.Location = new System.Drawing.Point(23, 115);
            this.txtWQty.Name = "txtWQty";
            this.txtWQty.Size = new System.Drawing.Size(220, 25);
            this.txtWQty.TabIndex = 5;
            // 
            // lblWQty
            // 
            this.lblWQty.AutoSize = true;
            this.lblWQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblWQty.Location = new System.Drawing.Point(20, 93);
            this.lblWQty.Name = "lblWQty";
            this.lblWQty.Size = new System.Drawing.Size(155, 19);
            this.lblWQty.TabIndex = 4;
            this.lblWQty.Text = "Damaged / Lost Yards:*";
            // 
            // lblWSysQtyVal
            // 
            this.lblWSysQtyVal.AutoSize = true;
            this.lblWSysQtyVal.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblWSysQtyVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.lblWSysQtyVal.Location = new System.Drawing.Point(743, 40);
            this.lblWSysQtyVal.Name = "lblWSysQtyVal";
            this.lblWSysQtyVal.Size = new System.Drawing.Size(38, 20);
            this.lblWSysQtyVal.TabIndex = 3;
            this.lblWSysQtyVal.Text = "0.00";
            // 
            // lblWSysQty
            // 
            this.lblWSysQty.AutoSize = true;
            this.lblWSysQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblWSysQty.Location = new System.Drawing.Point(616, 41);
            this.lblWSysQty.Name = "lblWSysQty";
            this.lblWSysQty.Size = new System.Drawing.Size(110, 19);
            this.lblWSysQty.TabIndex = 2;
            this.lblWSysQty.Text = "Yards in System:";
            // 
            // cbWProduct
            // 
            this.cbWProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbWProduct.FormattingEnabled = true;
            this.cbWProduct.Location = new System.Drawing.Point(23, 38);
            this.cbWProduct.Name = "cbWProduct";
            this.cbWProduct.Size = new System.Drawing.Size(560, 25);
            this.cbWProduct.TabIndex = 1;
            this.cbWProduct.SelectedIndexChanged += new System.EventHandler(this.cbWProduct_SelectedIndexChanged);
            // 
            // lblWProduct
            // 
            this.lblWProduct.AutoSize = true;
            this.lblWProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblWProduct.Location = new System.Drawing.Point(20, 16);
            this.lblWProduct.Name = "lblWProduct";
            this.lblWProduct.Size = new System.Drawing.Size(133, 19);
            this.lblWProduct.TabIndex = 0;
            this.lblWProduct.Text = "Select Fabric / Suit:*";
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLogs.ColumnHeadersHeight = 30;
            this.dgvLogs.Location = new System.Drawing.Point(22, 398);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.Size = new System.Drawing.Size(1006, 222);
            this.dgvLogs.TabIndex = 1;
            this.dgvLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            // 
            // btnSearchLog
            // 
            this.btnSearchLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnSearchLog.FlatAppearance.BorderSize = 0;
            this.btnSearchLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchLog.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearchLog.ForeColor = System.Drawing.Color.White;
            this.btnSearchLog.Location = new System.Drawing.Point(890, 361);
            this.btnSearchLog.Name = "btnSearchLog";
            this.btnSearchLog.Size = new System.Drawing.Size(138, 26);
            this.btnSearchLog.TabIndex = 15;
            this.btnSearchLog.Text = "🔍 Search";
            this.btnSearchLog.UseVisualStyleBackColor = false;
            this.btnSearchLog.Click += new System.EventHandler(this.btnSearchLog_Click);
            // 
            // lblLogsTitle
            // 
            this.lblLogsTitle.AutoSize = true;
            this.lblLogsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblLogsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblLogsTitle.Location = new System.Drawing.Point(22, 335);
            this.lblLogsTitle.Name = "lblLogsTitle";
            this.lblLogsTitle.Size = new System.Drawing.Size(155, 20);
            this.lblLogsTitle.TabIndex = 2;
            this.lblLogsTitle.Text = "Recent Stock Changes";
            // 
            // rbShowWastage
            // 
            this.rbShowWastage.AutoSize = true;
            this.rbShowWastage.Checked = true;
            this.rbShowWastage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.rbShowWastage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.rbShowWastage.Location = new System.Drawing.Point(22, 365);
            this.rbShowWastage.Name = "rbShowWastage";
            this.rbShowWastage.Size = new System.Drawing.Size(200, 21);
            this.rbShowWastage.TabIndex = 3;
            this.rbShowWastage.TabStop = true;
            this.rbShowWastage.Text = "Show Damaged Stock";
            this.rbShowWastage.UseVisualStyleBackColor = true;
            this.rbShowWastage.CheckedChanged += new System.EventHandler(this.rbLogFilter_Changed);
            // 
            // rbShowPurchases
            // 
            this.rbShowPurchases.AutoSize = true;
            this.rbShowPurchases.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.rbShowPurchases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.rbShowPurchases.Location = new System.Drawing.Point(230, 365);
            this.rbShowPurchases.Name = "rbShowPurchases";
            this.rbShowPurchases.Size = new System.Drawing.Size(200, 21);
            this.rbShowPurchases.TabIndex = 4;
            this.rbShowPurchases.Text = "Show New Purchases";
            this.rbShowPurchases.UseVisualStyleBackColor = true;
            this.rbShowPurchases.CheckedChanged += new System.EventHandler(this.rbLogFilter_Changed);
            // 
            // lblLogStart
            // 
            this.lblLogStart.AutoSize = true;
            this.lblLogStart.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLogStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLogStart.Location = new System.Drawing.Point(450, 367);
            this.lblLogStart.Name = "lblLogStart";
            this.lblLogStart.Size = new System.Drawing.Size(80, 17);
            this.lblLogStart.TabIndex = 11;
            this.lblLogStart.Text = "From:";
            // 
            // dtpLogStart
            // 
            this.dtpLogStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpLogStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLogStart.Location = new System.Drawing.Point(530, 363);
            this.dtpLogStart.Name = "dtpLogStart";
            this.dtpLogStart.Size = new System.Drawing.Size(120, 23);
            this.dtpLogStart.TabIndex = 12;
            // 
            // lblLogEnd
            // 
            this.lblLogEnd.AutoSize = true;
            this.lblLogEnd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLogEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblLogEnd.Location = new System.Drawing.Point(670, 367);
            this.lblLogEnd.Name = "lblLogEnd";
            this.lblLogEnd.Size = new System.Drawing.Size(60, 17);
            this.lblLogEnd.TabIndex = 13;
            this.lblLogEnd.Text = "To:";
            // 
            // dtpLogEnd
            // 
            this.dtpLogEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpLogEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLogEnd.Location = new System.Drawing.Point(730, 363);
            this.dtpLogEnd.Name = "dtpLogEnd";
            this.dtpLogEnd.Size = new System.Drawing.Size(120, 23);
            this.dtpLogEnd.TabIndex = 14;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1050, 60);
            this.panelTop.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Fabric Stock Check & Damage Logs";
            // 
            // UC_Audit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.btnSearchLog);
            this.Controls.Add(this.dtpLogEnd);
            this.Controls.Add(this.lblLogEnd);
            this.Controls.Add(this.dtpLogStart);
            this.Controls.Add(this.lblLogStart);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.rbShowPurchases);
            this.Controls.Add(this.rbShowWastage);
            this.Controls.Add(this.lblLogsTitle);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.tabControlAudit);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UC_Audit";
            this.Size = new System.Drawing.Size(1050, 640);
            this.Load += new System.EventHandler(this.UC_Audit_Load);
            this.tabControlAudit.ResumeLayout(false);
            this.tabReconcile.ResumeLayout(false);
            this.tabReconcile.PerformLayout();
            this.tabWastage.ResumeLayout(false);
            this.tabWastage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
