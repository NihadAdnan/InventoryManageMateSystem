

namespace InventoryManageMate.DTO.DTOs
{
    public class OrderDto
    {
        public Guid OrderID { get; set; }
        public string? CustomerName { get; set; }

        public string? CustomerEmail { get; set; }

        public string? PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }

        public string? ShippingAddress { get; set; }
    }
}
