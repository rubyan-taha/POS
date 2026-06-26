namespace GarmentShopPos
{
    partial class SectionSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnGents;
        private System.Windows.Forms.Button btnLadies;

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
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnGents = new System.Windows.Forms.Button();
            this.btnLadies = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(37)))));
            this.panelTop.Controls.Add(this.lblHeader);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(480, 75);
            this.panelTop.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(480, 75);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Please Select Shop Section\r\n(براہ کرم شاپ سیکشن منتخب کریں)";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGents
            // 
            this.btnGents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnGents.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGents.FlatAppearance.BorderSize = 0;
            this.btnGents.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.btnGents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGents.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnGents.ForeColor = System.Drawing.Color.White;
            this.btnGents.Location = new System.Drawing.Point(35, 110);
            this.btnGents.Name = "btnGents";
            this.btnGents.Size = new System.Drawing.Size(190, 110);
            this.btnGents.TabIndex = 1;
            this.btnGents.Text = "👔 Gents Section\r\n\r\nمردانہ سیکشن";
            this.btnGents.UseVisualStyleBackColor = false;
            this.btnGents.Click += new System.EventHandler(this.btnGents_Click);
            // 
            // btnLadies
            // 
            this.btnLadies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(63)))), ((int)(((byte)(140)))));
            this.btnLadies.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLadies.FlatAppearance.BorderSize = 0;
            this.btnLadies.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(100)))), ((int)(((byte)(166)))));
            this.btnLadies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLadies.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnLadies.ForeColor = System.Drawing.Color.White;
            this.btnLadies.Location = new System.Drawing.Point(255, 110);
            this.btnLadies.Name = "btnLadies";
            this.btnLadies.Size = new System.Drawing.Size(190, 110);
            this.btnLadies.TabIndex = 2;
            this.btnLadies.Text = "👗 Ladies Section\r\n\r\nزنانہ سیکشن";
            this.btnLadies.UseVisualStyleBackColor = false;
            this.btnLadies.Click += new System.EventHandler(this.btnLadies_Click);
            // 
            // SectionSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 260);
            this.Controls.Add(this.btnLadies);
            this.Controls.Add(this.btnGents);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SectionSelectionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shop Section Selection";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
