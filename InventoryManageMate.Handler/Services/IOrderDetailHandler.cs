using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManageMate.DTO.Models;

namespace InventoryManageMate.Handler.Services
{
    public interface IOrderDetailHandler
    {
        Task<List<OrderDetail>> GetOrderDetailsAsync();
        Task<OrderDetail?> GetOrderDetailByIdAsync(Guid id);
        Task AddOrderDetailAsync(OrderDetail orderDetailDto);
        Task UpdateOrderDetailAsync(OrderDetail orderDetailDto);
        Task DeleteOrderDetailAsync(Guid id);
    }
}