using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GarmentShopPos
{
    public partial class ShopSettingsForm : Form
    {
        private int selectedUserId = 0;

        public ShopSettingsForm()
        {
            InitializeComponent();
        }

        private void ShopSettingsForm_Load(object sender, EventArgs e)
        {
            // Set flat styles and premium color theory backgrounds
            cbTimeZone.FlatStyle = FlatStyle.Flat;
            cbTimeZone.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbShopLanguage.FlatStyle = FlatStyle.Flat;
            cbShopLanguage.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbUserRole.FlatStyle = FlatStyle.Flat;
            cbUserRole.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);
            cbUserShop.FlatStyle = FlatStyle.Flat;
            cbUserShop.BackColor = System.Drawing.Color.FromArgb(242, 242, 255);

            // Load Shop Details
            txtShopName.Text = SessionManager.ShopName;
            txtShopAddress.Text = SessionManager.ShopAddress;
            txtShopPhone.Text = SessionManager.ShopPhone;

            // Populate system time zones
            cbTimeZone.Items.Clear();
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
            {
                cbTimeZone.Items.Add(tz.Id);
            }

            // Select active timezone
            if (cbTimeZone.Items.Contains(SessionManager.ShopTimeZone))
            {
                cbTimeZone.SelectedItem = SessionManager.ShopTimeZone;
            }
            else
            {
                cbTimeZone.Text = SessionManager.ShopTimeZone;
            }

            // Select active language
            if (cbShopLanguage.Items.Contains(SessionManager.ShopLanguage))
            {
                cbShopLanguage.SelectedItem = SessionManager.ShopLanguage;
            }
            else
            {
                cbShopLanguage.SelectedIndex = 0; // Default to English
            }

            // Populate user management grid
            ConfigureUsersGrid();
            LoadUsers();
            btnUserClear_Click(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtShopName.Text.Trim();
            string address = txtShopAddress.Text.Trim();
            string phone = txtShopPhone.Text.Trim();
            string timezone = cbTimeZone.SelectedItem?.ToString() ?? cbTimeZone.Text.Trim();
            string language = cbShopLanguage.SelectedItem?.ToString() ?? cbShopLanguage.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(timezone) || string.IsNullOrEmpty(language))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            UpdateSetting(conn, trans, "shop_name", name);
                            UpdateSetting(conn, trans, "shop_address", address);
                            UpdateSetting(conn, trans, "shop_phone", phone);
                            UpdateSetting(conn, trans, "shop_timezone", timezone);

                            // Check and insert/update shop language setting
                            string checkKeyQuery = "SELECT COUNT(*) FROM settings WHERE setting_key = 'shop_language'";
                            int keyCount = 0;
                            using (var checkCmd = new SqlCommand(checkKeyQuery, conn, trans))
                            {
                                keyCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                            }
                            if (keyCount == 0)
                            {
                                string insertQuery = "INSERT INTO settings (setting_key, setting_value) VALUES ('shop_language', @value)";
                                using (var insertCmd = new SqlCommand(insertQuery, conn, trans))
                                {
                                    insertCmd.Parameters.AddWithValue("@value", language);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                UpdateSetting(conn, trans, "shop_language", language);
                            }
                            
                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }

                // Update Session variables
                SessionManager.ShopName = name;
                SessionManager.ShopAddress = address;
                SessionManager.ShopPhone = phone;
                SessionManager.ShopTimeZone = timezone;
                SessionManager.ShopLanguage = language;

                MessageBox.Show("Shop settings updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save settings:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSetting(SqlConnection conn, SqlTransaction trans, string key, string value)
        {
            string query = "UPDATE settings SET setting_value = @value WHERE setting_key = @key";
            using (var cmd = new SqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.ExecuteNonQuery();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ==========================================
        // USER MANAGEMENT CRUD CODE
        // ==========================================

        private void LoadUsers()
        {
            try
            {
                DataTable dt = new DataTable();
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id, username, full_name, role, assigned_shop FROM users ORDER BY username";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load users:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureUsersGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            if (dgvUsers.Columns.Count > 0)
            {
                dgvUsers.Columns.Clear();
            }

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "id", HeaderText = "ID", Width = 40, Visible = false });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "username", HeaderText = "Username", Width = 90 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "full_name", HeaderText = "Full Name", Width = 130 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "role", HeaderText = "Role", Width = 90 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "assigned_shop", HeaderText = "Assigned Shop", Width = 90 });
            TranslationHelper.ApplyModernGridStyle(dgvUsers);
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var row = dgvUsers.SelectedRows[0];
                if (row.Cells.Count >= 5 && row.Cells[0].Value != null && row.Cells[0].Value != DBNull.Value)
                {
                    selectedUserId = Convert.ToInt32(row.Cells[0].Value);
                    txtUserUsername.Text = row.Cells[1].Value?.ToString() ?? "";
                    txtUserFullName.Text = row.Cells[2].Value?.ToString() ?? "";
                    txtUserPassword.Clear(); // Keep empty for security; don't show hashes

                    string role = row.Cells[3].Value?.ToString() ?? "Employee";
                    
                    cbUserRole.Items.Clear();
                    cbUserRole.Items.AddRange(new object[] { "Manager", "Cashier", "Employee" });

                    if (role == "SuperAdmin")
                    {
                        cbUserRole.Items.Add("SuperAdmin");
                        cbUserRole.SelectedItem = "SuperAdmin";
                        cbUserRole.Enabled = false; // Block changing role of SuperAdmin
                    }
                    else if (role == "Admin")
                    {
                        cbUserRole.Items.Add("Admin");
                        cbUserRole.SelectedItem = "Admin";
                        cbUserRole.Enabled = false; // Block changing role of Admin
                    }
                    else
                    {
                        cbUserRole.Enabled = true;
                        if (cbUserRole.Items.Contains(role))
                        {
                            cbUserRole.SelectedItem = role;
                        }
                        else
                        {
                            cbUserRole.SelectedIndex = -1;
                        }
                    }

                    string assignedShop = row.Cells[4].Value?.ToString() ?? "Both";
                    if (cbUserShop.Items.Contains(assignedShop))
                    {
                        cbUserShop.SelectedItem = assignedShop;
                    }
                    else
                    {
                        cbUserShop.Text = assignedShop;
                    }
                }
            }
        }

        private void btnUserClear_Click(object sender, EventArgs e)
        {
            dgvUsers.ClearSelection();
            selectedUserId = 0;
            txtUserUsername.Clear();
            txtUserFullName.Clear();
            txtUserPassword.Clear();
            
            cbUserRole.Items.Clear();
            cbUserRole.Items.AddRange(new object[] { "Manager", "Cashier", "Employee" });
            cbUserRole.SelectedIndex = -1;
            cbUserRole.Enabled = true;

            cbUserShop.SelectedIndex = -1;
        }

        private void btnUserSave_Click(object sender, EventArgs e)
        {
            string username = txtUserUsername.Text.Trim();
            string fullName = txtUserFullName.Text.Trim();
            string password = txtUserPassword.Text;
            string role = cbUserRole.SelectedItem?.ToString() ?? cbUserRole.Text.Trim();
            string assignedShop = cbUserShop.SelectedItem?.ToString() ?? "Both";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Username, Full Name, and Role are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedUserId == 0 && (role == "SuperAdmin" || role == "Admin"))
            {
                MessageBox.Show("Creation of new SuperAdmin or Admin accounts is not allowed.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedUserId == 0 && string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password is required for new users.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();

                    // Username duplication check
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username AND id != @id";
                    using (var checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        checkCmd.Parameters.AddWithValue("@id", selectedUserId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string query;
                    if (selectedUserId == 0)
                    {
                        query = "INSERT INTO users (username, password_hash, full_name, role, assigned_shop) VALUES (@username, @pwdHash, @fullName, @role, @assignedShop)";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(password))
                        {
                            query = "UPDATE users SET username = @username, full_name = @fullName, role = @role, assigned_shop = @assignedShop WHERE id = @id";
                        }
                        else
                        {
                            query = "UPDATE users SET username = @username, password_hash = @pwdHash, full_name = @fullName, role = @role, assigned_shop = @assignedShop WHERE id = @id";
                        }
                    }

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedUserId);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@fullName", fullName);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@assignedShop", assignedShop);
                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("@pwdHash", DatabaseManager.HashPassword(password));
                        }
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(selectedUserId == 0 ? "User created successfully!" : "User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                btnUserClear_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save user:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SessionManager.CurrentUser != null && selectedUserId == SessionManager.CurrentUser.Id)
            {
                MessageBox.Show("You cannot delete your own logged-in account.", "Security Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();

                    // Check if selected user is an Admin
                    string roleQuery = "SELECT role FROM users WHERE id = @id";
                    string role = "";
                    using (var roleCmd = new SqlCommand(roleQuery, conn))
                    {
                        roleCmd.Parameters.AddWithValue("@id", selectedUserId);
                        role = roleCmd.ExecuteScalar()?.ToString() ?? "";
                    }

                    if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Count Admin accounts
                        string countQuery = "SELECT COUNT(*) FROM users WHERE role = 'Admin'";
                        using (var countCmd = new SqlCommand(countQuery, conn))
                        {
                            int adminCount = Convert.ToInt32(countCmd.ExecuteScalar());
                            if (adminCount <= 1)
                            {
                                MessageBox.Show("Cannot delete the last administrator user. There must be at least one Admin in the system.", "Security Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    var confirm = MessageBox.Show($"Are you sure you want to permanently delete user '{txtUserUsername.Text}'?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM users WHERE id = @id";
                        using (var deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@id", selectedUserId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                        btnUserClear_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete user:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowUserPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtUserPassword.UseSystemPasswordChar = !chkShowUserPassword.Checked;
        }

        private void btnResetUserPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var row = dgvUsers.SelectedRows[0];
                string username = row.Cells[1].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(username))
                {
                    using (var resetForm = new ResetPasswordForm(username, true))
                    {
                        resetForm.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to reset their password.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
