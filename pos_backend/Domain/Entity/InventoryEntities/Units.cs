using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.InventoryEntities
{
    public class Units
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }
        [Required (ErrorMessage = "Unit Name is Requied!")]
        [Display (Name = "Unit Name")]
        public string UnitName { get; set; } = string.Empty;

        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
