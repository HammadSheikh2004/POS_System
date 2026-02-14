using Application.ServicesInterfaces.InventoryServiceInterface;
using Domain.DTOs;
using Domain.DTOValidators;
using Domain.Entity.Sales;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pos_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        public ISaleService SaleService { get; set; }
        private readonly IValidator<SaleDto> _validator;
        public SaleController(ISaleService saleService, IValidator<SaleDto> validator)
        {
            this.SaleService = saleService;
            this._validator = validator;
        }
        [HttpPost("AddSale")]
        public async Task<IActionResult> AddSale([FromBody] List<SaleDto> saleDtos)
        {
            if (saleDtos == null || !saleDtos.Any())
            {
                return BadRequest(new { ErrorMessage = "All Fields is Required!" });
            }
            try
            {
                foreach (var dto in saleDtos)
                {
                    var result = await _validator.ValidateAsync(dto);
                    if (!result.IsValid)
                    {
                        return BadRequest(result.Errors);
                    }
                }

                var data = await SaleService.ICreateSaleService(saleDtos);
                return Ok(new { SuccessMessage = "Data Save!", data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
                throw;
            }
        }
        [HttpGet("GetAllSale")]
        public async Task<IActionResult> GetAllSale()
        {
            var data = await SaleService.IGetAllSaleService();
            return Ok(new { data });
        }
        [HttpGet("GetSaleById/{SaleId}")]
        public async Task<IActionResult> GetSaleById(int SaleId)
        {
            var data = await SaleService.IGetSaleByIdService(SaleId);
            return Ok(new { data });
        }
        [HttpGet("DailySaleReport")]
        public async Task<IActionResult> DailySaleReport()
        {
            try
            {
                var data = await SaleService.IDailySaleReportService();
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("CustomerSaleHistoryReport")]
        public async Task<IActionResult> CustomerSaleHistoryReport()
        {
            try
            {
                var data = await SaleService.ICustomerSaleHistoryReportService();
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("ProductWiseSaleReport")]
        public async Task<IActionResult> ProductWiseSaleReport()
        {
            try
            {
                var data = await SaleService.IProductWiseSaleReportService();
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("TodaySaleReport/{todayDate}")]
        public async Task<IActionResult> TodaySaleReport(DateOnly todayDate)
        {
            try
            {
                if (todayDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    return BadRequest(new { success = false, ErrorMessage = "Future Date is not allowed!" });
                }
                var data = await SaleService.ITodaySaleReportService(todayDate);
                if (data == null || !data.Any())
                {
                    return NotFound(new { success = false, ErrorMessage = "No sale data found for the selected date!" });
                }
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("MonthlySaleReport/{month}/{year}")]
        public async Task<IActionResult> MonthlySaleReport(int month, int year)
        {
            try
            {
                if (month < 1 || month > 12)
                {
                    return BadRequest(new { success = false, ErrorMessage = "Invalid month. Please provide a value between 1 and 12." });
                }
                if (year < 1 || year > DateTime.Now.Year)
                {
                    return BadRequest(new { success = false, ErrorMessage = "Invalid year. Please provide a valid year." });
                }
                var data = await SaleService.IMonthWiseSaleReportService(month, year);
                if (data == null || !data.Any())
                {
                    return NotFound(new { success = false, ErrorMessage = "No sale data found for the selected month and year!" });
                }
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("YearlySaleReport/{year}")]
        public async Task<IActionResult> YearlySaleReport(int year)
        {
            try
            {
                if (year < 1 || year > DateTime.Now.Year)
                {
                    return BadRequest(new { success = false, ErrorMessage = "Invalid year. Please provide a valid year." });
                }
                var data = await SaleService.IMonthlySaleReportService(year);
                if (data == null || !data.Any())
                {
                    return NotFound(new { success = false, ErrorMessage = "No sale data found for the selected year!" });
                }
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
