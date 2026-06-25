using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GarmentShopPos
{
    public partial class ResetPasswordForm : Form
    {
        public ResetPasswordForm() : this("", false)
        {
        }

        public ResetPasswordForm(string prefilledUsername, bool isAdminReset)
        {
            InitializeComponent();
            
            if (!string.IsNullOrEmpty(prefilledUsername))
            {
                txtUsername.Text = prefilledUsername;
                txtUsername.Enabled = false;
            }

            if (isAdminReset)
            {
                // Hide passcode and mode fields
                lblShopMode.Visible = false;
                cbShopMode.Visible = false;
                lblShopPasscode.Visible = false;
                txtShopPasscode.Visible = false;

                // Move password fields up dynamically based on txtUsername
                lblNewPassword.Top = txtUsername.Bottom + 12;
                txtNewPassword.Top = lblNewPassword.Bottom + 5;
                lblConfirmPassword.Top = txtNewPassword.Bottom + 12;
                txtConfirmPassword.Top = lblConfirmPassword.Bottom + 5;
                
                // Set tag for admin bypass
                this.Tag = "AdminReset";
            }

            SetupShowPasswordCheckbox(isAdminReset);
        }

        private void SetupShowPasswordCheckbox(bool isAdminReset)
        {
            CheckBox chkShow = new CheckBox();
            chkShow.Text = string.Equals(SessionManager.ShopLanguage, "Urdu", StringComparison.OrdinalIgnoreCase) ? "پاس ورڈ دیکھیں" : "See Password";
            chkShow.Font = new System.Drawing.Font("Segoe UI", 9F);
            chkShow.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            chkShow.AutoSize = true;
            chkShow.Location = new System.Drawing.Point(txtConfirmPassword.Left, txtConfirmPassword.Bottom + 8);
            chkShow.CheckedChanged += (s, e) => {
                txtNewPassword.UseSystemPasswordChar = !chkShow.Checked;
                txtConfirmPassword.UseSystemPasswordChar = !chkShow.Checked;
            };
            this.Controls.Add(chkShow);

            // Lay out buttons dynamically based on checkbox position
            btnReset.Top = chkShow.Bottom + 15;
            btnCancel.Top = btnReset.Bottom + 10;

            // Set client size height to safely enclose the cancel button
            this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, btnCancel.Bottom + 20);
        }

        private void ResetPasswordForm_Load(object sender, EventArgs e)
        {
            bool isAdminReset = (this.Tag?.ToString() == "AdminReset");
            if (!isAdminReset)
            {
                cbShopMode.SelectedIndex = 0;
                txtShopPasscode.Text = "gents";
            }
        }

        private void cbShopMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbShopMode.SelectedIndex == 0)
                txtShopPasscode.Text = "gents";
            else
                txtShopPasscode.Text = "ladies";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string shopPasscode = txtShopPasscode.Text.Trim().ToLower();
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter the Username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Passcode
            bool isAdminReset = (this.Tag?.ToString() == "AdminReset");
            if (!isAdminReset)
            {
                string expectedPass = (cbShopMode.SelectedIndex == 0) ? "gents" : "ladies";
                if (shopPasscode != expectedPass)
                {
                    MessageBox.Show($"Invalid passcode for the selected Shop Mode. Use '{expectedPass}' for security clearance.", "Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Verify user exists
                bool userExists = false;
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE username = @username", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = (int)cmd.ExecuteScalar();
                        userExists = (count > 0);
                    }
                }

                if (!userExists)
                {
                    MessageBox.Show("Username not found. Please verify the spelling.", "Reset Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update password
                string passwordHash = DatabaseManager.HashPassword(newPassword);
                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UPDATE users SET password_hash = @hash WHERE username = @username", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@hash", passwordHash);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Password has been reset successfully. You can now login with your new credentials.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to reset password:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
