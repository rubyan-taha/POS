namespace GarmentShopPos.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Section { get; set; } = "Gents"; // Gents, Ladies
        public string FabricType { get; set; } = string.Empty; // Lawn, Suiting, Georgette, etc.
        public string? FabricMaterial { get; set; } // Cotton, Linen, etc. (Gents only)
        public string? Color { get; set; } // Blue, White, etc. (Gents only)
        public bool IsPrinted { get; set; }
        public string? PrintType { get; set; } // Digital, Block, Screen (Ladies only)
        public bool IsEmbroidered { get; set; }
        public string? EmbroideryType { get; set; } // Hand work, Machine, Chikan (Ladies only)
        public decimal EmbroideryExtraCharge { get; set; }
        public string? SuitType { get; set; } // 3-Piece, 2-Piece, Single (Ladies only)
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; } // Per yard/meter price
        public decimal CurrentStock { get; set; } // length in yards/meters
        public decimal ReorderPoint { get; set; }
        public bool IsDeleted { get; set; }

        // Helper property to display product details nicely in dropdowns or list boxes
        public string DisplayName
        {
            get
            {
                bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", System.StringComparison.OrdinalIgnoreCase);
                string fabric = isUrdu ? TranslationHelper.TranslateFabric(FabricType, Section) : FabricType;
                
                bool isBox = (FabricType ?? "").IndexOf("box", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                             (FabricType ?? "").IndexOf("باکس", System.StringComparison.OrdinalIgnoreCase) >= 0;

                if (Section == "Gents")
                {
                    string material = isUrdu ? TranslationHelper.TranslateMaterial(FabricMaterial) : FabricMaterial;
                    string col = isUrdu ? TranslationHelper.TranslateColor(Color) : Color;
                    string unit = isUrdu ? (isBox ? "باکس" : "میٹر") : (isBox ? "box" : "m");
                    string stockStr = isBox ? $"{CurrentStock:N0}" : $"{CurrentStock:N1}";
                    return $"{fabric} ({material} - {col}) - Rs {RetailPrice:#,##0.00}/{unit} - Stock: {stockStr}";
                }
                else
                {
                    string suit = isUrdu ? TranslationHelper.TranslateSuitType(SuitType) : SuitType;
                    string print = IsPrinted ? (isUrdu ? $" - پرنٹڈ ({TranslationHelper.TranslatePrintType(PrintType)})" : $" - Printed ({PrintType})") : "";
                    string embroidery = IsEmbroidered ? (isUrdu ? $" - کڑھائی ({TranslationHelper.TranslateEmbroideryType(EmbroideryType)})" : $" - Embroidered ({EmbroideryType})") : "";
                    string unit = isUrdu ? (isBox ? "باکس" : "میٹر") : (isBox ? "box" : "m");
                    string stockStr = isBox ? $"{CurrentStock:N0}" : $"{CurrentStock:N1}";
                    return $"{fabric} ({suit}{print}{embroidery}) - Rs {RetailPrice:#,##0.00}/{unit} - Stock: {stockStr}";
                }
            }
        }

        // Simpler name for listing
        public string ShortName
        {
            get
            {
                bool isUrdu = string.Equals(SessionManager.ShopLanguage, "Urdu", System.StringComparison.OrdinalIgnoreCase);
                string fabric = isUrdu ? TranslationHelper.TranslateFabric(FabricType, Section) : FabricType;

                if (Section == "Gents")
                {
                    string material = isUrdu ? TranslationHelper.TranslateMaterial(FabricMaterial) : FabricMaterial;
                    string col = isUrdu ? TranslationHelper.TranslateColor(Color) : Color;
                    return $"{fabric} ({material} {col})";
                }
                else
                {
                    string suit = isUrdu ? TranslationHelper.TranslateSuitType(SuitType) : SuitType;
                    string print = IsPrinted ? (isUrdu ? $" ({TranslationHelper.TranslatePrintType(PrintType)})" : $" ({PrintType})") : "";
                    string embroidery = IsEmbroidered ? (isUrdu ? $" ({TranslationHelper.TranslateEmbroideryType(EmbroideryType)})" : $" ({EmbroideryType})") : "";
                    return $"{fabric} - {suit}{print}{embroidery}";
                }
            }
        }
    }
}
