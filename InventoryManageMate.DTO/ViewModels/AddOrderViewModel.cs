

namespace InventoryManageMate.DTO.ViewModels
{
    public class AddOrderViewModel
    {
        public string? CustomerName { get; set; }

        public string? CustomerEmail { get; set; }

        public string? CustomerPhone { get; set; }

        public string? ShippingAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; } = "Pending";

        public string? PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }
    }
}