using AutoMapper;
using InventoryManageMate.DTO.Models;
using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.Repository.Repositories;
using InventoryManageMate.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManageMate.Handler.Services
{
    public class OrderDetailHandler : IOrderDetailHandler
    {
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly ExportService _exportService;
        private readonly IMapper _mapper;

        public OrderDetailHandler(IGenericRepository<OrderDetail> orderDetailRepository, ExportService exportService, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _exportService = exportService;
            _mapper = mapper;
        }

        public async Task<List<OrderDetailDto>> GetOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            return _mapper.Map<List<OrderDetailDto>>(orderDetails);
        }

        public async Task<OrderDetailDto?> GetOrderDetailByIdAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
            return orderDetail == null ? null : _mapper.Map<OrderDetailDto>(orderDetail);
        }

        public async Task AddOrderDetailAsync(OrderDetailDto orderDetailDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
            await _orderDetailRepository.AddAsync(orderDetail);
        }

        public async Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto)
        {
            var existingOrderDetail = await _orderDetailRepository.GetByIdAsync(orderDetailDto.OrderDetailID);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.ProductName = orderDetailDto.ProductName;
                existingOrderDetail.Quantity = orderDetailDto.Quantity;
                existingOrderDetail.UnitPrice = orderDetailDto.UnitPrice;
                await _orderDetailRepository.UpdateAsync(existingOrderDetail);
            }
        }

        public async Task DeleteOrderDetailAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
            if (orderDetail != null)
            {
                await _orderDetailRepository.DeleteAsync(orderDetail);
            }
        }

        public async Task<byte[]> ExportOrderDetailsToCsvAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            var orderDetailDtos = _mapper.Map<List<OrderDetailDto>>(orderDetails);
            return _exportService.ExportOrderDetailsToCsv(orderDetailDtos);
        }

        public async Task<byte[]> ExportOrderDetailsToPdfAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            var orderDetailDtos = _mapper.Map<List<OrderDetailDto>>(orderDetails);
            return _exportService.ExportOrderDetailsToPdf(orderDetailDtos);
        }
    }
}
