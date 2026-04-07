using Domain.Entity.InventoryEntities;
using Domain.Entity.PurchaseOrderEntities;
using Domain.Entity.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infrastructure.Context
{
    public class POSDbContext : DbContext
    {
        public POSDbContext(DbContextOptions<POSDbContext> options) : base(options)
        {
            
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<POEntity> PurchaseOrder { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItems> SaleItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>().HasOne<Units>().WithMany(u => u.Products).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Products>().HasOne<Category>().WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Products>().HasOne<Brand>().WithMany(x => x.Products).HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SaleItems>()
             .HasOne(si => si.Product)            // navigation property in SaleItems
             .WithMany(p => p.SaleItems)          // navigation property in Products
             .HasForeignKey(si => si.ProductId)
             .OnDelete(DeleteBehavior.Restrict);

            // SaleItems -> Sale (many SaleItems belong to one Sale)
            modelBuilder.Entity<SaleItems>()
                .HasOne(si => si.Sale)               // navigation property in SaleItems
                .WithMany(s => s.SaleItems)          // navigation property in Sale
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sale -> Customer (many Sales belong to one Customer)
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)             // navigation property in Sale
                .WithMany(c => c.Sales)              // navigation property in Customer
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
