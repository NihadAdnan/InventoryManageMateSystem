using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
