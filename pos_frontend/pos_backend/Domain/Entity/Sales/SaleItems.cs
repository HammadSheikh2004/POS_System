using Domain.Entity.InventoryEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Sales
{
    public class SaleItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesItemId { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Unit Price is Required")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Total Price is Required")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
        [Required(ErrorMessage = "Amount Pay is Required")]
        [Display(Name = "Amount Pay")]
        public decimal AmountPay { get; set; }
        [Required(ErrorMessage = "Due Amount is Required")]
        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }
        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public Sale? Sale { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products? Product { get; set; } 
    }
}
