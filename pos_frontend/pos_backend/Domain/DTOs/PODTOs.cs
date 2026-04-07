using Domain.Entity.InventoryEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public sealed class PODTOs
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public int Items { get; set; }
        public decimal CostPricePerItem { get; set; }
        public decimal TotalAmount { get; set; }
        public string? productName { get; set; } 

    }
}
