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
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository purchaseOrderRepository;
        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            this.purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<PODTOs> IAddPurchaseOrderService(PODTOs poDtos)
        {
            return await purchaseOrderRepository.IAddPurchaseOrder(poDtos);
        }

        public async Task<List<PODTOs>> IGetAllPurchaseOrderService()
        {
            return await purchaseOrderRepository.IGetAllPurchaseOrders();
        }

        public async Task<List<PODTOs>> IGetPOByIdService(int id)
        {
            return await purchaseOrderRepository.IGetPOById(id);
        }

        public async Task<PODTOs> IUpdatePOService(PODTOs poDtos, int id)
        {
            return await purchaseOrderRepository.IUpdatePO(poDtos, id);
        }

        public async Task<bool> IDeletePOService(int id)
        {
            return await purchaseOrderRepository.IDeletePO(id);
        }
    }
}
