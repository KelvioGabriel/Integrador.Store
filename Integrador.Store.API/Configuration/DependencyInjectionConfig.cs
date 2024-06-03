using Microsoft.AspNetCore.Identity;
using Integrador.Application.Interfaces;
using Integrador.Application.Services;
using Integrador.Domain.Interfaces;
using Integrador.Domain.Shared.Transaction;
using Integrador.Infra.Data.Context;
using Integrador.Infra.Data.Repositories;
using Integrador.Infra.Data.UoW;
using Integrador.Infra.Identity.Data;
using Integrador.Infra.Identity.Services;
using System.Reflection;

namespace Integrador.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMediatr();
            services.AddRepositories();
            services.AddServices();
        }

        public static void AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly()));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EShopDbContext>();
            services.AddScoped<IdentityDataContext>();

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAddressAppService, AddressAppService>();
            services.AddScoped<IBasketAppService, BasketAppService>();
            services.AddScoped<IClientAppService, ClientAppService>();
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IPaymentMethodAppService, PaymentMethodAppService>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IVoucherAppService, VoucherAppService>();
        }
    }
}
