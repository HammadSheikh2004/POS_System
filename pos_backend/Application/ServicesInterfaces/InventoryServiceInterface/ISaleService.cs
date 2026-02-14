using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServicesInterfaces.InventoryServiceInterface
{
    public interface ISaleService
    {
        public Task<List<SaleDto>> ICreateSaleService(List<SaleDto> SaleDto);
        public Task<List<SaleDto>> IGetAllSaleService();
        public Task<List<SaleDto>> IGetSaleByIdService(int SaleId);
        public Task<List<DailySaleReportDTO>> IDailySaleReportService();
        public Task<List<CustomerSaleHistoryDTO>> ICustomerSaleHistoryReportService();
        public Task<List<ProductWiseSaleReportDTO>> IProductWiseSaleReportService();
        public Task<List<TodaySaleReportDTO>> ITodaySaleReportService(DateOnly todayDate);
        public Task<List<MonthWiseSaleReportDTO>> IMonthWiseSaleReportService(int month, int year);
        public Task<List<MonthlySaleReportDTO>> IMonthlySaleReportService(int year);
    }
}
