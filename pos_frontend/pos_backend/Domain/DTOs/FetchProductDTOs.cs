using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public sealed class FetchProductDTOs
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public string ReorderLevel { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public CategoryDTO Category { get; set; } = new();
        public BrandDTO Brand { get; set; } = new();
        public UnitDTO Unit {get; set; } = new();
    }

    public sealed class CategoryDTO {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public sealed class BrandDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
    }

    public sealed class UnitDTO
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
    }
}
