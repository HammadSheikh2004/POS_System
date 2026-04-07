using Application.Repositories.InventoryRepository;
using Application.ServicesInterfaces.InventoryServiceInterface;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InventoryService
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository CustomerRepository { get; set; }

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.CustomerRepository = customerRepository;
        }

        public async Task<List<CustomerOnPageReportDTO>> ICustomerOnPageReportService(DateOnly first_date, DateOnly sec_date, string customerName)
        {
            return await CustomerRepository.ICustomerOnPageReport(first_date, sec_date, customerName);
        }

        public async Task<List<CustomerAgingReportDTO>> ICustomerAgingReportService()
        {
            return await CustomerRepository.ICustomerAgingReport();
        }
    }
}
