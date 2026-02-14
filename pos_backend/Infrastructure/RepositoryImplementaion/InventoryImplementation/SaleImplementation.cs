using Application.Repositories.InventoryRepository;
using Domain.DTOs;
using Domain.Entity.Sales;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementaion.InventoryImplementation
{
    public class SaleImplementation : ISaleRepository
    {
        public POSDbContext _dbContext { get; set; }
        public SaleImplementation(POSDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<SaleDto>> ICreateSale(List<SaleDto> SaleDto)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var dto in SaleDto)
                {
                    var product = await _dbContext.Products
                        .FirstOrDefaultAsync(x => x.ProductId == dto.SaleItemsDto!.ProductId);

                    if (product == null)
                        throw new Exception($"Product with ID {dto.SaleItemsDto!.ProductId} not found.");

                    if (product.QuantityInStock < dto.SaleItemsDto!.Quantity)
                        throw new Exception($"Insufficient stock for product ID {dto.SaleItemsDto.ProductId}. " +
                                            $"Available: {product.QuantityInStock}, Requested: {dto.SaleItemsDto.Quantity}");
                    product.QuantityInStock -= dto.SaleItemsDto.Quantity;
                }

                var firstDto = SaleDto.First();

                var customer = new Customer
                {
                    CustomerName = firstDto.CustomerName,
                    CustomerPhoneNumber = firstDto.CustomerPhoneNumber,
                    CustomerAddress = firstDto.CustomerAddress,
                };
                await _dbContext.Customers.AddAsync(customer);
                
                await _dbContext.SaveChangesAsync();

                foreach (var dto in SaleDto)
                {
                    var sale = new Sale
                    {
                        SaleNumber = dto.SaleDateTransferDto!.SaleNumber,
                        SaleDate = dto.SaleDateTransferDto.SaleDate,
                        PaymentStatus = dto.SaleDateTransferDto.PaymentStatus,
                        PaymentMethod = dto.SaleDateTransferDto.PaymentMethod,
                        CustomerId = customer.CustomerId
                    };

                    await _dbContext.Sales.AddAsync(sale);
                    await _dbContext.SaveChangesAsync();

                    var saleItem = new SaleItems
                    {
                        Quantity = dto.SaleItemsDto!.Quantity,
                        UnitPrice = dto.SaleItemsDto.UnitPrice,
                        TotalPrice = dto.SaleItemsDto.Quantity * dto.SaleItemsDto.UnitPrice,
                        ProductId = dto.SaleItemsDto.ProductId,
                        SaleId = sale.SaleId,
                        AmountPay = dto.SaleItemsDto.AmountPay, 
                        DueAmount = dto.SaleItemsDto.TotalPrice - dto.SaleItemsDto.AmountPay,
                    };

                    await _dbContext.SaleItems.AddAsync(saleItem);
                    await _dbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return SaleDto;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<List<SaleDto>> IGetAllSale()
        {
            var sales = await _dbContext.Sales.Join(
                _dbContext.Customers,
                sale => sale.CustomerId,
                customer => customer.CustomerId,
                (sale, customer) => new { sale, customer }
            ).Join(_dbContext.SaleItems,
                sc => sc.sale.SaleId,
                saleItem => saleItem.SaleId,
                (sc, saleItem) => new SaleDto
                {
                    CustomerId = sc.customer.CustomerId,
                    CustomerName = sc.customer.CustomerName,
                    CustomerPhoneNumber = sc.customer.CustomerPhoneNumber,
                    CustomerAddress = sc.customer.CustomerAddress,
                    SaleDateTransferDto = new SaleDateTransferDto
                    {
                        SaleId = sc.sale.SaleId,
                        SaleNumber = sc.sale.SaleNumber,
                        SaleDate = sc.sale.SaleDate,
                        PaymentStatus = sc.sale.PaymentStatus,
                        PaymentMethod = sc.sale.PaymentMethod
                    },
                    SaleItemsDto = new SaleItemsDto
                    {
                        SalesItemId = saleItem.SalesItemId,
                        Quantity = saleItem.Quantity,
                        UnitPrice = saleItem.UnitPrice,
                        TotalPrice = saleItem.TotalPrice,
                        ProductId = saleItem.ProductId,
                        AmountPay = saleItem.AmountPay,
                        DueAmount = saleItem.DueAmount
                    }
                }
            ).ToListAsync();
            return sales;
        }

        public async Task<List<SaleDto>> IGetSaleById(int SaleId)
        {
            var sale_by_id = await _dbContext.Sales.Where(x => x.SaleId == SaleId)
            .Join(
                _dbContext.Customers,
                sale => sale.CustomerId,
                customer => customer.CustomerId,
                (sale, customer) => new { sale, customer }
            ).Join(_dbContext.SaleItems,
                sc => sc.sale.SaleId,
                saleItem => saleItem.SaleId,
                (sc, saleItem) => new SaleDto
                {
                    CustomerId = sc.customer.CustomerId,
                    CustomerName = sc.customer.CustomerName,
                    CustomerPhoneNumber = sc.customer.CustomerPhoneNumber,
                    CustomerAddress = sc.customer.CustomerAddress,
                    SaleDateTransferDto = new SaleDateTransferDto
                    {
                        SaleId = sc.sale.SaleId,
                        SaleNumber = sc.sale.SaleNumber,
                        SaleDate = sc.sale.SaleDate,
                        PaymentStatus = sc.sale.PaymentStatus,
                        PaymentMethod = sc.sale.PaymentMethod
                    },
                    SaleItemsDto = new SaleItemsDto
                    {
                        SalesItemId = saleItem.SalesItemId,
                        Quantity = saleItem.Quantity,
                        UnitPrice = saleItem.UnitPrice,
                        TotalPrice = saleItem.TotalPrice,
                        ProductId = saleItem.ProductId
                    }
                }
            ).ToListAsync();
            return sale_by_id;
        }

        public async Task<List<DailySaleReportDTO>> IDailySaleReport()
        {
            var dailySale = await _dbContext.SaleItems
                .Join(_dbContext.Sales, si => si.SaleId, s => s.SaleId, (si, s) => new { si, s }).GroupBy(g => g.s.SaleDate.Date).Select(x => new DailySaleReportDTO
                {
                    SaleDay = x.Key,
                    TotalRevenue = x.Sum(y => y.si.TotalPrice),
                }).OrderByDescending(o => o.SaleDay).ToListAsync();
            return dailySale;
        }

        public async Task<List<CustomerSaleHistoryDTO>> ICustomerSaleHistoryReport()
        {
            var CustomerSaleHistory = await _dbContext.Sales
                .Join(_dbContext.SaleItems, s => s.SaleId, si => si.SaleId, (s, si) => new { s, si })
                .GroupBy(g => new { g.s.CustomerId, g.s.Customer!.CustomerName })
                .Select(x => new CustomerSaleHistoryDTO
                {
                    CustomerId = x.Key.CustomerId,
                    CustomerName = x.Key.CustomerName,
                    TotalQuantity = x.Sum(x => x.si.Quantity),
                    TotalAmount = x.Sum(y => y.si.TotalPrice),
                    CustomerPhoneNumber = x.Select(p => p.s.Customer!.CustomerPhoneNumber).FirstOrDefault()!,
                    SaleDate = x.Select(y => y.s.SaleDate.Date).FirstOrDefault(),
                    PaymentMethod = x.Select(p => p.s.PaymentMethod).FirstOrDefault(),
                }).Where(y => y.TotalAmount > 0).OrderByDescending(x => x.SaleDate).ToListAsync();
            return CustomerSaleHistory;
        }

        public async Task<List<ProductWiseSaleReportDTO>> IProductWiseSaleReport()
        {
            var product_wise_report = await _dbContext.SaleItems
                .Join(_dbContext.Products, si => si.ProductId, p => p.ProductId, (si, p) => new { si, p })
                .GroupBy(g => new { g.p.ProductId, g.p.ProductName })
                .Select(x => new ProductWiseSaleReportDTO
                {
                    ProductID = x.Key.ProductId,
                    ProductName = x.Key.ProductName,
                    TotalQuantity = x.Sum(y => y.si.Quantity),
                    TotalRenevue = x.Sum(y => y.si.TotalPrice),
                }).OrderByDescending(x => x.TotalRenevue).ToListAsync();

            return product_wise_report;
        }

        public async Task<List<TodaySaleReportDTO>> ITodaySaleReport(DateOnly todayDate)
        {
            var date = todayDate.ToDateTime(TimeOnly.MinValue);
            var today_report = await _dbContext.Sales
                .Where(w => w.SaleDate.Date == date.Date)
                .Join(_dbContext.SaleItems, s => s.SaleId, si => si.SaleId, (s, si) => new { s, si })
                .Join(_dbContext.Products, si => si.si.ProductId, p => p.ProductId, (si, p) => new { si, p })
                .Join(_dbContext.Customers, s => s.si.s.CustomerId, c => c.CustomerId, (s, c) => new { s, c }).GroupBy(g => new { g.s.p.ProductId, g.s.p.ProductName })
                .Select(x => new TodaySaleReportDTO
                {
                    CustomerID = x.Select(y => y.c.CustomerId).FirstOrDefault(),
                    CustomerName = x.Select(y => y.c.CustomerName).FirstOrDefault()!,
                    ProductID = x.Key.ProductId,
                    ProductName = x.Key.ProductName,
                    TotalQuantity = x.Sum(y => y.s.si.si.Quantity),
                    TotalRenevue = x.Sum(y => y.s.si.si.TotalPrice),
                    CustomerPhoneNumber = x.Select(p => p.c.CustomerPhoneNumber).FirstOrDefault()!,
                    SaleDate = x.Select(d => d.s.si.s.SaleDate).FirstOrDefault()
                }).ToListAsync();

            return today_report;
        }

        public async Task<List<MonthWiseSaleReportDTO>> IMonthWiseSaleReport(int month, int year)
        {
            var monthly_report = await _dbContext.Sales
                .Where(w => w.SaleDate.Month == month && w.SaleDate.Year == year)
                .Join(_dbContext.SaleItems, s => s.SaleId, si => si.SaleId, (s, si) => new { s, si })
                .Join(_dbContext.Products, si => si.si.ProductId, p => p.ProductId, (si, p) => new { si, p }).GroupBy(g => new { g.p.ProductId, g.p.ProductName })
                .Select(x => new MonthWiseSaleReportDTO
                {
                    ProductID = x.Key.ProductId,
                    ProductName = x.Key.ProductName,
                    TotalQuantity = x.Sum(y => y.si.si.Quantity),
                    TotalAmount = x.Sum(y => y.si.si.TotalPrice),
                }).OrderByDescending(x => x.TotalAmount).ToListAsync();
            return monthly_report;
        }

        public async Task<List<MonthlySaleReportDTO>> IMonthlySaleReport(int year)
        {
            var monthlyData = await _dbContext.Sales
                .Where(s => s.SaleDate.Year == year)
                .Join(_dbContext.SaleItems,
                      s => s.SaleId,
                      si => si.SaleId,
                      (s, si) => new { s.SaleDate, si.TotalPrice, si.Quantity })
                .GroupBy(x => x.SaleDate.Month)
                .Select(g => new
                {
                    MonthNumber = g.Key,
                    TotalRevenue = g.Sum(y => y.TotalPrice),
                    TotalQuantity = g.Sum(y => y.Quantity)
                })
                .OrderBy(x => x.MonthNumber)
                .ToListAsync();

            var result = monthlyData.Select(x => new MonthlySaleReportDTO
            {
                MonthNumber = x.MonthNumber,
                MonthName = System.Globalization.CultureInfo.CurrentCulture
                                .DateTimeFormat.GetAbbreviatedMonthName(x.MonthNumber),
                TotalRevenue = x.TotalRevenue,
                TotalQuantity = x.TotalQuantity
            }).ToList();

            return result;
        }

    }
}
