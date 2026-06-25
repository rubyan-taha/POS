using System;
using System.Drawing;
using System.Windows.Forms;
using GarmentShopPos.Models;

namespace GarmentShopPos
{
    public    partial class MainForm : Form
    {
        private Button? currentActiveButton;
        private Color normalBtnColor = Color.Transparent;
        private Color activeBtnColor = Color.FromArgb(88, 86, 214); // Beautiful Royal Purple
        private Color normalTextColor = Color.FromArgb(190, 192, 204);
        private Color activeTextColor = Color.White;

        // Cached UserControl instances
        private UC_Dashboard? ucDashboard;
        private UC_POS? ucPOS;
        private UC_ProductEntry? ucProductEntry;
        private UC_StockViewer? ucStockViewer;
        private UC_Audit? ucAudit;
        private UC_Reports? ucReports;
        private Button? btnLanguageToggle;

        private System.Windows.Forms.Timer sidebarTimer = new System.Windows.Forms.Timer();
        private int targetSidebarWidth = 60;
        private const int sidebarStep = 25;

        public MainForm()
        {
            InitializeComponent();
            InitializeLanguageToggle();
            
            sidebarTimer.Interval = 10;
            sidebarTimer.Tick += SidebarTimer_Tick;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Start collapsed by default
            isSidebarCollapsed = true;
            CollapseSidebarInstant();

            // First, trigger Login
            ShowLogin();
            if (SessionManager.IsLoggedIn)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void ShowLogin()
        {
            using (LoginForm loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Login successful
                    ApplyUserPermissions();
                    UpdateLanguageButtonText();
                    RefreshLanguageRealtime();
                    // Load default tab
                    LoadDefaultTab();
                }
                else
                {
                    // Login cancelled, close application
                    Application.Exit();
                }
            }
        }

        private void ApplyUserPermissions()
        {
            if (SessionManager.CurrentUser == null) return;

            User user = SessionManager.CurrentUser;
            lblUserSession.Text = $"👤 User: {user.FullName} | Role: {user.Role} ({SessionManager.ActiveShopMode} Shop)";

            // Update sidebar logo text dynamically based on current shop settings
            if (!isSidebarCollapsed)
            {
                lblLogoText.Text = "👗 " + SessionManager.ShopName;
            }

            // Reset all buttons visibility
            btnDashboard.Visible = true;
            btnPOS.Visible = true;
            btnProductEntry.Visible = true;
            btnStockViewer.Visible = true;
            btnAudit.Visible = true;
            btnReports.Visible = true;
            btnSettings.Visible = true;

            // Set visibility based on Role
            if (user.Role == "Employee" || user.Role == "Cashier")
            {
                btnDashboard.Visible = false;
                btnProductEntry.Visible = false;
                btnAudit.Visible = false;
                btnReports.Visible = false;
                btnSettings.Visible = false;
            }
            else if (user.Role == "Manager")
            {
                btnDashboard.Visible = true;
                btnPOS.Visible = true;
                btnProductEntry.Visible = true;
                btnStockViewer.Visible = true;
                btnAudit.Visible = true;
                btnReports.Visible = true;
                btnSettings.Visible = false; // Only Admin / SuperAdmin can manage Settings/Users
            }
            else // Admin, SuperAdmin
            {
                btnDashboard.Visible = true;
                btnPOS.Visible = true;
                btnProductEntry.Visible = true;
                btnStockViewer.Visible = true;
                btnAudit.Visible = true;
                btnReports.Visible = true;
                btnSettings.Visible = true;
            }

            ApplySidebarLanguageTranslation();
        }

        private void LoadDefaultTab()
        {
            if (SessionManager.CurrentUser == null) return;

            if (SessionManager.CurrentUser.Role == "Employee" || SessionManager.CurrentUser.Role == "Cashier")
            {
                ActivateButton(btnPOS);
                if (ucPOS == null) ucPOS = new UC_POS();
                ShowUserControl(ucPOS, "Make Sale (POS)");
            }
            else
            {
                ActivateButton(btnDashboard);
                if (ucDashboard == null) ucDashboard = new UC_Dashboard();
                ShowUserControl(ucDashboard, "Dashboard Overview");
            }
        }

        private void ActivateButton(Button btnSender)
        {
            if (btnSender == null) return;

            // Deactivate previous active button
            if (currentActiveButton != null)
            {
                currentActiveButton.BackColor = normalBtnColor;
                currentActiveButton.ForeColor = normalTextColor;
            }

            // Activate new button
            currentActiveButton = btnSender;
            currentActiveButton.BackColor = activeBtnColor;
            currentActiveButton.ForeColor = activeTextColor;
        }

