using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.DTO.Models;

namespace InventoryManageMate.Handler.Services
{
    public interface IOrderDetailHandler
    {
        Task<List<OrderDetailDto>> GetOrderDetailsAsync();
        Task<OrderDetailDto?> GetOrderDetailByIdAsync(Guid id);
        Task AddOrderDetailAsync(OrderDetailDto orderDetailDto);
        Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto);
        Task DeleteOrderDetailAsync(Guid id);
        Task<byte[]> ExportOrderDetailsToCsvAsync(); 
        Task<byte[]> ExportOrderDetailsToPdfAsync();
    }
}
