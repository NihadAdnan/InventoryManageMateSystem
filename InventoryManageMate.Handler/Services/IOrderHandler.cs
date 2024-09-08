using InventoryManageMate.DTO.DTOs;

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
