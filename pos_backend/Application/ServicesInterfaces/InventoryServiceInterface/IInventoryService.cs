using Domain.DTOs;
using Domain.Entity.InventoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServicesInterfaces.InventoryServiceInterface
{
    public interface IInventoryService
    {
        public Task<List<ProductDTOs>> IAddProductsService(List<ProductDTOs> products);
        public Task<List<FetchProductDTOs>> IFetchAllProductService();
        public Task<List<ProductNameDTO>> IFetchProductsNameService();
        public Task<List<FetchProductDTOs>> IFetchProductsByProductService(int id);
        public Task<FetchProductDTOs> IUpdateProductService(FetchProductDTOs products, int id);
        public Task<bool> IDeleteProductService(int id);
    }
}
