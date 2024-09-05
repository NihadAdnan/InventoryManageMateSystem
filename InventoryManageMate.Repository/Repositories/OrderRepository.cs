using InventoryManageMate.DTO.Models;
using InventoryManageMate.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryManageMate.Repository.Data;


namespace InventoryManageMate.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            var orderToDelete = await _dbContext.Orders.SingleOrDefaultAsync(x=>x.OrderID == order.OrderID);
            if (orderToDelete is not null)
            {
                _dbContext.Orders.Remove(orderToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
