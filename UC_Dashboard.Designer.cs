namespace GarmentShopPos
{
    partial class UC_Dashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelKpis;
        private System.Windows.Forms.Panel kpiDailySales;
        private System.Windows.Forms.Label lblDailySalesVal;
        private System.Windows.Forms.Label lblDailySalesTitle;
        private System.Windows.Forms.Panel kpiTodayOrders;
        private System.Windows.Forms.Label lblTodayOrdersVal;
        private System.Windows.Forms.Label lblTodayOrdersTitle;
        private System.Windows.Forms.Panel kpiLowStock;
        private System.Windows.Forms.Label lblLowStockVal;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.Label lblLowStockAlerts;
        private System.Windows.Forms.DataGridView dgvLowStockAlerts;
        private System.Windows.Forms.Label lblRecentSales;
        private System.Windows.Forms.DataGridView dgvRecentSales;

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
            this.panelKpis = new System.Windows.Forms.Panel();
            this.kpiLowStock = new System.Windows.Forms.Panel();
            this.lblLowStockVal = new System.Windows.Forms.Label();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this.kpiTodayOrders = new System.Windows.Forms.Panel();
            this.lblTodayOrdersVal = new System.Windows.Forms.Label();
            this.lblTodayOrdersTitle = new System.Windows.Forms.Label();
            this.kpiDailySales = new System.Windows.Forms.Panel();
            this.lblDailySalesVal = new System.Windows.Forms.Label();
            this.lblDailySalesTitle = new System.Windows.Forms.Label();
            this.lblLowStockAlerts = new System.Windows.Forms.Label();
            this.dgvLowStockAlerts = new System.Windows.Forms.DataGridView();
            this.lblRecentSales = new System.Windows.Forms.Label();
            this.dgvRecentSales = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelKpis.SuspendLayout();
            this.kpiLowStock.SuspendLayout();
            this.kpiTodayOrders.SuspendLayout();
            this.kpiDailySales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStockAlerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1050, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(183, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard Overview";
            // 
            // panelKpis
            // 
            this.panelKpis.Controls.Add(this.kpiLowStock);
            this.panelKpis.Controls.Add(this.kpiTodayOrders);
            this.panelKpis.Controls.Add(this.kpiDailySales);
            this.panelKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKpis.Location = new System.Drawing.Point(0, 60);
            this.panelKpis.Name = "panelKpis";
            this.panelKpis.Size = new System.Drawing.Size(1050, 110);
            this.panelKpis.TabIndex = 1;
            // 
            // kpiLowStock
            // 
            this.kpiLowStock.BackColor = System.Drawing.Color.White;
            this.kpiLowStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiLowStock.Controls.Add(this.lblLowStockVal);
            this.kpiLowStock.Controls.Add(this.lblLowStockTitle);
            this.kpiLowStock.Location = new System.Drawing.Point(704, 10);
            this.kpiLowStock.Name = "kpiLowStock";
            this.kpiLowStock.Size = new System.Drawing.Size(320, 90);
            this.kpiLowStock.TabIndex = 3;
            // 
            // lblLowStockVal
            // 
            this.lblLowStockVal.AutoSize = true;
            this.lblLowStockVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLowStockVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.lblLowStockVal.Location = new System.Drawing.Point(18, 38);
            this.lblLowStockVal.Name = "lblLowStockVal";
            this.lblLowStockVal.Size = new System.Drawing.Size(26, 30);
            this.lblLowStockVal.TabIndex = 1;
            this.lblLowStockVal.Text = "0";
            // 
            // lblLowStockTitle
            // 
            this.lblLowStockTitle.AutoSize = true;
            this.lblLowStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(122)))), ((int)(((byte)(135)))));
            this.lblLowStockTitle.Location = new System.Drawing.Point(18, 14);
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.Size = new System.Drawing.Size(130, 15);
            this.lblLowStockTitle.TabIndex = 0;
            this.lblLowStockTitle.Text = "⚠️ Low Stock Warning";
            // 
            // kpiTodayOrders
            // 
            this.kpiTodayOrders.BackColor = System.Drawing.Color.White;
            this.kpiTodayOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiTodayOrders.Controls.Add(this.lblTodayOrdersVal);
            this.kpiTodayOrders.Controls.Add(this.lblTodayOrdersTitle);
            this.kpiTodayOrders.Location = new System.Drawing.Point(363, 10);
            this.kpiTodayOrders.Name = "kpiTodayOrders";
            this.kpiTodayOrders.Size = new System.Drawing.Size(320, 90);
            this.kpiTodayOrders.TabIndex = 2;
            // 
            // lblTodayOrdersVal
            // 
            this.lblTodayOrdersVal.AutoSize = true;
            this.lblTodayOrdersVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTodayOrdersVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblTodayOrdersVal.Location = new System.Drawing.Point(18, 38);
            this.lblTodayOrdersVal.Name = "lblTodayOrdersVal";
            this.lblTodayOrdersVal.Size = new System.Drawing.Size(26, 30);
            this.lblTodayOrdersVal.TabIndex = 1;
            this.lblTodayOrdersVal.Text = "0";
            // 
            // lblTodayOrdersTitle
            // 
            this.lblTodayOrdersTitle.AutoSize = true;
            this.lblTodayOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(122)))), ((int)(((byte)(135)))));
            this.lblTodayOrdersTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTodayOrdersTitle.Name = "lblTodayOrdersTitle";
            this.lblTodayOrdersTitle.Size = new System.Drawing.Size(100, 15);
            this.lblTodayOrdersTitle.TabIndex = 0;
            this.lblTodayOrdersTitle.Text = "📦 Today\'s Orders";
            // 
            // kpiDailySales
            // 
            this.kpiDailySales.BackColor = System.Drawing.Color.White;
            this.kpiDailySales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpiDailySales.Controls.Add(this.lblDailySalesVal);
            this.kpiDailySales.Controls.Add(this.lblDailySalesTitle);
            this.kpiDailySales.Location = new System.Drawing.Point(22, 10);
            this.kpiDailySales.Name = "kpiDailySales";
            this.kpiDailySales.Size = new System.Drawing.Size(320, 90);
            this.kpiDailySales.TabIndex = 1;
            // 
            // lblDailySalesVal
            // 
            this.lblDailySalesVal.AutoSize = true;
            this.lblDailySalesVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDailySalesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.lblDailySalesVal.Location = new System.Drawing.Point(18, 38);
            this.lblDailySalesVal.Name = "lblDailySalesVal";
            this.lblDailySalesVal.Size = new System.Drawing.Size(89, 30);
            this.lblDailySalesVal.TabIndex = 1;
            this.lblDailySalesVal.Text = "Rs 0.00";
            // 
            // lblDailySalesTitle
            // 
            this.lblDailySalesTitle.AutoSize = true;
            this.lblDailySalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(122)))), ((int)(((byte)(135)))));
            this.lblDailySalesTitle.Location = new System.Drawing.Point(18, 14);
            this.lblDailySalesTitle.Name = "lblDailySalesTitle";
            this.lblDailySalesTitle.Size = new System.Drawing.Size(95, 15);
            this.lblDailySalesTitle.TabIndex = 0;
            this.lblDailySalesTitle.Text = "💰 Today\'s Revenue";
            // 
            // lblLowStockAlerts
            // 
            this.lblLowStockAlerts.AutoSize = true;
            this.lblLowStockAlerts.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblLowStockAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.lblLowStockAlerts.Location = new System.Drawing.Point(22, 190);
            this.lblLowStockAlerts.Name = "lblLowStockAlerts";
            this.lblLowStockAlerts.Size = new System.Drawing.Size(204, 19);
            this.lblLowStockAlerts.TabIndex = 2;
            this.lblLowStockAlerts.Text = "⚠️ Urgent Low Stock Warnings";
            // 
            // dgvLowStockAlerts
            // 
            this.dgvLowStockAlerts.AllowUserToAddRows = false;
            this.dgvLowStockAlerts.AllowUserToDeleteRows = false;
            this.dgvLowStockAlerts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLowStockAlerts.BackgroundColor = System.Drawing.Color.White;
            this.dgvLowStockAlerts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLowStockAlerts.ColumnHeadersHeight = 28;
            this.dgvLowStockAlerts.Location = new System.Drawing.Point(22, 215);
            this.dgvLowStockAlerts.Name = "dgvLowStockAlerts";
            this.dgvLowStockAlerts.RowHeadersVisible = false;
            this.dgvLowStockAlerts.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvLowStockAlerts.Size = new System.Drawing.Size(460, 400);
            this.dgvLowStockAlerts.TabIndex = 3;
            // 
            // lblRecentSales
            // 
            this.lblRecentSales.AutoSize = true;
            this.lblRecentSales.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblRecentSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblRecentSales.Location = new System.Drawing.Point(520, 190);
            this.lblRecentSales.Name = "lblRecentSales";
            this.lblRecentSales.Size = new System.Drawing.Size(134, 19);
            this.lblRecentSales.TabIndex = 4;
            this.lblRecentSales.Text = "⚡ Today\'s Activities";
            // 
            // dgvRecentSales
            // 
            this.dgvRecentSales.AllowUserToAddRows = false;
            this.dgvRecentSales.AllowUserToDeleteRows = false;
            this.dgvRecentSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentSales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentSales.ColumnHeadersHeight = 28;
            this.dgvRecentSales.Location = new System.Drawing.Point(524, 215);
            this.dgvRecentSales.Name = "dgvRecentSales";
            this.dgvRecentSales.RowHeadersVisible = false;
            this.dgvRecentSales.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvRecentSales.Size = new System.Drawing.Size(500, 400);
            this.dgvRecentSales.TabIndex = 5;
            // 
            // UC_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.dgvRecentSales);
            this.Controls.Add(this.lblRecentSales);
            this.Controls.Add(this.dgvLowStockAlerts);
            this.Controls.Add(this.lblLowStockAlerts);
            this.Controls.Add(this.panelKpis);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UC_Dashboard";
            this.Size = new System.Drawing.Size(1050, 640);
            this.Load += new System.EventHandler(this.UC_Dashboard_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelKpis.ResumeLayout(false);
            this.kpiLowStock.ResumeLayout(false);
            this.kpiLowStock.PerformLayout();
            this.kpiTodayOrders.ResumeLayout(false);
            this.kpiTodayOrders.PerformLayout();
            this.kpiDailySales.ResumeLayout(false);
            this.kpiDailySales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStockAlerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentSales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
