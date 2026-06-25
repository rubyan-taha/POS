using System;
using System.Windows.Forms;

namespace GarmentShopPos
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                // Initialize database schema and seeds
                DatabaseManager.InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection or setup failed:\n{ex.Message}\n\nPlease check your appsettings.json or make sure SQL Server Express is running.", 
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm());
        }    
    }
}