using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;

namespace GarmentShopPos
{
    public partial class UC_Reports : UserControl
    {
        private int hoveredTabIndex = -1;
        private Label? lblBestSellersHeading;
        private Label? lblSectionHeading;
        private Label? lblSalesmenHeading;
        private Label? lblFinancialsHeading;

        public UC_Reports()
        {
            InitializeComponent();
            
            // Custom owner-draw tab rendering for modern premium button look
            this.tabControlReports.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControlReports.ItemSize = new System.Drawing.Size(230, 36);
            this.tabControlReports.SizeMode = TabSizeMode.Fixed;
            
            this.tabControlReports.DrawItem += new DrawItemEventHandler(tabControlReports_DrawItem);
            this.tabControlReports.MouseMove += new MouseEventHandler(tabControlReports_MouseMove);
            this.tabControlReports.MouseLeave += new EventHandler(tabControlReports_MouseLeave);

            InitializeCustomHeadings();
            RevampSearchPanel();
        }

        private void RevampSearchPanel()
        {
            panelTop.BackColor = System.Drawing.Color.FromArgb(245, 247, 250); // Soft modern background
            panelTop.Height = 75;
            panelTop.Padding = new Padding(15);
            
            // Apply slight border visually if possible, or just rely on color contrast
            
            // Center all controls vertically in the 75px height panel
            lblTitle.Top = (panelTop.Height - lblTitle.Height) / 2;
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80); // Darker elegant text
            
            lblStart.Top = (panelTop.Height - lblStart.Height) / 2;
            dtpStart.Top = (panelTop.Height - dtpStart.Height) / 2;
            
            lblEnd.Top = (panelTop.Height - lblEnd.Height) / 2;
            dtpEnd.Top = (panelTop.Height - dtpEnd.Height) / 2;

            btnRefresh.Top = (panelTop.Height - 38) / 2;
            btnRefresh.Height = 38;
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(41, 128, 185); // Professional Blue
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;

            btnExportPdf.Top = (panelTop.Height - 38) / 2;
            btnExportPdf.Height = 38;
            btnExportPdf.BackColor = System.Drawing.Color.FromArgb(39, 174, 96); // Distinctive Green
            btnExportPdf.ForeColor = System.Drawing.Color.White;
            btnExportPdf.FlatStyle = FlatStyle.Flat;
            btnExportPdf.FlatAppearance.BorderSize = 0;
        }

        private void InitializeCustomHeadings()
        {
            lblBestSellersHeading = CreateHeadingLabel();
            lblSectionHeading = CreateHeadingLabel();
            lblSalesmenHeading = CreateHeadingLabel();
            lblFinancialsHeading = CreateHeadingLabel();

            tabBestSellers.Controls.Add(lblBestSellersHeading);
            dgvBestSellers.Dock = DockStyle.None;
            lblBestSellersHeading.Dock = DockStyle.Top;
            dgvBestSellers.Dock = DockStyle.Fill;
            dgvBestSellers.BringToFront();
            TranslationHelper.ApplyModernGridStyle(dgvBestSellers);

            tabSection.Controls.Add(lblSectionHeading);
            dgvSection.Dock = DockStyle.None;
            lblSectionHeading.Dock = DockStyle.Top;
            dgvSection.Dock = DockStyle.Fill;
            dgvSection.BringToFront();
            TranslationHelper.ApplyModernGridStyle(dgvSection);

            tabSalesmen.Controls.Add(lblSalesmenHeading);
            dgvSalesmen.Dock = DockStyle.None;
            lblSalesmenHeading.Dock = DockStyle.Top;
            dgvSalesmen.Dock = DockStyle.Fill;
            dgvSalesmen.BringToFront();
            TranslationHelper.ApplyModernGridStyle(dgvSalesmen);

            tabFinancials.Controls.Add(lblFinancialsHeading);
            dgvFinancials.Dock = DockStyle.None;
            lblFinancialsHeading.Dock = DockStyle.Top;
            dgvFinancials.Dock = DockStyle.Fill;
            dgvFinancials.BringToFront();
            TranslationHelper.ApplyModernGridStyle(dgvFinancials);
        }

        private Label CreateHeadingLabel()
        {
            var lbl = new Label();
            lbl.Height = 40;
            lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 11.5F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.FromArgb(88, 86, 214);
            lbl.BackColor = System.Drawing.Color.White;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lbl.Padding = new Padding(10, 0, 10, 10);
            return lbl;
        }

