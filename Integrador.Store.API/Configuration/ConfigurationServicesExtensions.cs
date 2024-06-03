using Microsoft.EntityFrameworkCore;
using Integrador.Infra.Data.Context;
using Integrador.Infra.Identity.Data;

namespace Integrador.API.Configuration
{
    public static class ConfigurationServicesExtensions
    {
        public static IServiceCollection DbContextConfigureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<EShopDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(connectionString));

            services.AddDistributedMemoryCache();

            return services;
        }
    }
}
