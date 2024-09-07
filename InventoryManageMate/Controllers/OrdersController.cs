using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.DTO.Models;
using InventoryManageMate.Handler.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManageMate.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderHandler _orderHandler;

        public OrdersController(IOrderHandler orderHandler)
        {
            _orderHandler = orderHandler;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orders = await _orderHandler.GetAllOrdersAsync();
            return View(orders); // Ensure your view is updated to handle OrderDto
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderHandler.AddOrderAsync(orderDto); // Use OrderDto
                return RedirectToAction("List");
            }
            return View(orderDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var orderDto = await _orderHandler.GetOrderByIdAsync(id);
            if (orderDto == null)
            {
                return NotFound();
            }
            return View(orderDto); // Ensure your view is updated to handle OrderDto
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderHandler.UpdateOrderAsync(orderDto); // Use OrderDto
                return RedirectToAction("List");
            }
            return View(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("List");
            }
            await _orderHandler.DeleteOrderAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToCsv()
        {
            var fileContent = await _orderHandler.ExportOrdersToCsvAsync();
            return File(fileContent, "text/csv", "OrderHistory.csv");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToPdf()
        {
            var fileContent = await _orderHandler.ExportOrdersToPdfAsync();
            return File(fileContent, "application/pdf", "OrderHistory.pdf");
        }
    }
}
