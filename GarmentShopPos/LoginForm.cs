using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using GarmentShopPos.Models;

namespace GarmentShopPos
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CenterLoginCard();
            txtUsername.Focus();
            LoadShopSettings();
        }

        private void LoadShopSettings()
        {
            try
            {
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT setting_key, setting_value FROM settings", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string key = reader.GetString(0);
                                string val = reader.GetString(1);
                                if (key == "shop_name") SessionManager.ShopName = val;
                                else if (key == "shop_address") SessionManager.ShopAddress = val;
                                else if (key == "shop_phone") SessionManager.ShopPhone = val;
                                else if (key == "shop_timezone") SessionManager.ShopTimeZone = val;
                                else if (key == "shop_language") SessionManager.ShopLanguage = val;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Fall back silently to default values in SessionManager
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT * FROM users WHERE username = @username";
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbHash = reader.GetString("password_hash");
                                string inputHash = DatabaseManager.HashPassword(password);

                                if (dbHash == inputHash)
                                {
                                    string role = reader.GetString("role");
                                    // Normalize roles case-insensitively
                                    if (string.Equals(role, "admin", StringComparison.OrdinalIgnoreCase))
                                    {
                                        role = "Admin";
                                    }
                                    else if (string.Equals(role, "superadmin", StringComparison.OrdinalIgnoreCase))
                                    {
                                        role = "SuperAdmin";
                                    }
                                    else if (string.Equals(role, "manager", StringComparison.OrdinalIgnoreCase))
                                    {
                                        role = "Manager";
                                    }
                                    else if (string.Equals(role, "cashier", StringComparison.OrdinalIgnoreCase))
                                    {
                                        role = "Cashier";
                                    }
                                    else
                                    {
                                        role = "Employee";
                                    }

                                    SessionManager.CurrentUser = new User
                                    {
                                        Id = reader.GetInt32("id"),
                                        Username = reader.GetString("username"),
                                        FullName = reader.GetString("full_name"),
                                        Role = role
                                    };

                                    // Save the active shop mode based on user credentials
                                    string assignedShop = reader.GetString("assigned_shop");
                                    if (string.Equals(assignedShop, "Both", StringComparison.OrdinalIgnoreCase))
                                    {
                                        // Show choice dialog
                                        using (var selectionForm = new SectionSelectionForm())
                                        {
                                            if (selectionForm.ShowDialog() == DialogResult.OK)
                                            {
                                                SessionManager.ActiveShopMode = selectionForm.SelectedSection;
                                            }
                                            else
                                            {
                                                // Cancel login if they close the selection form
                                                SessionManager.CurrentUser = null;
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SessionManager.ActiveShopMode = assignedShop; // "Gents" or "Ladies"
                                    }

                                    // Log login activity in database
                                    DatabaseManager.LogActivity(SessionManager.CurrentUser.Id, "Login", SessionManager.ActiveShopMode);

                                    DialogResult = DialogResult.OK;
                                    Close();
                                    return;
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Authentication error:\n{ex.Message}\n\nPlease check your appsettings.json or make sure SQL Server is running.", 
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void CenterLoginCard()
        {
            if (panelLoginCard != null)
            {
                panelLoginCard.Location = new System.Drawing.Point(
                    (this.ClientSize.Width - panelLoginCard.Width) / 2,
                    (this.ClientSize.Height - panelLoginCard.Height) / 2
                );
            }
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            CenterLoginCard();
        }
    }
}
