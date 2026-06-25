using System;
using System.Collections.Generic;

namespace GarmentShopPos.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public int? CustomerId { get; set; }
        public int SalesmanId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; // Cash, Card, Mobile, Credit
        public decimal AmountPaid { get; set; }
        public decimal ChangeReturned { get; set; }
        public bool IsRefunded { get; set; }
        public DateTime? RefundDate { get; set; }

        // Navigation properties (not mapped in simple ADO.NET query directly but useful)
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string SalesmanName { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
