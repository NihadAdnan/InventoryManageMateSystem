using InventoryManageMate.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManageMate.Repository.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetOrderDetailsAsync();
        Task<OrderDetail?> GetOrderDetailByIdAsync(Guid id);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task DeleteOrderDetailAsync(OrderDetail orderDetail);
    }
}