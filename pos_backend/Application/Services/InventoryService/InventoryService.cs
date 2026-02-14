using Application.Repositories.InventoryRepository;
using Application.ServicesInterfaces.InventoryServiceInterface;
using Domain.DTOs;
using Domain.Entity.InventoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _InventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _InventoryRepository = inventoryRepository;
        }
        public async Task<List<ProductDTOs>> IAddProductsService(List<ProductDTOs> products)
        {
            return await _InventoryRepository.IAddProducts(products);
        }

        public async Task<List<FetchProductDTOs>> IFetchAllProductService()
        {
            return await _InventoryRepository.IFetchAllProducts();
        }

        public async Task<List<FetchProductDTOs>> IFetchProductsByProductService(int id)
        {
            return await _InventoryRepository.IFetchProductsByProduct(id);
        }
        public async Task<FetchProductDTOs> IUpdateProductService(FetchProductDTOs productDTOs, int id)
        {
            return await _InventoryRepository.IUpdateProducts(productDTOs, id);
        }
        public async Task<bool> IDeleteProductService(int id){
            return await _InventoryRepository.IDeleteProducts(id);
        }

        public async Task<List<ProductNameDTO>> IFetchProductsNameService()
        {
            return await _InventoryRepository.IFetchProductsName();
        }
    }
}
