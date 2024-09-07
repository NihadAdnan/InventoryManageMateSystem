using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.DTO.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace InventoryManageMate.AggregateRoot
{
    public class ExportService
    {
        // Export Orders to CSV format using OrderDto
        public byte[] ExportOrdersToCsv(List<OrderDto> orders)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Name,Email,Address,Total Amount,Payment Method");

            foreach (var order in orders)
            {
                builder.AppendLine($"{order.CustomerName},{order.CustomerEmail},{order.ShippingAddress},{order.TotalAmount},{order.PaymentMethod}");
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        // Export Orders to PDF format using OrderDto
        public byte[] ExportOrdersToPdf(List<OrderDto> orders)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(5); // Adjust the number of columns according to OrderDto fields
                table.WidthPercentage = 100;
                table.AddCell(new PdfPCell(new Phrase("Name")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Email")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Address")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Total Amount")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Payment Method")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });

                foreach (var order in orders)
                {
                    table.AddCell(order.CustomerName ?? string.Empty);
                    table.AddCell(order.CustomerEmail ?? string.Empty);
                    table.AddCell(order.ShippingAddress ?? string.Empty);
                    table.AddCell(order.TotalAmount.ToString("F2"));
                    table.AddCell(order.PaymentMethod ?? string.Empty);
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                return stream.ToArray();
            }
        }

        // Export Order Details to CSV format using OrderDetailDto
        public byte[] ExportOrderDetailsToCsv(List<OrderDetailDto> orderDetails)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Product Name,Quantity,Unit Price");

            foreach (var orderDetail in orderDetails)
            {
                builder.AppendLine($"{orderDetail.ProductName},{orderDetail.Quantity},{orderDetail.UnitPrice}");
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        // Export Order Details to PDF format using OrderDetailDto
        public byte[] ExportOrderDetailsToPdf(List<OrderDetailDto> orderDetails)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(3); // 3 columns for OrderDetailDto fields
                table.WidthPercentage = 100;
                table.AddCell(new PdfPCell(new Phrase("Product Name")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Quantity")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Unit Price")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });

                foreach (var orderDetail in orderDetails)
                {
                    table.AddCell(orderDetail.ProductName ?? string.Empty);
                    table.AddCell(orderDetail.Quantity.ToString());
                    table.AddCell(orderDetail.UnitPrice.ToString("F2"));
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                return stream.ToArray();
            }
        }
    }
}
