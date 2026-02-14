using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public sealed class DailySaleReportDTO
    {
        public DateTime SaleDay { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
