using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public sealed class CustomerAgingReportDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalDue { get; set; }
        public decimal Days0To30 { get; set; }
        public decimal Days31To60 { get; set; }
        public decimal Days61To90 { get; set; }
        public decimal Days90Plus { get; set; }
    }
}
