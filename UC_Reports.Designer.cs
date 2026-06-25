namespace GarmentShopPos
{
    partial class UC_Reports
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Panel panelKpi;
        private System.Windows.Forms.Panel kpiRevenue;
        private System.Windows.Forms.Label lblRevVal;
        private System.Windows.Forms.Label lblRevTitle;
        private System.Windows.Forms.Panel kpiProfit;
        private System.Windows.Forms.Label lblProfitVal;
        private System.Windows.Forms.Label lblProfitTitle;
        private System.Windows.Forms.Panel kpiOrders;
        private System.Windows.Forms.Label lblOrdersVal;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.TabControl tabControlReports;
        private System.Windows.Forms.TabPage tabBestSellers;
        private System.Windows.Forms.DataGridView dgvBestSellers;
        private System.Windows.Forms.TabPage tabSection;
        private System.Windows.Forms.DataGridView dgvSection;
        private System.Windows.Forms.TabPage tabSalesmen;
        private System.Windows.Forms.DataGridView dgvSalesmen;
        private System.Windows.Forms.TabPage tabFinancials;
        private System.Windows.Forms.DataGridView dgvFinancials;

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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelKpi = new System.Windows.Forms.Panel();
            this.kpiOrders = new System.Windows.Forms.Panel();
            this.lblOrdersVal = new System.Windows.Forms.Label();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.kpiProfit = new System.Windows.Forms.Panel();
            this.lblProfitVal = new System.Windows.Forms.Label();
            this.lblProfitTitle = new System.Windows.Forms.Label();
            this.kpiRevenue = new System.Windows.Forms.Panel();
            this.lblRevVal = new System.Windows.Forms.Label();
            this.lblRevTitle = new System.Windows.Forms.Label();
            this.tabControlReports = new System.Windows.Forms.TabControl();
            this.tabBestSellers = new System.Windows.Forms.TabPage();
            this.dgvBestSellers = new System.Windows.Forms.DataGridView();
            this.tabSection = new System.Windows.Forms.TabPage();
            this.dgvSection = new System.Windows.Forms.DataGridView();
            this.tabSalesmen = new System.Windows.Forms.TabPage();
            this.dgvSalesmen = new System.Windows.Forms.DataGridView();
            this.tabFinancials = new System.Windows.Forms.TabPage();
            this.dgvFinancials = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelKpi.SuspendLayout();
            this.kpiOrders.SuspendLayout();
            this.kpiProfit.SuspendLayout();
            this.kpiRevenue.SuspendLayout();
            this.tabControlReports.SuspendLayout();
            this.tabBestSellers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestSellers)).BeginInit();
            this.tabSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSection)).BeginInit();
            this.tabSalesmen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesmen)).BeginInit();
            this.tabFinancials.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinancials)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnExportPdf);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.dtpEnd);
            this.panelTop.Controls.Add(this.lblEnd);
            this.panelTop.Controls.Add(this.dtpStart);
            this.panelTop.Controls.Add(this.lblStart);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1050, 65);
            this.panelTop.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(723, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 27);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "🔄 Refresh Reports";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnExportPdf.FlatAppearance.BorderSize = 0;
            this.btnExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPdf.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnExportPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportPdf.Location = new System.Drawing.Point(873, 17);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(150, 27);
            this.btnExportPdf.TabIndex = 6;
            this.btnExportPdf.Text = "📄 Export Report (PDF)";
            this.btnExportPdf.UseVisualStyleBackColor = false;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(581, 19);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(120, 23);
            this.dtpEnd.TabIndex = 4;
            // 
            // lblEnd
            // 
            this.lblEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblEnd.Location = new System.Drawing.Point(513, 21);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(65, 17);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "End Date:";
            // 
            // dtpStart
            // 
            this.dtpStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(371, 19);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(120, 23);
            this.dtpStart.TabIndex = 2;
            // 
            // lblStart
            // 
            this.lblStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblStart.Location = new System.Drawing.Point(297, 21);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(71, 17);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "Start Date:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Business Performance Reports";
            // 
            // panelKpi
            // 
            this.panelKpi.Controls.Add(this.kpiOrders);
            this.panelKpi.Controls.Add(this.kpiProfit);
            this.panelKpi.Controls.Add(this.kpiRevenue);
            this.panelKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKpi.Location = new System.Drawing.Point(0, 65);
            this.panelKpi.Name = "panelKpi";
            this.panelKpi.Size = new System.Drawing.Size(1050, 110);
            this.panelKpi.TabIndex = 1;
            // 
            // kpiOrders
            // 
            this.kpiOrders.BackColor = System.Drawing.Color.White;
            this.kpiOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiOrders.Controls.Add(this.lblOrdersVal);
            this.kpiOrders.Controls.Add(this.lblOrdersTitle);
            this.kpiOrders.Location = new System.Drawing.Point(704, 10);
            this.kpiOrders.Name = "kpiOrders";
            this.kpiOrders.Size = new System.Drawing.Size(320, 90);
            this.kpiOrders.TabIndex = 2;
            // 
            // lblOrdersVal
            // 
            this.lblOrdersVal.AutoSize = true;
            this.lblOrdersVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblOrdersVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblOrdersVal.Location = new System.Drawing.Point(18, 38);
            this.lblOrdersVal.Name = "lblOrdersVal";
            this.lblOrdersVal.Size = new System.Drawing.Size(26, 30);
            this.lblOrdersVal.TabIndex = 1;
            this.lblOrdersVal.Text = "0";
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(122)))), ((int)(((byte)(135)))));
            this.lblOrdersTitle.Location = new System.Drawing.Point(18, 14);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(101, 15);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "📦 Orders Count";
            // 
            // kpiProfit
            // 
            this.kpiProfit.BackColor = System.Drawing.Color.White;
            this.kpiProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiProfit.Controls.Add(this.lblProfitVal);
            this.kpiProfit.Controls.Add(this.lblProfitTitle);
            this.kpiProfit.Location = new System.Drawing.Point(363, 10);
            this.kpiProfit.Name = "kpiProfit";
            this.kpiProfit.Size = new System.Drawing.Size(320, 90);
            this.kpiProfit.TabIndex = 1;
            // 
            // lblProfitVal
            // 
            this.lblProfitVal.AutoSize = true;
            this.lblProfitVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblProfitVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblProfitVal.Location = new System.Drawing.Point(18, 38);
            this.lblProfitVal.Name = "lblProfitVal";
            this.lblProfitVal.Size = new System.Drawing.Size(89, 30);
            this.lblProfitVal.TabIndex = 1;
            this.lblProfitVal.Text = "Rs 0.00";
            // 
            // lblProfitTitle
            // 
            this.lblProfitTitle.AutoSize = true;
            this.lblProfitTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(122)))), ((int)(((byte)(135)))));
            this.lblProfitTitle.Location = new System.Drawing.Point(18, 14);
            this.lblProfitTitle.Name = "lblProfitTitle";
            this.lblProfitTitle.Size = new System.Drawing.Size(130, 15);
            this.lblProfitTitle.TabIndex = 0;
            this.lblProfitTitle.Text = "📈 Gross Net Profit (Rs)";
            // 
            // kpiRevenue
            // 
            this.kpiRevenue.BackColor = System.Drawing.Color.White;
            this.kpiRevenue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiRevenue.Controls.Add(this.lblRevVal);
            this.kpiRevenue.Controls.Add(this.lblRevTitle);
            this.kpiRevenue.Location = new System.Drawing.Point(22, 10);
            this.kpiRevenue.Name = "kpiRevenue";
            this.kpiRevenue.Size = new System.Drawing.Size(320, 90);
            this.kpiRevenue.TabIndex = 0;
            // 
            // lblRevVal
            // 
            this.lblRevVal.AutoSize = true;
            this.lblRevVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRevVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.lblRevVal.Location = new System.Drawing.Point(18, 38);
            this.lblRevVal.Name = "lblRevVal";
            this.lblRevVal.Size = new System.Drawing.Size(89, 30);
            this.lblRevVal.TabIndex = 1;
            this.lblRevVal.Text = "Rs 0.00";
            // 
            // lblRevTitle
            // 
            this.lblRevTitle.AutoSize = true;
            this.lblRevTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(122)))), ((int)(((byte)(135)))));
            this.lblRevTitle.Location = new System.Drawing.Point(18, 14);
            this.lblRevTitle.Name = "lblRevTitle";
            this.lblRevTitle.Size = new System.Drawing.Size(117, 15);
            this.lblRevTitle.TabIndex = 0;
            this.lblRevTitle.Text = "💰 Total Revenue (Rs)";
            // 
            // tabControlReports
            // 
            this.tabControlReports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlReports.Controls.Add(this.tabBestSellers);
            this.tabControlReports.Controls.Add(this.tabSection);
            this.tabControlReports.Controls.Add(this.tabSalesmen);
            this.tabControlReports.Controls.Add(this.tabFinancials);
            this.tabControlReports.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabControlReports.Location = new System.Drawing.Point(22, 191);
            this.tabControlReports.Name = "tabControlReports";
            this.tabControlReports.SelectedIndex = 0;
            this.tabControlReports.Size = new System.Drawing.Size(1006, 431);
            this.tabControlReports.TabIndex = 2;
            // 
            // tabBestSellers
            // 
            this.tabBestSellers.Controls.Add(this.dgvBestSellers);
            this.tabBestSellers.Location = new System.Drawing.Point(4, 26);
            this.tabBestSellers.Name = "tabBestSellers";
            this.tabBestSellers.Padding = new System.Windows.Forms.Padding(10);
            this.tabBestSellers.Size = new System.Drawing.Size(998, 401);
            this.tabBestSellers.TabIndex = 0;
            this.tabBestSellers.Text = "Best Selling Fabrics";
            this.tabBestSellers.UseVisualStyleBackColor = true;
            // 
            // dgvBestSellers
            // 
            this.dgvBestSellers.AllowUserToAddRows = false;
            this.dgvBestSellers.AllowUserToDeleteRows = false;
            this.dgvBestSellers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBestSellers.BackgroundColor = System.Drawing.Color.White;
            this.dgvBestSellers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBestSellers.ColumnHeadersHeight = 30;
            this.dgvBestSellers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBestSellers.Location = new System.Drawing.Point(10, 10);
            this.dgvBestSellers.Name = "dgvBestSellers";
            this.dgvBestSellers.RowHeadersVisible = false;
            this.dgvBestSellers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvBestSellers.Size = new System.Drawing.Size(978, 381);
            this.dgvBestSellers.TabIndex = 0;
            // 
            // tabSection
            // 
            this.tabSection.Controls.Add(this.dgvSection);
            this.tabSection.Location = new System.Drawing.Point(4, 26);
            this.tabSection.Name = "tabSection";
            this.tabSection.Padding = new System.Windows.Forms.Padding(10);
            this.tabSection.Size = new System.Drawing.Size(998, 401);
            this.tabSection.TabIndex = 1;
            this.tabSection.Text = "Section Performance (Gents vs Ladies)";
            this.tabSection.UseVisualStyleBackColor = true;
            // 
            // dgvSection
            // 
            this.dgvSection.AllowUserToAddRows = false;
            this.dgvSection.AllowUserToDeleteRows = false;
            this.dgvSection.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSection.BackgroundColor = System.Drawing.Color.White;
            this.dgvSection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSection.ColumnHeadersHeight = 30;
            this.dgvSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSection.Location = new System.Drawing.Point(10, 10);
            this.dgvSection.Name = "dgvSection";
            this.dgvSection.RowHeadersVisible = false;
            this.dgvSection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvSection.Size = new System.Drawing.Size(978, 381);
            this.dgvSection.TabIndex = 0;
            // 
            // tabSalesmen
            // 
            this.tabSalesmen.Controls.Add(this.dgvSalesmen);
            this.tabSalesmen.Location = new System.Drawing.Point(4, 26);
            this.tabSalesmen.Name = "tabSalesmen";
            this.tabSalesmen.Padding = new System.Windows.Forms.Padding(10);
            this.tabSalesmen.Size = new System.Drawing.Size(998, 401);
            this.tabSalesmen.TabIndex = 2;
            this.tabSalesmen.Text = "Salesman Performance";
            this.tabSalesmen.UseVisualStyleBackColor = true;
            // 
            // dgvSalesmen
            // 
            this.dgvSalesmen.AllowUserToAddRows = false;
            this.dgvSalesmen.AllowUserToDeleteRows = false;
            this.dgvSalesmen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSalesmen.BackgroundColor = System.Drawing.Color.White;
            this.dgvSalesmen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSalesmen.ColumnHeadersHeight = 30;
            this.dgvSalesmen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSalesmen.Location = new System.Drawing.Point(10, 10);
            this.dgvSalesmen.Name = "dgvSalesmen";
            this.dgvSalesmen.RowHeadersVisible = false;
            this.dgvSalesmen.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvSalesmen.Size = new System.Drawing.Size(978, 381);
            this.dgvSalesmen.TabIndex = 0;
            // 
            // tabFinancials
            // 
            this.tabFinancials.Controls.Add(this.dgvFinancials);
            this.tabFinancials.Location = new System.Drawing.Point(4, 26);
            this.tabFinancials.Name = "tabFinancials";
            this.tabFinancials.Padding = new System.Windows.Forms.Padding(10);
            this.tabFinancials.Size = new System.Drawing.Size(998, 401);
            this.tabFinancials.TabIndex = 3;
            this.tabFinancials.Text = "Daily Financial Summary";
            this.tabFinancials.UseVisualStyleBackColor = true;
            // 
            // dgvFinancials
            // 
            this.dgvFinancials.AllowUserToAddRows = false;
            this.dgvFinancials.AllowUserToDeleteRows = false;
            this.dgvFinancials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFinancials.BackgroundColor = System.Drawing.Color.White;
            this.dgvFinancials.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFinancials.ColumnHeadersHeight = 30;
            this.dgvFinancials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFinancials.Location = new System.Drawing.Point(10, 10);
            this.dgvFinancials.Name = "dgvFinancials";
            this.dgvFinancials.RowHeadersVisible = false;
            this.dgvFinancials.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvFinancials.Size = new System.Drawing.Size(978, 381);
            this.dgvFinancials.TabIndex = 0;
            // 
            // UC_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.tabControlReports);
            this.Controls.Add(this.panelKpi);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UC_Reports";
            this.Size = new System.Drawing.Size(1050, 640);
            this.Load += new System.EventHandler(this.UC_Reports_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelKpi.ResumeLayout(false);
            this.kpiOrders.ResumeLayout(false);
            this.kpiOrders.PerformLayout();
            this.kpiProfit.ResumeLayout(false);
            this.kpiProfit.PerformLayout();
            this.kpiRevenue.ResumeLayout(false);
            this.kpiRevenue.PerformLayout();
            this.tabControlReports.ResumeLayout(false);
            this.tabBestSellers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestSellers)).EndInit();
            this.tabSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSection)).EndInit();
            this.tabSalesmen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesmen)).EndInit();
            this.tabFinancials.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinancials)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
