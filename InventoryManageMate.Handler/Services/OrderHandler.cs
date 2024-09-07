using AutoMapper;
using InventoryManageMate.DTO.Models;
using InventoryManageMate.Handler.Services;
using InventoryManageMate.Repository.Repositories;
using InventoryManageMate.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManageMate.DTO.DTOs;

namespace InventoryManageMate.Handler.Services
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ExportService _exportService;
        private readonly IMapper _mapper;

        public OrderHandler(IOrderRepository orderRepository, ExportService exportService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _exportService = exportService;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return order != null ? _mapper.Map<OrderDto>(order) : null;
        }

        public async Task AddOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
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

        public async Task<byte[]> ExportOrdersToCsvAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return _exportService.ExportOrdersToCsv(orderDtos);
        }

        public async Task<byte[]> ExportOrdersToPdfAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return _exportService.ExportOrdersToPdf(orderDtos);
        }
    }
}
