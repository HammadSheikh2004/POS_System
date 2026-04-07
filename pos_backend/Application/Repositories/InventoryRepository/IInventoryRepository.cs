using Domain.DTOs;
using Domain.Entity.InventoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.InventoryRepository
{
    public interface IInventoryRepository
    {
        public Task<List<ProductDTOs>> IAddProducts(List<ProductDTOs> products);
        public Task<List<FetchProductDTOs>> IFetchAllProducts();
        public Task<List<ProductNameDTO>> IFetchProductsName();
        public Task<List<FetchProductDTOs>> IFetchProductsByProduct(int id);
        public Task<FetchProductDTOs> IUpdateProducts(FetchProductDTOs products, int id);
        public Task<bool> IDeleteProducts(int id);
    }
}
