using Application.ServicesInterfaces.InventoryServiceInterface;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pos_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public IInventoryService InventoryService { get; set; }
        public IPurchaseOrderService PurchaseOrderService { get; set; }
        public InventoryController(IInventoryService inventoryService, IPurchaseOrderService purchaseOrderService)
        {
            InventoryService = inventoryService;
            PurchaseOrderService = purchaseOrderService;
        }

        [HttpPost("AddInventory")]
        public async Task<IActionResult> AddInventory([FromBody] List<ProductDTOs> productDTOs)
        {
            if (productDTOs == null)
            {
                return BadRequest(new { ErrorMessage = "Products data is Required!" });
            }
            var result = await InventoryService.IAddProductsService(productDTOs);
            return Ok(new { SuccessMessage = "Inventory Added Succesfully!", result });
        }

        [HttpGet("FetchAllProducts")]
        public async Task<IActionResult> FetcAllProducts()
        {
            var result = await InventoryService.IFetchAllProductService();
            if (result == null)
            {
                return BadRequest(new { ErrorMessage = "Prodcut is Null!" });
            }
            return Ok(result);
        }
        [HttpGet("FetchProductsById/{id}")]
        public async Task<IActionResult> FetchProductsById(int id)
        {
            var result = await InventoryService.IFetchProductsByProductService(id);
            if (result == null)
            {
                return BadRequest(new { ErrorMessage = "Prodcut is Null!" });
            }
            return Ok(result);
        }
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(FetchProductDTOs productDTOs, int id)
        {
            var result = await InventoryService.IUpdateProductService(productDTOs, id);
            if (result == null)
            {
                return BadRequest(new { ErrorMessage = "Prodcut is Null!" });
            }
            return Ok(new { SuccessMessage = "Product is Update!", result });
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await InventoryService.IDeleteProductService(id);
            if (!result)
            {
                return BadRequest(new { ErrorMessage = "Product is Not Deleted!" });
            }
            return Ok(new { SuccessMessage = "Product is Deleted!" });

        }
        [HttpPost("AddPurchaseOrder")]
        public async Task<IActionResult> AddPurchaseOrder([FromBody] PODTOs poDtos)
        {
            if (poDtos == null)
            {
                return BadRequest(new { ErrorMessage = "Purchase Order data is Required!" });
            }
            try
            {
                var result = await PurchaseOrderService.IAddPurchaseOrderService(poDtos);
                return Ok(new { SuccessMessage = "Purchase Order Added Succesfully!", result });
            }
            catch (Exception ex)
            {

                return BadRequest(new { ErrorMessage = ex.Message });
            }
            
        }
        [HttpGet("FetchAllPurchaseOrders")]
        public async Task<IActionResult> FetchAllPurchaseOrders()
        {
            var result = await PurchaseOrderService.IGetAllPurchaseOrderService();
            if (result == null)
            {
                return BadRequest(new { ErrorMessage = "Purchase Orders is Null!" });
            }
            return Ok(result);
        }
        [HttpGet("GetPOById/{id}")]
        public async Task<IActionResult> GetPOById(int id)
        {
            var result = await PurchaseOrderService.IGetPOByIdService(id);
            if (result == null)
            {
                return BadRequest(new { ErrorMessage = $"Id {id} not found!" });
            }
            return Ok(result);
        }
        [HttpPut("UpdatePO/{id}")]
        public async Task<IActionResult> UpdatePO(PODTOs poDto, int id)
        {
            var result = await PurchaseOrderService.IUpdatePOService(poDto, id);
            if (result == null)
            {
                return BadRequest(new { ErrorMessage = $"Can not update purchase order by this ID {id}" });
            }
            return Ok(new { SuccessMessage = "Purchase Order successfully Updated!", result });
        }
        [HttpDelete("DeletePO/{id}")]
        public async Task<IActionResult> DeletePO(int id)
        {
            var result = await PurchaseOrderService.IDeletePOService(id);
            if (result == false)
            {
                return BadRequest(new { ErrorMessage = $"Can not Delete purchase order by this ID {id}" });
            }
            return Ok(new { SuccessMessage = "Purchase Order Delete Successfully!" });
        }
        [HttpGet("FetchProductsName")]
        public async Task<IActionResult> FetchProductsName()
        {
            var result = await InventoryService.IFetchProductsNameService();
            if (result == null)
            {
                return BadRequest(new { ErrorMessage = "Prodcut is Null!" });
            }
            return Ok(result);
        }
    }
}
