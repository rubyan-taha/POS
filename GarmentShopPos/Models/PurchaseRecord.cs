using System;

namespace GarmentShopPos.Models
{
    public class PurchaseRecord
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        // Helper fields
        public string FabricDescription { get; set; } = string.Empty;
    }
}
