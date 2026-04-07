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
    public class SaleService : ISaleService
    {
        public ISaleRepository SaleRepository { get; set; }
        public SaleService(ISaleRepository saleRepository)
        {
            this.SaleRepository = saleRepository;
        }
        public async Task<List<SaleDto>> ICreateSaleService(List<SaleDto> SaleDto)
        {
            return await SaleRepository.ICreateSale(SaleDto);
        }

        public async Task<List<SaleDto>> IGetAllSaleService()
        {
            return await SaleRepository.IGetAllSale();
        }

        public async Task<List<SaleDto>> IGetSaleByIdService(int SaleId)
        {
            return await SaleRepository.IGetSaleById(SaleId);
        }

        public async Task<List<DailySaleReportDTO>> IDailySaleReportService()
        {
            return await SaleRepository.IDailySaleReport();
        }

        public async Task<List<CustomerSaleHistoryDTO>> ICustomerSaleHistoryReportService()
        {
            return await SaleRepository.ICustomerSaleHistoryReport();
        }

        public async Task<List<ProductWiseSaleReportDTO>> IProductWiseSaleReportService()
        {
            return await SaleRepository.IProductWiseSaleReport();
        }

        public async Task<List<TodaySaleReportDTO>> ITodaySaleReportService(DateOnly todayDate)
        {
            return await SaleRepository.ITodaySaleReport(todayDate);
        }

        public async Task<List<MonthWiseSaleReportDTO>> IMonthWiseSaleReportService(int month, int year)
        {
            return await SaleRepository.IMonthWiseSaleReport(month, year);
        }

        public async Task<List<MonthlySaleReportDTO>> IMonthlySaleReportService(int year)
        {
            return await SaleRepository.IMonthlySaleReport(year);
        }
    }
}
