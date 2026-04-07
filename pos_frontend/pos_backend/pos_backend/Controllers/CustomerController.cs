using Application.Repositories.InventoryRepository;
using Domain.DTOs;
using Infrastructure.RepositoryImplementaion.InventoryImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pos_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("OnPageReport/{first_date}/{sec_date}/{customerName}")]
        public async Task<IActionResult> OnPageReport(DateOnly first_date, DateOnly sec_date, string customerName)
        {
            var data = await _customerRepository.ICustomerOnPageReport(first_date, sec_date, customerName);
            return Ok(data);
        }

        [HttpGet("CustomerAgingReport")]
        public async Task<IActionResult> CustomerAgingReport()
        {
            var aging_report = await _customerRepository.ICustomerAgingReport();
            return Ok(aging_report);
        }
    }
}
