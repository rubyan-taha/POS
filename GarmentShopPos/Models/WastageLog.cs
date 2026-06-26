using System;

namespace GarmentShopPos.Models
{
    public class WastageLog
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime LoggedAt { get; set; } = DateTime.Now;
        public int LoggedBy { get; set; }

        // Helper fields
        public string FabricDescription { get; set; } = string.Empty;
        public string LoggedByUsername { get; set; } = string.Empty;
    }
}
