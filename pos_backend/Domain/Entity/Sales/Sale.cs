using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Sales
{
    public enum PaymentStatus
    {
        Pending = 0,
        Paid = 1,
        Overdue = 2,
        Cancelled = 3
    }
    public enum PaymentMethod
    {
        Cash = 0,
        CreditCard = 1,
        DebitCard = 2,
        OnlinePayment = 3,
        MobilePayment = 4
    }
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Sale Date is Required")]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }
        [Required(ErrorMessage = "Payment Status is Required")]
        [Display(Name = "Payment Status")]
        public PaymentStatus PaymentStatus { get; set; }
        [Required(ErrorMessage = "Payment Method is Required")]
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public ICollection<SaleItems>? SaleItems { get; set; }

    }
}
