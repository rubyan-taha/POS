namespace GarmentShopPos.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; } // Yards/Meters
        public decimal UnitPrice { get; set; } // Price per yard/meter
        public bool IsPrinted { get; set; }
        public string? PrintType { get; set; }
        public bool IsEmbroidered { get; set; }
        public string? EmbroideryType { get; set; }
        public decimal EmbroideryExtraCharge { get; set; }
        public decimal TotalItemAmount { get; set; }

        // Navigation helpers for display
        public string FabricType { get; set; } = string.Empty;
        public string Section { get; set; } = "Gents";
        public string Details
        {
            get
            {
                if (Section == "Gents")
                {
                    return "Gents Section Fabric";
                }
                else
                {
                    string print = IsPrinted ? $"Printed ({PrintType})" : "Plain";
                    string emb = IsEmbroidered ? $", Embroidered ({EmbroideryType} +{EmbroideryExtraCharge:C})" : "";
                    return $"{print}{emb}";
                }
            }
        }
    }
}