        private void UC_Reports_Load(object sender, EventArgs e)
        {
            // Default date range: Last 30 days to today
            dtpStart.Value = DateTime.Today.AddDays(-30);
            dtpEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1); // End of today

            ApplyLanguageTranslation();
        }

        public void RefreshData()
        {
            LoadAllReports();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllReports();
        }

        private async void btnExportPdf_Click(object sender, EventArgs e)
        {
            // Refresh grid data first to match the selected date picker values
            LoadAllReports();

            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName = $"Business_Report_{startDate:yyyyMMdd}_to_{endDate:yyyyMMdd}.pdf";
            saveFileDialog.Title = "Save Business Performance Report (PDF)";

            // Use 'this' as the owner to force the file dialog to the foreground
            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            string outputPath = saveFileDialog.FileName;

            // Disable buttons, show wait cursor
            btnExportPdf.Enabled = false;
            Cursor = Cursors.WaitCursor;

            // Capture data from UI/Session on UI thread
            string shopName = SessionManager.ShopName;
            string shopAddress = SessionManager.ShopAddress;
            string shopPhone = SessionManager.ShopPhone;
            string shopTimeZone = SessionManager.ShopTimeZone;
            
            string revVal = lblRevVal.Text;
            string ordersVal = lblOrdersVal.Text;
            string profitVal = lblProfitVal.Text;

            DataTable dtBestSellers = (dgvBestSellers.DataSource as DataTable)?.Copy();
            DataTable dtSection = (dgvSection.DataSource as DataTable)?.Copy();
            DataTable dtSalesmen = (dgvSalesmen.DataSource as DataTable)?.Copy();
            DataTable dtFinancials = (dgvFinancials.DataSource as DataTable)?.Copy();

            try
            {
                bool success = await Task.Run(() =>
                {
                    try
                    {
                        StringBuilder html = new StringBuilder();
                        html.AppendLine("<!DOCTYPE html>");
                        html.AppendLine("<html>");
                        html.AppendLine("<head>");
                        html.AppendLine("<meta charset='utf-8'>");
                        html.AppendLine("<title>Business Performance Report</title>");
                        html.AppendLine("<style>");
                        html.AppendLine("  body { font-family: 'Segoe UI', Arial, sans-serif; margin: 30px; color: #333; background-color: #fff; line-height: 1.4; }");
                        html.AppendLine("  .header { border-bottom: 2px solid #5856d6; padding-bottom: 15px; margin-bottom: 30px; }");
                        html.AppendLine("  .header h1 { margin: 0 0 5px 0; color: #1a1925; font-size: 26px; }");
                        html.AppendLine("  .header p { margin: 3px 0; color: #666; font-size: 14px; }");
                        html.AppendLine("  .date-range { font-weight: bold; color: #5856d6; margin-top: 10px; }");
                        html.AppendLine("  .kpis { display: flex; justify-content: space-between; gap: 20px; margin-bottom: 30px; }");
                        html.AppendLine("  .kpi-card { flex: 1; border: 1px solid #e2e8f0; border-radius: 8px; padding: 15px; background-color: #f8fafc; box-shadow: 0 1px 3px rgba(0,0,0,0.05); }");
                        html.AppendLine("  .kpi-title { font-size: 12px; color: #64748b; text-transform: uppercase; font-weight: bold; margin-bottom: 5px; }");
                        html.AppendLine("  .kpi-value { font-size: 20px; font-weight: bold; }");
                        html.AppendLine("  .kpi-revenue { color: #5856d6; }");
                        html.AppendLine("  .kpi-orders { color: #009688; }");
                        html.AppendLine("  .kpi-profit { color: #4caf50; }");
                        html.AppendLine("  h2 { color: #1a1925; font-size: 18px; margin-top: 30px; margin-bottom: 12px; border-left: 4px solid #5856d6; padding-left: 10px; }");
                        html.AppendLine("  table { width: 100%; border-collapse: collapse; margin-bottom: 25px; font-size: 13px; }");
                        html.AppendLine("  th, td { border: 1px solid #e2e8f0; padding: 10px 12px; text-align: left; }");
                        html.AppendLine("  th { background-color: #f1f5f9; font-weight: bold; color: #1e293b; }");
                        html.AppendLine("  tr:nth-child(even) { background-color: #f8fafc; }");
                        html.AppendLine("  .text-right { text-align: right; }");
                        html.AppendLine("  .footer { margin-top: 50px; text-align: center; font-size: 11px; color: #94a3b8; border-top: 1px solid #e2e8f0; padding-top: 15px; page-break-inside: avoid; }");
                        html.AppendLine("</style>");
                        html.AppendLine("</head>");
                        html.AppendLine("<body>");

                        // Shop Header
                        html.AppendLine("<div class='header'>");
                        html.AppendLine($"  <h1>{WebUtility.HtmlEncode(shopName)}</h1>");
                        html.AppendLine($"  <p>{WebUtility.HtmlEncode(shopAddress)}</p>");
                        html.AppendLine($"  <p>Phone: {WebUtility.HtmlEncode(shopPhone)}</p>");
                        html.AppendLine($"  <p class='date-range'>Performance Report: {startDate:dd MMM yyyy} to {endDate:dd MMM yyyy}</p>");
                        html.AppendLine("</div>");

                        // KPIs section
                        html.AppendLine("<div class='kpis'>");
                        html.AppendLine("  <div class='kpi-card'>");
                        html.AppendLine("    <div class='kpi-title'>Total Revenue</div>");
                        html.AppendLine($"    <div class='kpi-value kpi-revenue'>{WebUtility.HtmlEncode(revVal)}</div>");
                        html.AppendLine("  </div>");
                        html.AppendLine("  <div class='kpi-card'>");
                        html.AppendLine("    <div class='kpi-title'>Orders Placed</div>");
                        html.AppendLine($"    <div class='kpi-value kpi-orders'>{WebUtility.HtmlEncode(ordersVal)}</div>");
                        html.AppendLine("  </div>");
                        html.AppendLine("  <div class='kpi-card'>");
                        html.AppendLine("    <div class='kpi-title'>Gross Profit</div>");
                        html.AppendLine($"    <div class='kpi-value kpi-profit'>{WebUtility.HtmlEncode(profitVal)}</div>");
                        html.AppendLine("  </div>");
                        html.AppendLine("</div>");

                        // Best Selling Fabrics
                        html.AppendLine("<h2>Best Selling Fabrics</h2>");
                        AppendHtmlTable(html, dtBestSellers);

                        // Section Performance
                        html.AppendLine("<h2>Section Performance</h2>");
                        AppendHtmlTable(html, dtSection);

                        // Salesman Leaderboard
                        html.AppendLine("<h2>Salesman Performance</h2>");
                        AppendHtmlTable(html, dtSalesmen);

                        // Daily Financial Summary
                        html.AppendLine("<h2>Daily Financial Summary</h2>");
                        AppendHtmlTable(html, dtFinancials);

                        // Footer
                        html.AppendLine("<div class='footer'>");
                        DateTime reportDate = DateTime.Now;
                        try
                        {
                            var tz = TimeZoneInfo.FindSystemTimeZoneById(shopTimeZone);
                            reportDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                        }
                        catch { }
                        html.AppendLine($"  <p>Report Generated on {reportDate:dd/MM/yyyy mm:hh tt} | GARMENT SHOP POS System</p>");
                        html.AppendLine("</div>");

                        html.AppendLine("</body>");
                        html.AppendLine("</html>");

                        // Save to temporary HTML file
                        string tempHtmlPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".html");
                        File.WriteAllText(tempHtmlPath, html.ToString(), Encoding.UTF8);

                        // Invoke Microsoft Edge CLI
                        string edgePath = "msedge";
                        string standardPath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                        if (File.Exists(standardPath))
                        {
                            edgePath = standardPath;
                        }

                        var psi = new System.Diagnostics.ProcessStartInfo();
                        psi.FileName = edgePath;
                        psi.Arguments = $"--headless --disable-gpu --no-pdf-header-footer --print-to-pdf=\"{outputPath}\" \"file:///{tempHtmlPath.Replace('\\', '/')}\"";
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;

                        using (var process = System.Diagnostics.Process.Start(psi))
                        {
                            if (process != null)
                            {
                                process.WaitForExit(10000); // 10s timeout
                            }
                        }

                        // Clean up temp file
                        try { File.Delete(tempHtmlPath); } catch { }

                        return File.Exists(outputPath);
                    }
                    catch
                    {
                        return false;
                    }
                });

                if (success)
                {
                    MessageBox.Show(this.FindForm(), "PDF report generated successfully!", "Report Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open the PDF automatically
                    var openPsi = new System.Diagnostics.ProcessStartInfo();
                    openPsi.FileName = outputPath;
                    openPsi.UseShellExecute = true;
                    System.Diagnostics.Process.Start(openPsi);
                }
                else
                {
                    MessageBox.Show(this.FindForm(), "Failed to convert HTML to PDF. Please check if Microsoft Edge is installed.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.FindForm(), $"Failed to export report to PDF:\n{ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExportPdf.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void AppendHtmlTable(StringBuilder sb, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                sb.AppendLine("<p>No data available for this section.</p>");
                return;
            }

            sb.AppendLine("<table>");
            
            // Header
            sb.AppendLine("  <thead>");
            sb.AppendLine("    <tr>");
            foreach (DataColumn col in dt.Columns)
            {
                sb.AppendLine($"      <th>{WebUtility.HtmlEncode(col.ColumnName)}</th>");
            }
            sb.AppendLine("    </tr>");
            sb.AppendLine("  </thead>");

            // Rows
            sb.AppendLine("  <tbody>");
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("    <tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string value = row[i] == DBNull.Value ? "" : row[i].ToString();
                    
                    // Format decimal numeric values nicely if they represent currency
                    if (dt.Columns[i].DataType == typeof(decimal) && decimal.TryParse(value, out decimal decVal))
                    {
                        if (dt.Columns[i].ColumnName.Contains("Price") || dt.Columns[i].ColumnName.Contains("Revenue") || dt.Columns[i].ColumnName.Contains("Rs") || dt.Columns[i].ColumnName.Contains("Profit") || dt.Columns[i].ColumnName.Contains("Amount") || dt.Columns[i].ColumnName.Contains("Subtotal"))
                        {
                            value = decVal.ToString("N2");
                        }
                        else
                        {
                            value = decVal.ToString("N2"); // Keep yards etc with decimals
                        }
                    }

                    // Check if alignment should be right
                    bool isNumeric = decimal.TryParse(value, out _) || int.TryParse(value, out _);
                    string colName = dt.Columns[i].ColumnName.ToLower();
                    bool shouldRightAlign = isNumeric || colName.Contains("revenue") || colName.Contains("profit") || colName.Contains("price") || colName.Contains("amount") || colName.Contains("total") || colName.Contains("yards") || colName.Contains("meters") || colName.Contains("count") || colName.Contains("subtotal");
                    
                    if (shouldRightAlign)
                    {
                        sb.AppendLine($"      <td class='text-right'>{WebUtility.HtmlEncode(value)}</td>");
                    }
                    else
                    {
                        sb.AppendLine($"      <td>{WebUtility.HtmlEncode(value)}</td>");
                    }
                }
                sb.AppendLine("    </tr>");
            }
            sb.AppendLine("  </tbody>");
            sb.AppendLine("</table>");
        }

        private void LoadAllReports()
        {
            // Setup parameters
            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1);

            LoadKpiCards(startDate, endDate);
            LoadBestSellers(startDate, endDate);
            LoadSectionPerformance(startDate, endDate);
            LoadSalesmanPerformance(startDate, endDate);
            LoadFinancialSummary(startDate, endDate);
        }

        private void LoadKpiCards(DateTime start, DateTime end)
        {
            decimal revenue = 0.00m;
            int ordersCount = 0;
            decimal grossProfit = 0.00m;

            string queryKpi = @"
                SELECT COALESCE(SUM(total_amount), 0), COUNT(*) 
                FROM orders 
                WHERE order_date BETWEEN @start AND @end AND is_refunded = 0;";

            string queryProfit = @"
                SELECT COALESCE(SUM(oi.total_item_amount - (oi.quantity * p.wholesale_price)), 0)
                FROM order_items oi
                JOIN products p ON oi.product_id = p.id
                JOIN orders o ON oi.order_id = o.id
                WHERE o.order_date BETWEEN @start AND @end AND o.is_refunded = 0;";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                
                // Revenue and orders
                using (var cmd = new SqlCommand(queryKpi, conn))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            revenue = reader.GetDecimal(0);
                            ordersCount = reader.GetInt32(1);
                        }
                    }
                }

                // Profit
                using (var cmd = new SqlCommand(queryProfit, conn))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    grossProfit = Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }

            lblRevVal.Text = $"Rs {revenue:N2}";
            lblOrdersVal.Text = ordersCount.ToString();
            lblProfitVal.Text = $"Rs {grossProfit:N2}";
        }

        private void LoadBestSellers(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            string colFabric = isUrdu ? "کپڑے کی قسم" : "Fabric Type";
            string colSection = isUrdu ? "سیکشن" : "Section";
            string colSpecs = isUrdu ? "تفصیلات / سوٹ کی قسم" : "Specs / Suit Type";
            string colSold = isUrdu ? "فروخت شدہ میٹر" : "Meters Sold";
            string colRev = isUrdu ? "کل آمدنی (روپے)" : "Total Revenue (Rs)";

            string query = $@"
                SELECT TOP 15 p.fabric_type AS [{colFabric}], 
                       p.section AS [{colSection}], 
                       CASE WHEN p.section='Gents' THEN CONCAT(p.fabric_material, ' (', p.color, ')') ELSE p.suit_type END AS [{colSpecs}],
                       SUM(oi.quantity) AS [{colSold}], 
                       SUM(oi.total_item_amount) AS [{colRev}]
                FROM order_items oi
                JOIN products p ON oi.product_id = p.id
                JOIN orders o ON oi.order_id = o.id
                WHERE o.order_date BETWEEN @start AND @end AND o.is_refunded = 0
                GROUP BY oi.product_id, p.fabric_type, p.section, p.fabric_material, p.color, p.suit_type
                ORDER BY SUM(oi.quantity) DESC;";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            dgvBestSellers.DataSource = null;
            dgvBestSellers.DataSource = dt;
            SetGridScrollability(dgvBestSellers);
        }

        private void LoadSectionPerformance(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            string colSection = isUrdu ? "سیکشن" : "Section";
            string colInvoices = isUrdu ? "انوائسز کی تعداد" : "Invoices Count";
            string colSold = isUrdu ? "کل فروخت شدہ میٹر" : "Total Meters Sold";
            string colRev = isUrdu ? "سیکشن آمدنی (روپے)" : "Section Revenue (Rs)";

            string query = $@"
                SELECT p.section AS [{colSection}], 
                       COUNT(DISTINCT o.id) AS [{colInvoices}],
                       SUM(oi.quantity) AS [{colSold}],
                       SUM(oi.total_item_amount) AS [{colRev}]
                FROM order_items oi
                JOIN products p ON oi.product_id = p.id
                JOIN orders o ON oi.order_id = o.id
                WHERE o.order_date BETWEEN @start AND @end AND o.is_refunded = 0
                GROUP BY p.section;";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            dgvSection.DataSource = null;
            dgvSection.DataSource = dt;
            SetGridScrollability(dgvSection);
        }

        private void LoadSalesmanPerformance(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            string colName = isUrdu ? "سیلزمین کا نام" : "Salesman Name";
            string colRole = isUrdu ? "عہدہ" : "Role";
            string colInvoices = isUrdu ? "کل انوائسز" : "Total Invoices";
            string colRev = isUrdu ? "کل حاصل شدہ آمدنی (روپے)" : "Total Revenue Generated (Rs)";

            string query = $@"
                SELECT u.full_name AS [{colName}], 
                       u.role AS [{colRole}],
                       COUNT(o.id) AS [{colInvoices}], 
                       SUM(o.total_amount) AS [{colRev}]
                FROM orders o
                JOIN users u ON o.salesman_id = u.id
                WHERE o.order_date BETWEEN @start AND @end AND o.is_refunded = 0
                GROUP BY o.salesman_id, u.full_name, u.role
                ORDER BY SUM(o.total_amount) DESC;";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            dgvSalesmen.DataSource = null;
            dgvSalesmen.DataSource = dt;
            SetGridScrollability(dgvSalesmen);
        }

        private void LoadFinancialSummary(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            string colDate = isUrdu ? "تاریخ" : "Date";
            string colOrders = isUrdu ? "آرڈرز کی تعداد" : "Orders Count";
            string colSubtotal = isUrdu ? "سب ٹوٹل (روپے)" : "Subtotal (Rs)";
            string colDiscounts = isUrdu ? "ڈسکاؤنٹ (روپے)" : "Discounts (Rs)";
            string colRev = isUrdu ? "خالص آمدنی (روپے)" : "Net Revenue (Rs)";

            string query = $@"
                SELECT CAST(order_date AS DATE) AS [{colDate}], 
                       COUNT(*) AS [{colOrders}], 
                       SUM(subtotal) AS [{colSubtotal}], 
                       SUM(discount) AS [{colDiscounts}], 
                       SUM(total_amount) AS [{colRev}]
                FROM orders
                WHERE order_date BETWEEN @start AND @end AND is_refunded = 0
                GROUP BY CAST(order_date AS DATE)
                ORDER BY CAST(order_date AS DATE) DESC;";

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            dgvFinancials.DataSource = null;
            dgvFinancials.DataSource = dt;
            SetGridScrollability(dgvFinancials);
        }

        public void ApplyLanguageTranslation()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);

            this.RightToLeft = isUrdu ? RightToLeft.Yes : RightToLeft.No;

            if (isUrdu)
            {
                lblTitle.Text = "کاروباری کارکردگی رپورٹیں";
                lblStart.Text = "تاریخ آغاز:";
                lblEnd.Text = "تاریخ اختتام:";
                lblRevTitle.Text = "💵 کل آمدنی (روپے)";
                lblOrdersTitle.Text = "🛍️ کل آرڈرز کی تعداد";
                lblProfitTitle.Text = "💰 خالص منافع (روپے)";

                tabBestSellers.Text = "سب سے زیادہ بکنے والے کپڑے";
                tabSection.Text = "سیکشن کارکردگی (مردانہ بمقابلہ زنانہ)";
                tabSalesmen.Text = "سیلزمین کارکردگی";
                tabFinancials.Text = "روزانہ مالیاتی خلاصہ";
                btnRefresh.Text = "🔍 تلاش کریں";
                btnExportPdf.Text = "🖨️ پی ڈی ایف ایکسپورٹ";

                if (lblBestSellersHeading != null) lblBestSellersHeading.Text = "تفصیلی رپورٹ: سب سے زیادہ فروخت ہونے والے کپڑے";
                if (lblSectionHeading != null) lblSectionHeading.Text = "سیکشن رپورٹ: مردانہ بمقابلہ زنانہ سیلز کارکردگی";
                if (lblSalesmenHeading != null) lblSalesmenHeading.Text = "سیلزمین رپورٹ: انفرادی فروخت اور کارکردگی لیڈر بورڈ";
                if (lblFinancialsHeading != null) lblFinancialsHeading.Text = "مالیاتی خلاصہ: روزانہ کا خلاصہ، ڈسکاؤنٹس اور خالص آمدنی";
            }
            else
            {
                lblTitle.Text = "Business Performance Reports";
                lblStart.Text = "Start Date:";
                lblEnd.Text = "End Date:";
                lblRevTitle.Text = "💵 Total Revenue (Rs)";
                lblOrdersTitle.Text = "🛍️ Orders Count";
                lblProfitTitle.Text = "💰 Gross Net Profit (Rs)";

                tabBestSellers.Text = "Best Selling Fabrics";
                tabSection.Text = "Section Performance (Gents vs Ladies)";
                tabSalesmen.Text = "Salesman Performance";
                tabFinancials.Text = "Daily Financial Summary";
                btnRefresh.Text = "🔍 Search";
                btnExportPdf.Text = "🖨️ Export PDF";

                if (lblBestSellersHeading != null) lblBestSellersHeading.Text = "Detailed Report: Best Selling Fabrics Listing";
                if (lblSectionHeading != null) lblSectionHeading.Text = "Section Report: Gents vs Ladies Fabric Sales Metrics";
                if (lblSalesmenHeading != null) lblSalesmenHeading.Text = "Salesman Leaderboard: Individual Performance Breakdown";
                if (lblFinancialsHeading != null) lblFinancialsHeading.Text = "Financial Summary: Daily Sales Revenue, Discounts & Net Total";
            }

            LoadAllReports();
        }

        private void tabControlReports_DrawItem(object sender, DrawItemEventArgs e)
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

        private void tabControlReports_MouseMove(object sender, MouseEventArgs e)
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

        private void tabControlReports_MouseLeave(object sender, EventArgs e)
        {
            if (hoveredTabIndex != -1)
            {
                hoveredTabIndex = -1;
                ((TabControl)sender).Invalidate();
            }
        }

        private void SetGridScrollability(DataGridView dgv)
        {
            if (dgv != null && dgv.Columns.Count > 0)
            {
                dgv.ScrollBars = ScrollBars.Both;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.MinimumWidth = 85;
                }
            }
        }
    }
}
