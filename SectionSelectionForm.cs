using System;
using System.Drawing;
using System.Windows.Forms;

namespace GarmentShopPos
{
    public partial class SectionSelectionForm : Form
    {
        public string SelectedSection { get; private set; } = "Gents";

        public SectionSelectionForm()
        {
            InitializeComponent();
        }

        private void btnGents_Click(object sender, EventArgs e)
        {
            SelectedSection = "Gents";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnLadies_Click(object sender, EventArgs e)
        {
            SelectedSection = "Ladies";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
