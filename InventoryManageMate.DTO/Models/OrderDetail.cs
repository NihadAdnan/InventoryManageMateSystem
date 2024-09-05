using InventoryManageMate.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryManageMate.DTO.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailID { get; set; }

        [Required]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 999999999999.99)]
        public decimal UnitPrice { get; set; }

        [Range(0, 999999999999.99)]
        public decimal Discount { get; set; } = 0;
    }
}