        public void ShowUserControl(UserControl uc, string title)
        {
            lblHeaderTitle.Text = GetLocalizedHeaderTitle(title);
            panelContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);

            // Dynamically refresh data on the control to clear stale data loops
            if (uc is UC_Dashboard dashboard)
            {
                dashboard.RefreshData();
            }
            else if (uc is UC_POS pos)
            {
                pos.RefreshData();
            }
            else if (uc is UC_StockViewer stockViewer)
            {
                stockViewer.RefreshData();
            }
            else if (uc is UC_Audit audit)
            {
                audit.RefreshData();
            }
            else if (uc is UC_Reports reports)
            {
                reports.RefreshData();
            }
        }

        // Navigation Clicks
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            if (ucDashboard == null) ucDashboard = new UC_Dashboard();
            ShowUserControl(ucDashboard, "Dashboard Overview");
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            if (ucPOS == null) ucPOS = new UC_POS();
            ShowUserControl(ucPOS, "Make Sale (POS)");
        }

        private void btnProductEntry_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            if (ucProductEntry == null) ucProductEntry = new UC_ProductEntry();
            ShowUserControl(ucProductEntry, "Product Setup");
        }

        private void btnStockViewer_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            if (ucStockViewer == null) ucStockViewer = new UC_StockViewer();
            ShowUserControl(ucStockViewer, "Current Stock");
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            if (ucAudit == null) ucAudit = new UC_Audit();
            ShowUserControl(ucAudit, "Check Fabric Stock");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            if (ucReports == null) ucReports = new UC_Reports();
            ShowUserControl(ucReports, "Reports & Business Analytics");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (ShopSettingsForm settingsForm = new ShopSettingsForm())
            {
                settingsForm.ShowDialog();
                // Reload active tab or permissions/title in case shop name/language changes
                ApplyUserPermissions();
                UpdateLanguageButtonText();
                RefreshLanguageRealtime();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (SessionManager.CurrentUser != null)
            {
                DatabaseManager.LogActivity(SessionManager.CurrentUser.Id, "Logout", SessionManager.ActiveShopMode);
            }
            base.OnFormClosing(e);
        }

        private void ClearCachedControls()
        {
            ucDashboard = null;
            ucPOS = null;
            ucProductEntry = null;
            ucStockViewer = null;
            ucAudit = null;
            ucReports = null;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (SessionManager.CurrentUser != null)
                {
                    DatabaseManager.LogActivity(SessionManager.CurrentUser.Id, "Logout", SessionManager.ActiveShopMode);
                }
                SessionManager.Logout();
                ClearCachedControls();
                panelContent.Controls.Clear();
                ShowLogin();
            }
        }

        private bool isSidebarCollapsed = true;
        
        private void CollapseSidebarInstant()
        {
            panelSidebar.Width = 60;
            lblLogoText.Text = "👗";
            lblLogoText.Location = new Point(18, 16);
            lblLogoText.Size = new Size(40, 48);
            
            btnDashboard.Text = "🖥️";
            btnPOS.Text = "🛒";
            btnProductEntry.Text = "📦";
            btnStockViewer.Text = "📊";
            btnAudit.Text = "🔍";
            btnReports.Text = "📈";
            btnSettings.Text = "⚙️";
            btnLogout.Text = "🚪";
            
            var noPad = new Padding(0);
            btnDashboard.Padding = noPad;
            btnPOS.Padding = noPad;
            btnProductEntry.Padding = noPad;
            btnStockViewer.Padding = noPad;
            btnAudit.Padding = noPad;
            btnReports.Padding = noPad;
            btnSettings.Padding = noPad;
            btnLogout.Padding = noPad;
            
            var center = ContentAlignment.MiddleCenter;
            btnDashboard.TextAlign = center;
            btnPOS.TextAlign = center;
            btnProductEntry.TextAlign = center;
            btnStockViewer.TextAlign = center;
            btnAudit.TextAlign = center;
            btnReports.TextAlign = center;
            btnSettings.TextAlign = center;
            btnLogout.TextAlign = center;

            btnToggleSidebar.Text = "☰";
        }

        private void btnToggleSidebar_Click(object sender, EventArgs e)
        {
            isSidebarCollapsed = !isSidebarCollapsed;
            targetSidebarWidth = isSidebarCollapsed ? 60 : 230;

            // Immediately change toggle button text to hamburger (collapsed) / back arrow (expanded)
            btnToggleSidebar.Text = isSidebarCollapsed ? "☰" : "◀";

            // If collapsing, set icons instantly so text doesn't wrap weirdly during sliding animation
            if (isSidebarCollapsed)
            {
                lblLogoText.Text = "👗";
                lblLogoText.Location = new Point(18, 16);
                lblLogoText.Size = new Size(40, 48);
                
                btnDashboard.Text = "🖥️";
                btnPOS.Text = "🛒";
                btnProductEntry.Text = "📦";
                btnStockViewer.Text = "📊";
                btnAudit.Text = "🔍";
                btnReports.Text = "📈";
                btnSettings.Text = "⚙️";
                btnLogout.Text = "🚪";
                
                var noPad = new Padding(0);
                btnDashboard.Padding = noPad;
                btnPOS.Padding = noPad;
                btnProductEntry.Padding = noPad;
                btnStockViewer.Padding = noPad;
                btnAudit.Padding = noPad;
                btnReports.Padding = noPad;
                btnSettings.Padding = noPad;
                btnLogout.Padding = noPad;
                
                var center = ContentAlignment.MiddleCenter;
                btnDashboard.TextAlign = center;
                btnPOS.TextAlign = center;
                btnProductEntry.TextAlign = center;
                btnStockViewer.TextAlign = center;
                btnAudit.TextAlign = center;
                btnReports.TextAlign = center;
                btnSettings.TextAlign = center;
                btnLogout.TextAlign = center;
            }

            sidebarTimer.Start();
        }

        private void SidebarTimer_Tick(object? sender, EventArgs e)
        {
            if (isSidebarCollapsed)
            {
                if (panelSidebar.Width > targetSidebarWidth)
                {
                    panelSidebar.Width = Math.Max(targetSidebarWidth, panelSidebar.Width - sidebarStep);
                }
                else
                {
                    sidebarTimer.Stop();
                }
            }
            else
            {
                if (panelSidebar.Width < targetSidebarWidth)
                {
                    panelSidebar.Width = Math.Min(targetSidebarWidth, panelSidebar.Width + sidebarStep);
                }
                else
                {
                    sidebarTimer.Stop();

                    // Expand completed: set full texts
                    lblLogoText.Text = "👗 " + SessionManager.ShopName;
                    lblLogoText.Location = new Point(12, 16);
                    lblLogoText.Size = new Size(210, 48);
                    
                    ApplySidebarLanguageTranslation();
                    
                    var pad = new Padding(20, 0, 0, 0);
                    btnDashboard.Padding = pad;
                    btnPOS.Padding = pad;
                    btnProductEntry.Padding = pad;
                    btnStockViewer.Padding = pad;
                    btnAudit.Padding = pad;
                    btnReports.Padding = pad;
                    btnSettings.Padding = pad;
                    btnLogout.Padding = pad;
                    
                    var left = ContentAlignment.MiddleLeft;
                    btnDashboard.TextAlign = left;
                    btnPOS.TextAlign = left;
                    btnProductEntry.TextAlign = left;
                    btnStockViewer.TextAlign = left;
                    btnAudit.TextAlign = left;
                    btnReports.TextAlign = left;
                    btnSettings.TextAlign = left;
                    btnLogout.TextAlign = left;
                }
            }
        }

        private void InitializeLanguageToggle()
        {
            btnLanguageToggle = new Button();
            btnLanguageToggle.Size = new Size(110, 32);
            btnLanguageToggle.Location = new Point(410, 24);
            btnLanguageToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLanguageToggle.FlatStyle = FlatStyle.Flat;
            btnLanguageToggle.FlatAppearance.BorderSize = 1;
            btnLanguageToggle.Cursor = Cursors.Hand;
            btnLanguageToggle.Click += btnLanguageToggle_Click;

            this.panelHeader.Controls.Add(btnLanguageToggle);
            UpdateLanguageButtonText();
        }

        private void btnLanguageToggle_Click(object? sender, EventArgs e)
        {
            string newLang = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase) ? "English" : "Urdu";
            SessionManager.ShopLanguage = newLang;

            DatabaseManager.UpdateSetting("shop_language", newLang);

            UpdateLanguageButtonText();
            RefreshLanguageRealtime();
        }

        private void UpdateLanguageButtonText()
        {
            if (btnLanguageToggle == null) return;

            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            if (isUrdu)
            {
                btnLanguageToggle.Text = "🌐 English";
                btnLanguageToggle.ForeColor = Color.FromArgb(88, 86, 214);
                btnLanguageToggle.FlatAppearance.BorderColor = Color.FromArgb(88, 86, 214);
            }
            else
            {
                btnLanguageToggle.Text = "🌐 اردو";
                btnLanguageToggle.ForeColor = Color.FromArgb(220, 53, 69);
                btnLanguageToggle.FlatAppearance.BorderColor = Color.FromArgb(220, 53, 69);
            }
        }

        private void ApplySidebarLanguageTranslation()
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            
            if (SessionManager.CurrentUser != null)
            {
                if (isUrdu)
                {
                    lblUserSession.Text = $"👤 صارف: {SessionManager.CurrentUser.FullName} | عہدہ: {SessionManager.CurrentUser.Role} ({SessionManager.ActiveShopMode} دکان)";
                }
                else
                {
                    lblUserSession.Text = $"👤 User: {SessionManager.CurrentUser.FullName} | Role: {SessionManager.CurrentUser.Role} ({SessionManager.ActiveShopMode} Shop)";
                }
            }

            if (isSidebarCollapsed) return;

            if (isUrdu)
            {
                btnDashboard.Text = "🖥️ ڈیش بورڈ";
                btnPOS.Text = "🛒 فروخت کریں";
                btnProductEntry.Text = "📦 مال کا اندراج";
                btnStockViewer.Text = "📊 اسٹاک دیکھیں";
                btnAudit.Text = "🔍 اسٹاک آڈٹ";
                btnReports.Text = "📈 رپورٹیں";
                btnSettings.Text = "⚙️ ترتیبات";
                btnLogout.Text = "🚪 لاگ آؤٹ";
            }
            else
            {
                btnDashboard.Text = "🖥️ Dashboard";
                btnPOS.Text = "🛒 Make Sale (POS)";
                btnProductEntry.Text = "📦 Product Setup";
                btnStockViewer.Text = "📊 View Stock";
                btnAudit.Text = "🔍 Check Fabric Stock";
                btnReports.Text = "📈 Reports & Analytics";
                btnSettings.Text = "⚙️ Shop Settings";
                btnLogout.Text = "🚪 Logout";
            }
        }

        private void RefreshLanguageRealtime()
        {
            ApplySidebarLanguageTranslation();
            UpdateHeaderTitle();

            if (ucDashboard != null)
            {
                ucDashboard.ApplyLanguageTranslation();
            }
            if (ucPOS != null)
            {
                ucPOS.ApplyLanguageTranslation();
            }
            if (ucProductEntry != null)
            {
                ucProductEntry.ApplyLanguageTranslation();
            }
            if (ucStockViewer != null)
            {
                ucStockViewer.ApplyLanguageTranslation();
            }
            if (ucAudit != null)
            {
                ucAudit.ApplyLanguageTranslation();
            }
            if (ucReports != null)
            {
                ucReports.ApplyLanguageTranslation();
            }
        }

        private string GetLocalizedHeaderTitle(string englishTitle)
        {
            bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase);
            if (!isUrdu) return englishTitle;

            switch (englishTitle)
            {
                case "Dashboard Overview":
                    return "ڈیش بورڈ کا جائزہ";
                case "Make Sale (POS)":
                    return "فروخت کریں (POS)";
                case "Product Setup":
                    return "مصنوعات کا اندراج";
                case "Current Stock":
                    return "موجودہ اسٹاک";
                case "Check Fabric Stock":
                    return "اسٹاک آڈٹ";
                case "Reports & Business Analytics":
                    return "رپورٹیں اور تجزیات";
                default:
                    return englishTitle;
            }
        }

        private void UpdateHeaderTitle()
        {
            if (currentActiveButton == btnDashboard)
                lblHeaderTitle.Text = GetLocalizedHeaderTitle("Dashboard Overview");
            else if (currentActiveButton == btnPOS)
                lblHeaderTitle.Text = GetLocalizedHeaderTitle("Make Sale (POS)");
            else if (currentActiveButton == btnProductEntry)
                lblHeaderTitle.Text = GetLocalizedHeaderTitle("Product Setup");
            else if (currentActiveButton == btnStockViewer)
                lblHeaderTitle.Text = GetLocalizedHeaderTitle("Current Stock");
            else if (currentActiveButton == btnAudit)
                lblHeaderTitle.Text = GetLocalizedHeaderTitle("Check Fabric Stock");
            else if (currentActiveButton == btnReports)
                lblHeaderTitle.Text = GetLocalizedHeaderTitle("Reports & Business Analytics");
        }
    }
}
