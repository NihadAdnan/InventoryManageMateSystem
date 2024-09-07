using AutoMapper;
using InventoryManageMate.DTO.Models;
using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManageMate.AggregateRoot;

namespace InventoryManageMate.Handler.Services
{
    public class OrderDetailHandler : IOrderDetailHandler
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ExportService _exportService;
        private readonly IMapper _mapper;

        public OrderDetailHandler(IOrderDetailRepository orderDetailRepository, ExportService exportService, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _exportService = exportService;
            _mapper = mapper;
        }

        public async Task<List<OrderDetailDto>> GetOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();

            return _mapper.Map<List<OrderDetailDto>>(orderDetails);
        }

        public async Task<OrderDetailDto?> GetOrderDetailByIdAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);

            return orderDetail == null ? null : _mapper.Map<OrderDetailDto>(orderDetail);
        }

        public async Task AddOrderDetailAsync(OrderDetailDto orderDetailDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);

            await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
        }

        public async Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto)
        {
            var existingOrderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailDto.OrderDetailID);

            if (existingOrderDetail != null)
            {
                existingOrderDetail.ProductName = orderDetailDto.ProductName;
                existingOrderDetail.Quantity = orderDetailDto.Quantity;
                existingOrderDetail.UnitPrice = orderDetailDto.UnitPrice;
                await _orderDetailRepository.UpdateOrderDetailAsync(existingOrderDetail);
            }
        }

        public async Task DeleteOrderDetailAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);

            if (orderDetail != null)
            {
                await _orderDetailRepository.DeleteOrderDetailAsync(orderDetail);
            }
        }

        public async Task<byte[]> ExportOrderDetailsToCsvAsync()
        {

            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            var orderDetailDtos = _mapper.Map<List<OrderDetailDto>>(orderDetails);
            return _exportService.ExportOrderDetailsToCsv(orderDetailDtos);
        }

        public async Task<byte[]> ExportOrderDetailsToPdfAsync()
        {

            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            var orderDetailDtos = _mapper.Map<List<OrderDetailDto>>(orderDetails);
            return _exportService.ExportOrderDetailsToPdf(orderDetailDtos);
        }
    }
}
