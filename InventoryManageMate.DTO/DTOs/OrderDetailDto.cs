

namespace InventoryManageMate.DTO.DTOs
{
    public class OrderDetailDto
    {
        public Guid OrderDetailID { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
