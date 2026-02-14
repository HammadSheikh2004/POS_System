using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CustomerOnPageReportDTO
    {
        public DateOnly DateOne {  get; set; }
        public DateOnly DateTwo { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
