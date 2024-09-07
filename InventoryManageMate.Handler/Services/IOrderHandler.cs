using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManageMate.Handler.Services
{
    public interface IOrderHandler
    {
        Task<List<OrderDto>> GetAllOrdersAsync();  // Return a list of OrderDto
        Task<OrderDto?> GetOrderByIdAsync(Guid id); // Return OrderDto for specific id
        Task AddOrderAsync(OrderDto orderDto); // Accept OrderDto for adding order
        Task UpdateOrderAsync(OrderDto orderDto); // Accept OrderDto for updating order
        Task DeleteOrderAsync(Guid id); // Delete by id, no change needed
        Task<byte[]> ExportOrdersToCsvAsync(); // Export orders to CSV using DTOs
        Task<byte[]> ExportOrdersToPdfAsync(); // Export orders to PDF using DTOs
    }
}
