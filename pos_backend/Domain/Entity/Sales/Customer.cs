using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Sales
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Customer Name is Required!")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Number is Required!")]
        [Display(Name = "Customer Phone Number")]
        public string CustomerPhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is Required!")]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; } = string.Empty;
        public ICollection<Sale>? Sales { get; set; } 
    }
}
