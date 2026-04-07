using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.InventoryRepository
{
    public interface IPurchaseOrderRepository
    {
        public Task<PODTOs> IAddPurchaseOrder(PODTOs poDtos);
        public Task<List<PODTOs>> IGetAllPurchaseOrders();
        public Task<List<PODTOs>> IGetPOById(int id);
        public Task<PODTOs> IUpdatePO(PODTOs poDtos, int id);
        public Task<bool> IDeletePO(int id);
    }
}
