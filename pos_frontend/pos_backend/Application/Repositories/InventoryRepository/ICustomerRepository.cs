using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.InventoryRepository
{
    public interface ICustomerRepository
    {
        public Task<List<CustomerOnPageReportDTO>> ICustomerOnPageReport(DateOnly first_date, DateOnly sec_date, string customerName);
        public Task<List<CustomerAgingReportDTO>> ICustomerAgingReport();   
    }
}
