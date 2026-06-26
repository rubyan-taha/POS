namespace GarmentShopPos
{
    partial class ShopSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        
        // Shop Details Tab Controls
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.TextBox txtShopName;
        private System.Windows.Forms.Label lblShopAddress;
        private System.Windows.Forms.TextBox txtShopAddress;
        private System.Windows.Forms.Label lblShopPhone;
        private System.Windows.Forms.TextBox txtShopPhone;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.ComboBox cbTimeZone;
        private System.Windows.Forms.Label lblShopLanguage;
        private System.Windows.Forms.ComboBox cbShopLanguage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        // Tab Control
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabShopDetails;
        private System.Windows.Forms.TabPage tabUserAccounts;

        // User Accounts Tab Controls
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.GroupBox gbUserFields;
        private System.Windows.Forms.Label lblUserUsername;
        private System.Windows.Forms.TextBox txtUserUsername;
        private System.Windows.Forms.Label lblUserFullName;
        private System.Windows.Forms.TextBox txtUserFullName;
        private System.Windows.Forms.Label lblUserPassword;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label lblUserPwdHelp;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.ComboBox cbUserRole;
        private System.Windows.Forms.Label lblUserShop;
        private System.Windows.Forms.ComboBox cbUserShop;
        private System.Windows.Forms.Button btnUserSave;
        private System.Windows.Forms.Button btnUserDelete;
        private System.Windows.Forms.Button btnUserClear;
        private System.Windows.Forms.CheckBox chkShowUserPassword;
        private System.Windows.Forms.Button btnResetUserPassword;

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
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabShopDetails = new System.Windows.Forms.TabPage();
            this.lblShopName = new System.Windows.Forms.Label();
            this.txtShopName = new System.Windows.Forms.TextBox();
            this.lblShopAddress = new System.Windows.Forms.Label();
            this.txtShopAddress = new System.Windows.Forms.TextBox();
            this.lblShopPhone = new System.Windows.Forms.Label();
            this.txtShopPhone = new System.Windows.Forms.TextBox();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.cbTimeZone = new System.Windows.Forms.ComboBox();
            this.lblShopLanguage = new System.Windows.Forms.Label();
            this.cbShopLanguage = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabUserAccounts = new System.Windows.Forms.TabPage();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.gbUserFields = new System.Windows.Forms.GroupBox();
            this.lblUserUsername = new System.Windows.Forms.Label();
            this.txtUserUsername = new System.Windows.Forms.TextBox();
            this.lblUserFullName = new System.Windows.Forms.Label();
            this.txtUserFullName = new System.Windows.Forms.TextBox();
            this.lblUserPassword = new System.Windows.Forms.Label();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.lblUserPwdHelp = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.cbUserRole = new System.Windows.Forms.ComboBox();
            this.lblUserShop = new System.Windows.Forms.Label();
            this.cbUserShop = new System.Windows.Forms.ComboBox();
            this.btnUserSave = new System.Windows.Forms.Button();
            this.btnUserDelete = new System.Windows.Forms.Button();
            this.btnUserClear = new System.Windows.Forms.Button();
            this.chkShowUserPassword = new System.Windows.Forms.CheckBox();
            this.btnResetUserPassword = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabShopDetails.SuspendLayout();
            this.tabUserAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.gbUserFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(37)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(850, 70);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(850, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚙️ Settings & User Management";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabShopDetails);
            this.tabControlSettings.Controls.Add(this.tabUserAccounts);
            this.tabControlSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.tabControlSettings.Location = new System.Drawing.Point(20, 90);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(810, 460);
            this.tabControlSettings.TabIndex = 1;
            // 
            // tabShopDetails
            // 
            this.tabShopDetails.BackColor = System.Drawing.Color.White;
            this.tabShopDetails.Controls.Add(this.cbShopLanguage);
            this.tabShopDetails.Controls.Add(this.lblShopLanguage);
            this.tabShopDetails.Controls.Add(this.cbTimeZone);
            this.tabShopDetails.Controls.Add(this.lblTimeZone);
            this.tabShopDetails.Controls.Add(this.btnCancel);
            this.tabShopDetails.Controls.Add(this.btnSave);
            this.tabShopDetails.Controls.Add(this.txtShopPhone);
            this.tabShopDetails.Controls.Add(this.lblShopPhone);
            this.tabShopDetails.Controls.Add(this.txtShopAddress);
            this.tabShopDetails.Controls.Add(this.lblShopAddress);
            this.tabShopDetails.Controls.Add(this.txtShopName);
            this.tabShopDetails.Controls.Add(this.lblShopName);
            this.tabShopDetails.Location = new System.Drawing.Point(4, 26);
            this.tabShopDetails.Name = "tabShopDetails";
            this.tabShopDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabShopDetails.Size = new System.Drawing.Size(802, 430);
            this.tabShopDetails.TabIndex = 0;
            this.tabShopDetails.Text = "🏢 Shop Details";
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblShopName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblShopName.Location = new System.Drawing.Point(23, 15);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(86, 19);
            this.lblShopName.TabIndex = 0;
            this.lblShopName.Text = "Shop Name:";
            // 
            // txtShopName
            // 
            this.txtShopName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtShopName.Location = new System.Drawing.Point(23, 37);
            this.txtShopName.Name = "txtShopName";
            this.txtShopName.Size = new System.Drawing.Size(750, 27);
            this.txtShopName.TabIndex = 1;
            // 
            // lblShopAddress
            // 
            this.lblShopAddress.AutoSize = true;
            this.lblShopAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblShopAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblShopAddress.Location = new System.Drawing.Point(23, 80);
            this.lblShopAddress.Name = "lblShopAddress";
            this.lblShopAddress.Size = new System.Drawing.Size(98, 19);
            this.lblShopAddress.TabIndex = 2;
            this.lblShopAddress.Text = "Shop Address:";
            // 
            // txtShopAddress
            // 
            this.txtShopAddress.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtShopAddress.Location = new System.Drawing.Point(23, 102);
            this.txtShopAddress.Name = "txtShopAddress";
            this.txtShopAddress.Size = new System.Drawing.Size(750, 27);
            this.txtShopAddress.TabIndex = 3;
            // 
            // lblShopPhone
            // 
            this.lblShopPhone.AutoSize = true;
            this.lblShopPhone.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblShopPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblShopPhone.Location = new System.Drawing.Point(23, 145);
            this.lblShopPhone.Name = "lblShopPhone";
            this.lblShopPhone.Size = new System.Drawing.Size(88, 19);
            this.lblShopPhone.TabIndex = 4;
            this.lblShopPhone.Text = "Shop Phone:";
            // 
            // txtShopPhone
            // 
            this.txtShopPhone.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtShopPhone.Location = new System.Drawing.Point(23, 167);
            this.txtShopPhone.Name = "txtShopPhone";
            this.txtShopPhone.Size = new System.Drawing.Size(750, 27);
            this.txtShopPhone.TabIndex = 5;
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimeZone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTimeZone.Location = new System.Drawing.Point(23, 210);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(103, 19);
            this.lblTimeZone.TabIndex = 6;
            this.lblTimeZone.Text = "Shop Timezone:";
            // 
            // cbTimeZone
            // 
            this.cbTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimeZone.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbTimeZone.Location = new System.Drawing.Point(23, 232);
            this.cbTimeZone.Name = "cbTimeZone";
            this.cbTimeZone.Size = new System.Drawing.Size(750, 28);
            this.cbTimeZone.TabIndex = 7;
            // 
            // lblShopLanguage
            // 
            this.lblShopLanguage.AutoSize = true;
            this.lblShopLanguage.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblShopLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblShopLanguage.Location = new System.Drawing.Point(23, 275);
            this.lblShopLanguage.Name = "lblShopLanguage";
            this.lblShopLanguage.Size = new System.Drawing.Size(107, 19);
            this.lblShopLanguage.TabIndex = 10;
            this.lblShopLanguage.Text = "Shop Language:";
            // 
            // cbShopLanguage
            // 
            this.cbShopLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShopLanguage.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbShopLanguage.Items.AddRange(new object[] {
            "English",
            "Urdu"});
            this.cbShopLanguage.Location = new System.Drawing.Point(23, 297);
            this.cbShopLanguage.Name = "cbShopLanguage";
            this.cbShopLanguage.Size = new System.Drawing.Size(750, 28);
            this.cbShopLanguage.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(23, 355);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 40);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "💾 Save Settings";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(220, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 40);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "❌ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnResetUserPassword
            // 
            this.btnResetUserPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnResetUserPassword.FlatAppearance.BorderSize = 0;
            this.btnResetUserPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetUserPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnResetUserPassword.ForeColor = System.Drawing.Color.White;
            this.btnResetUserPassword.Location = new System.Drawing.Point(15, 370);
            this.btnResetUserPassword.Name = "btnResetUserPassword";
            this.btnResetUserPassword.Size = new System.Drawing.Size(430, 35);
            this.btnResetUserPassword.TabIndex = 15;
            this.btnResetUserPassword.Text = "🔒 Reset Password";
            this.btnResetUserPassword.UseVisualStyleBackColor = false;
            this.btnResetUserPassword.Click += new System.EventHandler(this.btnResetUserPassword_Click);
            // 
            // tabUserAccounts
            // 
            this.tabUserAccounts.BackColor = System.Drawing.Color.White;
            this.tabUserAccounts.Controls.Add(this.btnResetUserPassword);
            this.tabUserAccounts.Controls.Add(this.dgvUsers);
            this.tabUserAccounts.Controls.Add(this.gbUserFields);
            this.tabUserAccounts.Location = new System.Drawing.Point(4, 26);
            this.tabUserAccounts.Name = "tabUserAccounts";
            this.tabUserAccounts.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserAccounts.Size = new System.Drawing.Size(802, 430);
            this.tabUserAccounts.TabIndex = 1;
            this.tabUserAccounts.Text = "👤 User Accounts";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(15, 20);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.RowTemplate.Height = 25;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(430, 335);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
            // 
            // gbUserFields
            // 
            this.gbUserFields.Controls.Add(this.chkShowUserPassword);
            this.gbUserFields.Controls.Add(this.cbUserShop);
            this.gbUserFields.Controls.Add(this.lblUserShop);
            this.gbUserFields.Controls.Add(this.btnUserClear);
            this.gbUserFields.Controls.Add(this.btnUserDelete);
            this.gbUserFields.Controls.Add(this.btnUserSave);
            this.gbUserFields.Controls.Add(this.cbUserRole);
            this.gbUserFields.Controls.Add(this.lblUserRole);
            this.gbUserFields.Controls.Add(this.lblUserPwdHelp);
            this.gbUserFields.Controls.Add(this.txtUserPassword);
            this.gbUserFields.Controls.Add(this.lblUserPassword);
            this.gbUserFields.Controls.Add(this.txtUserFullName);
            this.gbUserFields.Controls.Add(this.lblUserFullName);
            this.gbUserFields.Controls.Add(this.txtUserUsername);
            this.gbUserFields.Controls.Add(this.lblUserUsername);
            this.gbUserFields.Location = new System.Drawing.Point(465, 15);
            this.gbUserFields.Name = "gbUserFields";
            this.gbUserFields.Size = new System.Drawing.Size(315, 390);
            this.gbUserFields.TabIndex = 1;
            this.gbUserFields.TabStop = false;
            this.gbUserFields.Text = "Add / Edit User Details";
            // 
            // lblUserUsername
            // 
            this.lblUserUsername.AutoSize = true;
            this.lblUserUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUserUsername.Location = new System.Drawing.Point(15, 25);
            this.lblUserUsername.Name = "lblUserUsername";
            this.lblUserUsername.Size = new System.Drawing.Size(74, 19);
            this.lblUserUsername.TabIndex = 0;
            this.lblUserUsername.Text = "Username:";
            // 
            // txtUserUsername
            // 
            this.txtUserUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUserUsername.Location = new System.Drawing.Point(15, 45);
            this.txtUserUsername.Name = "txtUserUsername";
            this.txtUserUsername.Size = new System.Drawing.Size(280, 25);
            this.txtUserUsername.TabIndex = 1;
            // 
            // lblUserFullName
            // 
            this.lblUserFullName.AutoSize = true;
            this.lblUserFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUserFullName.Location = new System.Drawing.Point(15, 85);
            this.lblUserFullName.Name = "lblUserFullName";
            this.lblUserFullName.Size = new System.Drawing.Size(77, 19);
            this.lblUserFullName.TabIndex = 2;
            this.lblUserFullName.Text = "Full Name:";
            // 
            // txtUserFullName
            // 
            this.txtUserFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUserFullName.Location = new System.Drawing.Point(15, 105);
            this.txtUserFullName.Name = "txtUserFullName";
            this.txtUserFullName.Size = new System.Drawing.Size(280, 25);
            this.txtUserFullName.TabIndex = 3;
            // 
            // lblUserPassword
            // 
            this.lblUserPassword.AutoSize = true;
            this.lblUserPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUserPassword.Location = new System.Drawing.Point(15, 145);
            this.lblUserPassword.Name = "lblUserPassword";
            this.lblUserPassword.Size = new System.Drawing.Size(71, 19);
            this.lblUserPassword.TabIndex = 4;
            this.lblUserPassword.Text = "Password:";
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUserPassword.Location = new System.Drawing.Point(15, 165);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(200, 25);
            this.txtUserPassword.TabIndex = 5;
            this.txtUserPassword.UseSystemPasswordChar = true;
            // 
            // chkShowUserPassword
            // 
            this.chkShowUserPassword.AutoSize = true;
            this.chkShowUserPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkShowUserPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.chkShowUserPassword.Location = new System.Drawing.Point(225, 167);
            this.chkShowUserPassword.Name = "chkShowUserPassword";
            this.chkShowUserPassword.Size = new System.Drawing.Size(48, 19);
            this.chkShowUserPassword.TabIndex = 14;
            this.chkShowUserPassword.Text = "See";
            this.chkShowUserPassword.UseVisualStyleBackColor = true;
            this.chkShowUserPassword.CheckedChanged += new System.EventHandler(this.chkShowUserPassword_CheckedChanged);
            // 
            // lblUserPwdHelp
            // 
            this.lblUserPwdHelp.AutoSize = true;
            this.lblUserPwdHelp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblUserPwdHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblUserPwdHelp.Location = new System.Drawing.Point(15, 192);
            this.lblUserPwdHelp.Name = "lblUserPwdHelp";
            this.lblUserPwdHelp.Size = new System.Drawing.Size(236, 13);
            this.lblUserPwdHelp.TabIndex = 6;
            this.lblUserPwdHelp.Text = "* Leave empty to keep existing user password.";
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUserRole.Location = new System.Drawing.Point(15, 215);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(40, 19);
            this.lblUserRole.TabIndex = 7;
            this.lblUserRole.Text = "Role:";
            // 
            // cbUserRole
            // 
            this.cbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbUserRole.FormattingEnabled = true;
            this.cbUserRole.Items.AddRange(new object[] {
            "SuperAdmin",
            "Admin",
            "Manager",
            "Cashier",
            "Employee"});
            this.cbUserRole.Location = new System.Drawing.Point(15, 235);
            this.cbUserRole.Name = "cbUserRole";
            this.cbUserRole.Size = new System.Drawing.Size(280, 25);
            this.cbUserRole.TabIndex = 8;
            // 
            // lblUserShop
            // 
            this.lblUserShop.AutoSize = true;
            this.lblUserShop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUserShop.Location = new System.Drawing.Point(15, 275);
            this.lblUserShop.Name = "lblUserShop";
            this.lblUserShop.Size = new System.Drawing.Size(102, 19);
            this.lblUserShop.TabIndex = 12;
            this.lblUserShop.Text = "Assigned Shop:";
            // 
            // cbUserShop
            // 
            this.cbUserShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserShop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbUserShop.FormattingEnabled = true;
            this.cbUserShop.Items.AddRange(new object[] {
            "Gents",
            "Ladies",
            "Both"});
            this.cbUserShop.Location = new System.Drawing.Point(15, 295);
            this.cbUserShop.Name = "cbUserShop";
            this.cbUserShop.Size = new System.Drawing.Size(280, 25);
            this.cbUserShop.TabIndex = 13;
            // 
            // btnUserSave
            // 
            this.btnUserSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(86)))), ((int)(((byte)(214)))));
            this.btnUserSave.FlatAppearance.BorderSize = 0;
            this.btnUserSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUserSave.ForeColor = System.Drawing.Color.White;
            this.btnUserSave.Location = new System.Drawing.Point(15, 335);
            this.btnUserSave.Name = "btnUserSave";
            this.btnUserSave.Size = new System.Drawing.Size(90, 35);
            this.btnUserSave.TabIndex = 9;
            this.btnUserSave.Text = "Save";
            this.btnUserSave.UseVisualStyleBackColor = false;
            this.btnUserSave.Click += new System.EventHandler(this.btnUserSave_Click);
            // 
            // btnUserDelete
            // 
            this.btnUserDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(85)))));
            this.btnUserDelete.FlatAppearance.BorderSize = 0;
            this.btnUserDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUserDelete.ForeColor = System.Drawing.Color.White;
            this.btnUserDelete.Location = new System.Drawing.Point(110, 335);
            this.btnUserDelete.Name = "btnUserDelete";
            this.btnUserDelete.Size = new System.Drawing.Size(90, 35);
            this.btnUserDelete.TabIndex = 10;
            this.btnUserDelete.Text = "Delete";
            this.btnUserDelete.UseVisualStyleBackColor = false;
            this.btnUserDelete.Click += new System.EventHandler(this.btnUserDelete_Click);
            // 
            // btnUserClear
            // 
            this.btnUserClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnUserClear.FlatAppearance.BorderSize = 0;
            this.btnUserClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUserClear.ForeColor = System.Drawing.Color.White;
            this.btnUserClear.Location = new System.Drawing.Point(205, 335);
            this.btnUserClear.Name = "btnUserClear";
            this.btnUserClear.Size = new System.Drawing.Size(90, 35);
            this.btnUserClear.TabIndex = 11;
            this.btnUserClear.Text = "➕ Add New";
            this.btnUserClear.UseVisualStyleBackColor = false;
            this.btnUserClear.Click += new System.EventHandler(this.btnUserClear_Click);
            // 
            // ShopSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(850, 570);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShopSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "System Configuration Settings";
            this.Load += new System.EventHandler(this.ShopSettingsForm_Load);
            this.panelTop.ResumeLayout(false);
            this.tabControlSettings.ResumeLayout(false);
            this.tabShopDetails.ResumeLayout(false);
            this.tabShopDetails.PerformLayout();
            this.tabUserAccounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.gbUserFields.ResumeLayout(false);
            this.gbUserFields.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
