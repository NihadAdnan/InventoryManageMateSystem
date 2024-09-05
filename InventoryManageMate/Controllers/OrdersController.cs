using InventoryManageMate.DTO.Models;
using InventoryManageMate.Handler.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManageMate.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderHandler _orderHandler;

        public OrdersController(OrderHandler orderHandler)
        {
            _orderHandler = orderHandler;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orders = await _orderHandler.GetAllOrdersAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Order order)
        {
            await _orderHandler.AddOrderAsync(order);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var order = await _orderHandler.GetOrderByIdAsync(id);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            await _orderHandler.UpdateOrderAsync(order);
            return RedirectToAction("List");
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
            var fileContent = await _orderHandler.ExportOrdersToCsvAsync(); // Use handler
            return File(fileContent, "text/csv", "OrderHistory.csv");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToPdf()
        {
            var fileContent = await _orderHandler.ExportOrdersToPdfAsync(); // Use handler
            return File(fileContent, "application/pdf", "OrderHistory.pdf");
        }

    }
}

