using Domain.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public sealed class SaleDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhoneNumber { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        
        public SaleDateTransferDto? SaleDateTransferDto { get; set; }

        public SaleItemsDto? SaleItemsDto { get; set; }
    }
    public sealed class SaleDateTransferDto
    {
        public int SaleId { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        
    }

    public sealed class SaleItemsDto
    {
        public int SalesItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AmountPay { get; set; }
        public decimal DueAmount { get; set; }

        public int ProductId { get; set; }
    }
}
