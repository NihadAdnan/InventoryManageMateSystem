using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManageMate.Handler.Services
{
    public interface IOrderHandler
    {
        Task<List<OrderDto>> GetAllOrdersAsync();  
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
        Task AddOrderAsync(OrderDto orderDto); 
        Task UpdateOrderAsync(OrderDto orderDto); 
        Task DeleteOrderAsync(Guid id); 
        Task<byte[]> ExportOrdersToCsvAsync();
        Task<byte[]> ExportOrdersToPdfAsync(); 
    }
}
