using InventoryManageMate.DTO.Models;
using InventoryManageMate.Repository.Data;
using InventoryManageMate.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryManageMate.Repository.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderDetailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync()
        {
            return await _dbContext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(Guid id)
        {
            return await _dbContext.OrderDetails.FindAsync(id);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _dbContext.OrderDetails.AddAsync(orderDetail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Update(orderDetail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Remove(orderDetail);
            await _dbContext.SaveChangesAsync();
        }
    }
}
