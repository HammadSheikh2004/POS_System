using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServicesInterfaces.InventoryServiceInterface
{
    public interface ICustomerService
    {
        public Task<List<CustomerOnPageReportDTO>> ICustomerOnPageReportService(DateOnly first_date, DateOnly sec_date, string customerName);
        public Task<List<CustomerAgingReportDTO>> ICustomerAgingReportService();
    }
}
