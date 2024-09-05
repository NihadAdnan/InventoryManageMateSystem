using InventoryManageMate.DTO.Models;
using InventoryManageMate.Handler.Services;
using InventoryManageMate.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManageMate.Handler.Services
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetOrdersAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteOrderAsync(order);
            }
        }
    }
}