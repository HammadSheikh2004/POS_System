using Application.Repositories.InventoryRepository;
using Domain.DTOs;
using Domain.Entity.InventoryEntities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementaion.InventoryImplementation
{
    public class InventoryImplementation : IInventoryRepository
    {
        private readonly POSDbContext _posDbContext;
        public InventoryImplementation(POSDbContext posDbContext)
        {
            this._posDbContext = posDbContext;
        }
        public async Task<List<ProductDTOs>> IAddProducts(List<ProductDTOs> products)
        {
            using var transaction = await _posDbContext.Database.BeginTransactionAsync();
            try
            {
                var existingBrand = await _posDbContext.Brands.ToListAsync();
                var existingCategory = await _posDbContext.Categories.ToListAsync();
                var existingUnit = await _posDbContext.Units.ToListAsync();

                var newBrand = new List<Brand>();
                var newCategory = new List<Category>();
                var newUnit = new List<Units>();

                foreach (var product in products)
                {
                    var brand = existingBrand.FirstOrDefault(b => b.BrandName == product.BrandName) ?? newBrand.FirstOrDefault(b => b.BrandName == product.BrandName);
                    if (brand == null)
                    {
                        brand = new Brand { BrandName = product.BrandName };
                        newBrand.Add(brand);
                    }

                    var category = existingCategory.FirstOrDefault(c => c.CategoryName == product.CategoryName) ?? newCategory.FirstOrDefault(c => c.CategoryName == product.CategoryName);
                    if (category == null)
                    {
                        category = new Category { CategoryName = product.CategoryName };
                        newCategory.Add(category);
                    }

                    var unit = existingUnit.FirstOrDefault(u => u.UnitName == product.UnitName) ?? newUnit.FirstOrDefault(u => u.UnitName == product.UnitName);
                    if (unit == null)
                    {
                        unit = new Units { UnitName = product.UnitName };
                        newUnit.Add(unit);
                    }
                }
                if (newBrand.Any())
                {
                    await _posDbContext.Brands.AddRangeAsync(newBrand);
                }
                if (newCategory.Any())
                {
                    await _posDbContext.Categories.AddRangeAsync(newCategory);
                }
                if (newUnit.Any())
                {
                    await _posDbContext.Units.AddRangeAsync(newUnit);
                }
                await _posDbContext.SaveChangesAsync();

                existingBrand = await _posDbContext.Brands.ToListAsync();
                existingCategory = await _posDbContext.Categories.ToListAsync();
                existingUnit = await _posDbContext.Units.ToListAsync();

                var newProduct = new List<Products>();

                foreach (var product in products)
                {
                    var brand = existingBrand.FirstOrDefault(b => b.BrandName == product.BrandName);
                    var category = existingCategory.FirstOrDefault(c => c.CategoryName == product.CategoryName);
                    var unit = existingUnit.FirstOrDefault(u => u.UnitName == product.UnitName);

                    var pro = new Products
                    {
                        ProductName = product.ProductName,
                        CostPrice = product.CostPrice,
                        SellingPrice = product.SellingPrice,
                        ReorderLevel = product.ReorderLevel,
                        BrandId = brand!.BrandId,
                        CategoryId = category!.CategoryId,
                        UnitId = unit!.UnitId,
                        QuantityInStock = product.QuantityInStock
                    };
                    newProduct.Add(pro);
                }
                await _posDbContext.AddRangeAsync(newProduct);
                await _posDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return products;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<List<FetchProductDTOs>> IFetchAllProducts()
        {
            try
            {
                var product = await _posDbContext.Products
               .Join(_posDbContext.Brands, p => p.BrandId, b => b.BrandId, (p, b) => new { p, b })
               .Join(_posDbContext.Categories, p => p.p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
               .Join(_posDbContext.Units, p => p.p.p.UnitId, u => u.UnitId, (p, u) => new FetchProductDTOs
               {
                   ProductId = p.p.p.ProductId,
                   ProductName = p.p.p.ProductName,
                   SellingPrice = p.p.p.SellingPrice,
                   CostPrice = p.p.p.CostPrice,
                   ReorderLevel = p.p.p.ReorderLevel,
                   QuantityInStock = p.p.p.QuantityInStock,

                   Category = new CategoryDTO
                   {
                       CategoryId = p.c.CategoryId,
                       CategoryName = p.c.CategoryName
                   },

                   Brand = new BrandDTO
                   {
                       BrandId = p.p.b.BrandId,
                       BrandName = p.p.b.BrandName,
                   },

                   Unit = new UnitDTO
                   {
                       UnitId = u.UnitId,
                       UnitName = u.UnitName,
                   }
               }).ToListAsync();

                return product;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<FetchProductDTOs>> IFetchProductsByProduct(int id)
        {
            var data = await _posDbContext.Products
                .Where(p => p.ProductId == id)
                .Join(_posDbContext.Brands, p => p.BrandId, b => b.BrandId, (p, b) => new { p, b })
                .Join(_posDbContext.Categories, p => p.p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_posDbContext.Units, p => p.p.p.UnitId, u => u.UnitId, (p, u) => new FetchProductDTOs
                {
                    ProductId = p.p.p.ProductId,
                    ProductName = p.p.p.ProductName,
                    SellingPrice = p.p.p.SellingPrice,
                    CostPrice = p.p.p.CostPrice,
                    ReorderLevel = p.p.p.ReorderLevel,
                    QuantityInStock = p.p.p.QuantityInStock,
                    Category = new CategoryDTO
                    {
                        CategoryId = p.c.CategoryId,
                        CategoryName = p.c.CategoryName
                    },
                    Brand = new BrandDTO
                    {
                        BrandId = p.p.b.BrandId,
                        BrandName = p.p.b.BrandName,
                    },
                    Unit = new UnitDTO
                    {
                        UnitId = u.UnitId,
                        UnitName = u.UnitName,
                    }
                }).ToListAsync();
            return data;
        }
        public async Task<FetchProductDTOs> IUpdateProducts(FetchProductDTOs products, int id)
        {
            var existingData = await _posDbContext.Products.FindAsync(id);
            if (existingData == null)
            {
                throw new KeyNotFoundException($"Product with id {id} not found!");
            }

            existingData.ProductName = products.ProductName;
            existingData.SellingPrice = products.SellingPrice;
            existingData.CostPrice = products.CostPrice;
            existingData.ReorderLevel = products.ReorderLevel;
            existingData.CategoryId = products.Category.CategoryId;
            existingData.BrandId = products.Brand.BrandId;
            existingData.UnitId = products.Unit.UnitId;
            existingData.QuantityInStock = products.QuantityInStock;

            await _posDbContext.SaveChangesAsync();

            return new FetchProductDTOs
            {
                ProductName = products.ProductName,
                CostPrice = products.CostPrice,
                SellingPrice = products.SellingPrice,
                ReorderLevel = products.ReorderLevel,
                QuantityInStock = products.QuantityInStock,
                Category = new CategoryDTO
                {
                    CategoryId = products.Category.CategoryId,
                    CategoryName = products.Category.CategoryName,
                },
                Brand = new BrandDTO
                {
                    BrandId = products.Brand.BrandId,
                    BrandName = products.Brand.BrandName,
                },
                Unit = new UnitDTO
                {
                    UnitId = products.Unit.UnitId,
                    UnitName = products.Unit.UnitName,
                }
            };

        }
        public async Task<bool> IDeleteProducts(int id)
        {
            var productId = await _posDbContext.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Units)
                    .FirstOrDefaultAsync(p => p.ProductId == id);
            var hasSales = await _posDbContext.SaleItems
                .AnyAsync(s => s.ProductId == id);

            if (hasSales)
            {
                throw new InvalidOperationException("Cannot delete product because it is used in sales.");
            }

            if (productId == null)
            {
                return false;
            }
            _posDbContext.Products.Remove(productId);
            await _posDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductNameDTO>> IFetchProductsName()
        {
            var data = await _posDbContext.Products
                .Select(p => new ProductNameDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName
                }).Distinct().ToListAsync();
            return data;
        }
    }
}

