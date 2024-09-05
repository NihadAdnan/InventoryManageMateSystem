using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using InventoryManageMate.DTO.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;


namespace InventoryManageMate.AggregateRoot
{
    public class ExportService
    {
        public byte[] ExportOrdersToCsv(List<Order> orders)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Name,Email,Phone,Address,Total Amount,Payment Method,Status,Date");

            foreach (var order in orders)
            {
                builder.AppendLine($"{order.CustomerName},{order.CustomerEmail},{order.CustomerPhone},{order.ShippingAddress},{order.TotalAmount},{order.PaymentMethod},{order.OrderStatus},{order.OrderDate}");
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public byte[] ExportOrdersToPdf(List<Order> orders)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100;
                table.AddCell("Name");
                table.AddCell("Email");
                table.AddCell("Phone");
                table.AddCell("Address");
                table.AddCell("Total Amount");
                table.AddCell("Payment Method");
                table.AddCell("Status");
                table.AddCell("Date");

                foreach (var order in orders)
                {
                    table.AddCell(order.CustomerName);
                    table.AddCell(order.CustomerEmail);
                    table.AddCell(order.CustomerPhone);
                    table.AddCell(order.ShippingAddress);
                    table.AddCell(order.TotalAmount.ToString());
                    table.AddCell(order.PaymentMethod);
                    table.AddCell(order.OrderStatus);
                    table.AddCell(order.OrderDate.ToShortDateString());
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                return stream.ToArray();
            }
        }

        // Export Order Details to CSV format
        public byte[] ExportOrderDetailsToCsv(List<OrderDetail> orderDetails)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Product Name,Quantity,Unit Price");

            foreach (var orderDetail in orderDetails)
            {
                builder.AppendLine($"{orderDetail.ProductName},{orderDetail.Quantity},{orderDetail.UnitPrice}");
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        // Export Order Details to PDF format
        public byte[] ExportOrderDetailsToPdf(List<OrderDetail> orderDetails)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                pdfDoc.Open();

                // Create table with 3 columns
                PdfPTable table = new PdfPTable(3);
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

