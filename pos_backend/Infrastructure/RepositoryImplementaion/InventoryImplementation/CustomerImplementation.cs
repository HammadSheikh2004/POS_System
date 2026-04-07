using Application.Repositories.InventoryRepository;
using Application.ServicesInterfaces.InventoryServiceInterface;
using Domain.DTOs;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementaion.InventoryImplementation
{
    public class CustomerImplementation : ICustomerRepository
    {
        public POSDbContext dbContext { get; set; }
        public CustomerImplementation(POSDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<List<CustomerOnPageReportDTO>> ICustomerOnPageReport(DateOnly first_date, DateOnly sec_date, string customerName)
        {
            try
            {
                var date_one = first_date.ToDateTime(TimeOnly.MinValue);
                var date_two = sec_date.ToDateTime(TimeOnly.MinValue);
                var customer_name = customerName.ToLower();

                var on_page_report = await dbContext.Sales
                    .Where(x => x.SaleDate >= date_one && x.SaleDate <= date_two && x.Customer!.CustomerName == customer_name )
                    .Join(dbContext.SaleItems, s => s.SaleId, si => si.SaleId, (s, si) => new { s, si })
                    .Join(dbContext.Customers, s => s.s.CustomerId, c => c.CustomerId, (s, c) => new { s, c })
                    .Select(x => new CustomerOnPageReportDTO
                    {
                        CustomerName = x.s.s.Customer!.CustomerName,
                        DateOne = DateOnly.FromDateTime(x.s.s.SaleDate!),
                        DateTwo = DateOnly.FromDateTime(x.s.s.SaleDate!),
                        TotalPrice = x.s.si.TotalPrice,
                        TotalQuantity = x.s.si.Quantity,
                    }).ToListAsync();
                return on_page_report;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CustomerAgingReportDTO>> ICustomerAgingReport()
        {
            var today= DateTime.Today;
            //var data = await dbContext.SaleItems.Where(w => w.AmountPay > 0)
            //        .Join(dbContext.Sales, si => si.SaleId, s => s.SaleId, (si, s) => new { si, s })
            //        .Join(dbContext.Customers, s => s.s.CustomerId, c => c.CustomerId, (s, c) => new { s, c }).Select(x => new
            //        {
            //            x.c.CustomerName,
            //            DueAmount = x.s.si.TotalPrice - x.s.si.AmountPay,
            //            DaysOld = EF.Functions.DateDiffDay(x.s.s.SaleDate, today),
            //        }).ToListAsync();

            var data = await dbContext.SaleItems.Where(w => w.AmountPay > 0)
                .Select(x => new
                {
                    x.Sale!.Customer!.CustomerName,
                    DueAmount = x.TotalPrice - x.AmountPay,
                    DaysOld = EF.Functions.DateDiffDay(x.Sale.SaleDate, today)
                }).AsNoTracking().ToListAsync();

            var aging_report = data.GroupBy(x => x.CustomerName)
                .Select(x => new CustomerAgingReportDTO
                {
                    CustomerName = x.Key,
                    TotalDue = x.Sum(s => s.DueAmount),
                    Days0To30 = x.Where(i => i.DaysOld <= 30).Sum(s => s.DueAmount),
                    Days31To60 = x.Where(i => i.DaysOld > 30 && i.DaysOld <= 60).Sum(s => s.DueAmount),
                    Days61To90 = x.Where(i => i.DaysOld > 60 && i.DaysOld <= 90).Sum(s => s.DueAmount),
                    Days90Plus = x.Where(i => i.DaysOld > 90).Sum(s => s.DueAmount)
                }).OrderBy(r => r.CustomerName).ToList();
            return aging_report;
        }
    }
}
