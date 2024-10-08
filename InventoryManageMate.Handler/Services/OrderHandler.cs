﻿using AutoMapper;
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
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly ExportService _exportService;
        private readonly IMapper _mapper;

        public OrderHandler(IGenericRepository<Order> orderRepository, ExportService exportService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _exportService = exportService;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order != null ? _mapper.Map<OrderDto>(order) : null;
        }

        public async Task AddOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteAsync(order);
            }
        }

        public async Task<byte[]> ExportOrdersToCsvAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return _exportService.ExportOrdersToCsv(orderDtos);
        }

        public async Task<byte[]> ExportOrdersToPdfAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return _exportService.ExportOrdersToPdf(orderDtos);
        }
    }
}
