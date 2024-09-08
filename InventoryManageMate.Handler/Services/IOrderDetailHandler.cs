
using InventoryManageMate.DTO.DTOs;

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
