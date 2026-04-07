using Application.Repositories.InventoryRepository;
using Domain.DTOs;
using Domain.Entity.InventoryEntities;
using Domain.Entity.PurchaseOrderEntities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementaion.InventoryImplementation
{
    public class PurchaseOrderImplementation : IPurchaseOrderRepository
    {
        private readonly POSDbContext _posDbContext;
        public PurchaseOrderImplementation(POSDbContext posDbContext)
        {
            this._posDbContext = posDbContext;
        }
        private string GeneratePONumber()
        {
            var lastPONumber = _posDbContext.PurchaseOrder
                .OrderByDescending(po => po.Id)
                .Select(po => po.OrderNumber)
                .FirstOrDefault();
            int newPONumber = 1;
            if (!string.IsNullOrEmpty(lastPONumber) && int.TryParse(lastPONumber, out int lastNumber))
            {
                newPONumber = lastNumber + 1;
            }
            return newPONumber.ToString("D6"); 
        }
        public async Task<PODTOs> IAddPurchaseOrder(PODTOs poDtos)
        {
            var product = await _posDbContext.Products.FirstOrDefaultAsync(x => x.ProductName == poDtos.productName);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            product.QuantityInStock += poDtos.Items;
            await _posDbContext.SaveChangesAsync();
            var po = new POEntity
            {
                OrderNumber = GeneratePONumber(),
                OrderDate = DateTime.Now,
                SupplierName = poDtos.SupplierName,
                Items = poDtos.Items,
                CostPricePerItem = poDtos.CostPricePerItem,
                TotalAmount = poDtos.Items * poDtos.CostPricePerItem,
                ProductName = poDtos.productName!
            };
            _posDbContext.PurchaseOrder.Add(po);
            await _posDbContext.SaveChangesAsync();

            var result = new PODTOs
            {
                OrderNumber = po.OrderNumber,
                OrderDate = po.OrderDate,
                SupplierName = po.SupplierName,
                Items = po.Items,
                CostPricePerItem = po.CostPricePerItem,
                TotalAmount = po.Items * po.CostPricePerItem,
                productName = poDtos.productName
            };

            return result;
        }

        public async Task<List<PODTOs>> IGetAllPurchaseOrders()
        {
            try
            {
                var data = await _posDbContext.PurchaseOrder.Select(x => new PODTOs
                {
                    Id = x.Id,   
                    OrderNumber = x.OrderNumber,
                    OrderDate = x.OrderDate,
                    SupplierName = x.SupplierName,
                    Items = x.Items,
                    CostPricePerItem = x.CostPricePerItem,
                    TotalAmount = x.TotalAmount,
                    productName = x.ProductName
                }).ToListAsync();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PODTOs>> IGetPOById(int id)
        {
            var po = await _posDbContext.PurchaseOrder.FindAsync(id);
            if (po == null)
            {
                return null!;
            }
            var result = new PODTOs
            {
                Id = po!.Id,
                OrderNumber = po!.OrderNumber,
                OrderDate = po!.OrderDate,
                SupplierName = po!.SupplierName,
                Items = po!.Items,
                CostPricePerItem = po!.CostPricePerItem,
                TotalAmount = po!.TotalAmount
            };
            return new List<PODTOs> { result };
        }

        public async Task<PODTOs> IUpdatePO(PODTOs poDtos, int id)
        {
            var po_existingData = await _posDbContext.PurchaseOrder.FindAsync(id);
            if (po_existingData == null)
            {
                return null!;
            }
            po_existingData.SupplierName = poDtos.SupplierName;
            po_existingData.Items = poDtos.Items;
            po_existingData.CostPricePerItem = poDtos.CostPricePerItem;
            po_existingData.TotalAmount = poDtos.Items * poDtos.CostPricePerItem;
            await _posDbContext.SaveChangesAsync();
            return new PODTOs
            {
                Id = poDtos.Id,
                SupplierName = poDtos.SupplierName,
                CostPricePerItem = poDtos.CostPricePerItem,
                Items = poDtos.Items,
                TotalAmount = poDtos.Items * poDtos.CostPricePerItem,
            };
        }

        public async Task<bool> IDeletePO(int id)
        {
            var data = await _posDbContext.PurchaseOrder.FindAsync(id);
            if (data == null)
            {
                return false;
            }
            _posDbContext.PurchaseOrder.Remove(data);
            await _posDbContext.SaveChangesAsync();
            return true;
        }
    }
}
