using Application.Repositories.InventoryRepository;
using Application.Services.InventoryService;
using Application.ServicesInterfaces.InventoryServiceInterface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extension
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services) 
        {
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ICustomerService, CustomerService>();
            return services;
        }
    }
}
