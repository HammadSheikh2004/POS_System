using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.PurchaseOrderEntities
{
    public class POEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Supplier Name is required")]
        public string SupplierName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Item Name is required")]
        public int Items { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public decimal CostPricePerItem { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }
}
