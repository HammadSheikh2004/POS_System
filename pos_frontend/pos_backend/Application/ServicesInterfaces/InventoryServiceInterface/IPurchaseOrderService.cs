using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServicesInterfaces.InventoryServiceInterface
{
    public interface IPurchaseOrderService
    {
        public Task<PODTOs> IAddPurchaseOrderService(PODTOs poDtos);
        public Task<List<PODTOs>> IGetAllPurchaseOrderService();
        public Task<List<PODTOs>> IGetPOByIdService(int id);
        public Task<PODTOs> IUpdatePOService(PODTOs poDtos, int id);
        public Task<bool> IDeletePOService(int id);
    }
}
