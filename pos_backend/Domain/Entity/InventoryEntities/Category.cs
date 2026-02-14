using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.InventoryEntities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required (ErrorMessage = "Category is Requird!")]
        [Display (Name = "Category")]
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
