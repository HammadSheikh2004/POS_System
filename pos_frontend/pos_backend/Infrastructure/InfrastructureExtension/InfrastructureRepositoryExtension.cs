using Application.Repositories.InventoryRepository;
using Infrastructure.RepositoryImplementaion.InventoryImplementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.InfrastructureExtension
{
    public static class InfrastructureRepositoryExtension
    {
        public static IServiceCollection AddInfrastructureRepository(this IServiceCollection services)
        {
            services.AddScoped<IInventoryRepository, InventoryImplementation>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderImplementation>();
            services.AddScoped<ISaleRepository, SaleImplementation>();
            services.AddScoped<ICustomerRepository, CustomerImplementation>();

            return services;
        }
    }
}
