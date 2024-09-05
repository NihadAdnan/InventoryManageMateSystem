using InventoryManageMate.DTO.Models;
using InventoryManageMate.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManageMate.Handler.Services
{
    public class OrderDetailHandler : IOrderDetailHandler
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync()
        {
            // Fetch all order details from the repository
            return await _orderDetailRepository.GetOrderDetailsAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(Guid id)
        {
            // Fetch a single order detail by ID
            return await _orderDetailRepository.GetOrderDetailByIdAsync(id);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            // Add a new order detail
            await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            // Update an existing order detail
            var existingOrderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetail.OrderDetailID);

            if (existingOrderDetail != null)
            {
                // Update properties
                existingOrderDetail.ProductName = orderDetail.ProductName;
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                existingOrderDetail.Discount = orderDetail.Discount;

                await _orderDetailRepository.UpdateOrderDetailAsync(existingOrderDetail);
            }
        }

        public async Task DeleteOrderDetailAsync(Guid id)
        {
            // Delete an order detail by ID
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);

            if (orderDetail != null)
            {
                await _orderDetailRepository.DeleteOrderDetailAsync(orderDetail);
            }
        }
    }
}