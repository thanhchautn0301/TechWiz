using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechWizProject.Models;
using TechWizProject.Areas.Admin.Services;
using TechWizProject.Areas.Users.Services;
using TechWizProject.Services;
using OrderService = TechWizProject.Services.OrderService;

namespace TechWizProject
{
    public class Startup
    {
        private IConfiguration iConfiguration;

        public Startup(IConfiguration _iConfiguration)
        {
            iConfiguration = _iConfiguration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddSession();
            var connectionString = iConfiguration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(option =>
              option.UseLazyLoadingProxies().UseSqlServer(connectionString));
            services.AddDbContext<Areas.Admin.Models.DatabaseContext>(option =>
                option.UseLazyLoadingProxies().UseSqlServer(connectionString));

            services.AddScoped<IRestaurantsServies, RestaurantsServicesImpl>();

            services.AddScoped<FoodService, FoodServiceImpl>();
            services.AddScoped<IFoodsService, FoodsService>();
            services.AddScoped<CartService, CartServiceImpl>();
            services.AddScoped<IUserService, UserServiceImpl>();
            services.AddScoped<OrderService, OrderServiceImpl>();
            services.AddScoped<CategoryService, CategoryServiceImpl>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IOrderService, IOrderServiceImpl>();
            services.AddScoped<TagService, TagServiceImpl>();
            services.AddScoped<OrderTrackingService, OrderTrackingServiceImpl>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}"
                );
            });
        }
    }
}
