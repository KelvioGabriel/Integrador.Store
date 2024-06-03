namespace Integrador.Web.Configuration
{
    public static class WebConfig
    {
        public static void ConfigureStartupConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddControllersWithViews();
            services.AddMvcConfiguration(configuration);
            services.RegisterServices(configuration);
        }

        public static WebApplication UseStartupConfiguration(this WebApplication app, 
            IWebHostEnvironment env)
        {
            app.UseMvcConfiguration(env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return app;
        }
    }
}
