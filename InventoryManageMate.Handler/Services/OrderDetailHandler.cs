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
            // Fetch all order details from the repository
            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();

            // Map entities to DTOs
            return _mapper.Map<List<OrderDetailDto>>(orderDetails);
        }

        public async Task<OrderDetailDto?> GetOrderDetailByIdAsync(Guid id)
        {
            // Fetch a single order detail by ID
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);

            // Map entity to DTO
            return orderDetail == null ? null : _mapper.Map<OrderDetailDto>(orderDetail);
        }

        public async Task AddOrderDetailAsync(OrderDetailDto orderDetailDto)
        {
            // Map DTO to entity
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);

            // Add a new order detail
            await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
        }

        public async Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto)
        {
            // Fetch existing order detail
            var existingOrderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailDto.OrderDetailID);

            if (existingOrderDetail != null)
            {
                // Update properties using DTO
                existingOrderDetail.ProductName = orderDetailDto.ProductName;
                existingOrderDetail.Quantity = orderDetailDto.Quantity;
                existingOrderDetail.UnitPrice = orderDetailDto.UnitPrice;
                // Assuming Discount is a valid property in the DTO, add it if needed:
                // existingOrderDetail.Discount = orderDetailDto.Discount; 

                // Update the order detail in the repository
                await _orderDetailRepository.UpdateOrderDetailAsync(existingOrderDetail);
            }
        }

        public async Task DeleteOrderDetailAsync(Guid id)
        {
            // Fetch the order detail by ID
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);

            if (orderDetail != null)
            {
                // Delete the order detail
                await _orderDetailRepository.DeleteOrderDetailAsync(orderDetail);
            }
        }

        public async Task<byte[]> ExportOrderDetailsToCsvAsync()
        {
            // Fetch data and export to CSV
            //var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            //return _exportService.ExportOrderDetailsToCsv(orderDetails);

            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            var orderDetailDtos = _mapper.Map<List<OrderDetailDto>>(orderDetails);
            return _exportService.ExportOrderDetailsToCsv(orderDetailDtos);
        }

        public async Task<byte[]> ExportOrderDetailsToPdfAsync()
        {
            //// Fetch data and export to PDF
            //var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            //return _exportService.ExportOrderDetailsToPdf(orderDetails);

            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            var orderDetailDtos = _mapper.Map<List<OrderDetailDto>>(orderDetails);
            return _exportService.ExportOrderDetailsToPdf(orderDetailDtos);
        }
    }
}
