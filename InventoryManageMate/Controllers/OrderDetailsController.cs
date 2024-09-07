using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.Handler.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InventoryManageMate.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailHandler _orderDetailHandler;

        public OrderDetailsController(IOrderDetailHandler orderDetailHandler)
        {
            _orderDetailHandler = orderDetailHandler;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Fetch list of order details using DTOs
            var orderDetails = await _orderDetailHandler.GetOrderDetailsAsync();
            return View(orderDetails);
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Render the Add view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderDetailDto orderDetailDto)
        {
            // Add new order detail using DTO
            await _orderDetailHandler.AddOrderDetailAsync(orderDetailDto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Fetch the order detail by ID using DTO
            var orderDetail = await _orderDetailHandler.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderDetailDto orderDetailDto)
        {
            // Update the order detail using DTO
            await _orderDetailHandler.UpdateOrderDetailAsync(orderDetailDto);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Delete the order detail by ID
            await _orderDetailHandler.DeleteOrderDetailAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToCsv()
        {
            // Export order details to CSV
            var fileContent = await _orderDetailHandler.ExportOrderDetailsToCsvAsync();
            return File(fileContent, "text/csv", "OrderDetails.csv");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToPdf()
        {
            // Export order details to PDF
            var fileContent = await _orderDetailHandler.ExportOrderDetailsToPdfAsync();
            return File(fileContent, "application/pdf", "OrderDetails.pdf");
        }
    }
}
