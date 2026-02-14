using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.InventoryRepository
{
    public interface ISaleRepository
    {
        public Task<List<SaleDto>> ICreateSale(List<SaleDto> SaleDto);
        public Task<List<SaleDto>> IGetAllSale();
        public Task<List<SaleDto>> IGetSaleById(int SaleId);
        public Task<List<DailySaleReportDTO>> IDailySaleReport();
        public Task<List<CustomerSaleHistoryDTO>> ICustomerSaleHistoryReport();
        public Task<List<ProductWiseSaleReportDTO>> IProductWiseSaleReport();
        public Task<List<TodaySaleReportDTO>> ITodaySaleReport(DateOnly todayDate);
        public Task<List<MonthWiseSaleReportDTO>> IMonthWiseSaleReport(int month, int year);
        public Task<List<MonthlySaleReportDTO>> IMonthlySaleReport(int year);
    }
}
