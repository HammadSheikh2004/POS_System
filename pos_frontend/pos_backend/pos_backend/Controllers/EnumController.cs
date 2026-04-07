using Domain.Entity.Sales;
using Infrastructure.HelperClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pos_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        [HttpGet("GetPaymentStatus")]
        public IActionResult GetPaymentStatus() {
            return Ok(EnumHelperClass.EnumList<PaymentStatus>());
        }
        [HttpGet("GetPaymentMethod")]
        public IActionResult GetPaymentMethod()
        {
            return Ok(EnumHelperClass.EnumList<PaymentMethod>());
        }
    }
}
