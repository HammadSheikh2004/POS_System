using Domain.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.InventoryEntities
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required (ErrorMessage = "Product Name is Requied!")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;
        public int UnitId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Selleing Price is Requied!")]
        [Display (Name = "Selling Price")]
        public decimal SellingPrice { get; set; }
        [Required(ErrorMessage = "Cost Price is Requied!")]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }
        [Required(ErrorMessage = "Order Level is Requied!")]
        [Display(Name = "Order Level")]
        public string ReorderLevel { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public Units? Units { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public ICollection<SaleItems>? SaleItems { get; set; }
    }
}
