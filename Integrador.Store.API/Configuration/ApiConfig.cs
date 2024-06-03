using Integrador.Application.AutoMapper;
using Senac.eShop.API.Configuration;
using Serilog;

namespace Integrador.API.Configuration
{
    public static class ApiConfig
    {
        public static void ConfigureStartupConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCors(c => c.AddPolicy("MyPolicy",
                policy => policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            services.AddControllers();
            services.AddAuthentication(configuration); //Essa linha aqui
            services.RegisterServices();
            AddJwtConfiguration(services);

            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.DbContextConfigureServices(configuration);
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

        }

        private static void AddJwtConfiguration(IServiceCollection services)
        {

        }

        public static WebApplication UseStartupConfiguration(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseAuthorization();
            app.UseCors("MyPolicy");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            return app;
        }

        public static void ConfigureSerilog(this ConfigureHostBuilder builder)
        {
            builder.UseSerilog((ctx, lc) => lc.WriteTo.Console());
        }
    }
}
