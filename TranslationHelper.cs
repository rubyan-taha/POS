using System;

namespace GarmentShopPos
{
    public static class TranslationHelper
    {
        public static readonly string[] GentsCategoriesEnglish = new string[]
        {
            "Cotton simple",
            "wash & wear simple",
            "wash & wear design",
            "cotton box with embroidery",
            "gents box simple",
            "gents box design",
            "gents box shalwar kameez",
            "gents mix box embroidery",
            "mix gents box design",
            "mix simple box"
        };

        public static readonly string[] GentsCategoriesUrdu = new string[]
        {
            "کاٹن سادہ",
            "واش اینڈ ویئر سادہ",
            "واش اینڈ ویئر ڈیزائن",
            "کاٹن باکس کڑھائی والا",
            "مردانہ باکس سادہ",
            "مردانہ باکس ڈیزائن",
            "مردانہ باکس شلوار قمیض",
            "مردانہ مکس باکس کڑھائی",
            "مکس مردانہ باکس ڈیزائن",
            "مکس سادہ باکس"
        };

        public static readonly string[] LadiesCategoriesEnglish = new string[]
        {
            "Ladies embroidery",
            "Ladies print embroidery",
            "Ladies print 3 piece",
            "Ladies print 2 piece simple shalwar",
            "Ladies print 2 piece all over",
            "Ladies shawl lawn",
            "Ladies shawl garam",
            "Ladies dupatta"
        };

        public static readonly string[] LadiesCategoriesUrdu = new string[]
        {
            "زنانہ کڑھائی",
            "زنانہ پرنٹ کڑھائی",
            "زنانہ پرنٹ 3 پیس",
            "زنانہ پرنٹ 2 پیس سادہ شلوار",
            "زنانہ پرنٹ 2 پیس آل اوور",
            "زنانہ شال لان",
            "زنانہ شال گرم",
            "زنانہ دوپٹہ"
        };

        public static string TranslateFabric(string fabric, string section)
        {
            if (string.IsNullOrEmpty(fabric)) return "";
            if (string.Equals(section, "Gents", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < GentsCategoriesEnglish.Length; i++)
                {
                    if (string.Equals(fabric, GentsCategoriesEnglish[i], StringComparison.OrdinalIgnoreCase))
                        return GentsCategoriesUrdu[i];
                }
            }
            else
            {
                for (int i = 0; i < LadiesCategoriesEnglish.Length; i++)
                {
                    if (string.Equals(fabric, LadiesCategoriesEnglish[i], StringComparison.OrdinalIgnoreCase))
                        return LadiesCategoriesUrdu[i];
                }
            }
            return fabric;
        }

        public static string GetDatabaseFabricType(string uiText, string section)
        {
            if (string.IsNullOrEmpty(uiText)) return "";
            string cleaned = uiText.Trim();
            if (string.Equals(section, "Gents", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < GentsCategoriesUrdu.Length; i++)
                {
                    if (string.Equals(cleaned, GentsCategoriesUrdu[i], StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(cleaned, GentsCategoriesEnglish[i], StringComparison.OrdinalIgnoreCase))
                    {
                        return GentsCategoriesEnglish[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < LadiesCategoriesUrdu.Length; i++)
                {
                    if (string.Equals(cleaned, LadiesCategoriesUrdu[i], StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(cleaned, LadiesCategoriesEnglish[i], StringComparison.OrdinalIgnoreCase))
                    {
                        return LadiesCategoriesEnglish[i];
                    }
                }
            }
            return cleaned;
        }

        public static string TranslateMaterial(string? material)
        {
            if (string.IsNullOrEmpty(material)) return "";
            switch (material.ToLower().Trim())
            {
                case "cotton": return "کاٹن";
                case "linen": return "لِنن";
                case "terry cotton": return "ٹیری کاٹن";
                case "wash and wear": return "واش اینڈ ویئر";
                default: return material;
            }
        }

        public static string TranslateColor(string? color)
        {
            if (string.IsNullOrEmpty(color)) return "";
            switch (color.ToLower().Trim())
            {
                case "navy blue": return "نیوی بلیو";
                case "white": return "سفید";
                case "khaki": return "خاکی";
                case "black": return "کالا";
                case "blue": return "نیلا";
                default: return color;
            }
        }

        public static string TranslateSuitType(string? suitType)
        {
            if (string.IsNullOrEmpty(suitType)) return "";
            switch (suitType.ToLower().Trim())
            {
                case "3-piece": return "تھری پیس";
                case "2-piece": return "ٹو پیس";
                case "single": return "سنگل";
                default: return suitType;
            }
        }

        public static string TranslatePrintType(string? printType)
        {
            if (string.IsNullOrEmpty(printType)) return "";
            switch (printType.ToLower().Trim())
            {
                case "digital": return "ڈیجیٹل";
                case "block": return "بلاک";
                case "screen": return "اسکرین";
                default: return printType;
            }
        }

        public static string TranslateEmbroideryType(string? embType)
        {
            if (string.IsNullOrEmpty(embType)) return "";
            switch (embType.ToLower().Trim())
            {
                case "hand work": return "ہاتھ کا کام";
                case "machine": return "مشین";
                case "chikan": return "چکن کاری";
                default: return embType;
            }
        }

        public static void ApplyModernGridStyle(DataGridView dgv)
        {
            if (dgv == null) return;

            // Basic Properties
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.GridColor = Color.LightGray;

            // Advanced Styling
            dgv.EnableHeadersVisualStyles = false;

            // Column Header Style
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(41, 128, 185); // Nice blue
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.Padding = new Padding(10, 5, 10, 5);
            headerStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Row Style
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = Color.White;
            rowStyle.ForeColor = Color.FromArgb(64, 64, 64);
            rowStyle.Font = new Font("Segoe UI", 9.5F);
            rowStyle.SelectionBackColor = Color.FromArgb(236, 240, 241); // Very light gray/blue
            rowStyle.SelectionForeColor = Color.Black;
            rowStyle.Padding = new Padding(10, 5, 10, 5);
            dgv.DefaultCellStyle = rowStyle;
            dgv.RowTemplate.Height = 35;

            // Alternating Row Style
            DataGridViewCellStyle altRowStyle = new DataGridViewCellStyle();
            altRowStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.AlternatingRowsDefaultCellStyle = altRowStyle;

            // Make it fill
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Add visible borders for data clarity
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.GridColor = Color.FromArgb(200, 200, 200); // More visible border line color
        }
    }
}
