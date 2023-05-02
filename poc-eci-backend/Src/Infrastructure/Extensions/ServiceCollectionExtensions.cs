using Domain.Interfaces.Repository;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MediatR;
using System.Reflection;
using Application.CQRS.Handlers.Category;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            string? connectionString = configuration.GetConnectionString("SQLServer");
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
