using IdentityBase.Models;
using IdentityBase.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Decorator.Repos;
using WebApp.Decorator.Repos.Decorator;

namespace IdentityBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddMemoryCache();
            builder.Services.AddHttpContextAccessor();

            #region Register the repository with the decorator First Way
            // Register the repository with the decorator FÝRST WAY
            //builder.Services.AddScoped<IProductRepository>(sp =>
            //{
            //    var dbContext = sp.GetRequiredService<IdentityContext>();
            //    var productRepository = new ProductRepository(dbContext);
            //    var memoryCache = sp.GetRequiredService<IMemoryCache>();
            //    var logService = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();


            //    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
            //    var loggingDecorator = new ProductRepositoryLoggingDecorator(cacheDecorator, logService);

            //    return loggingDecorator;
            //});
            #endregion

            #region Register the repo with Library Structor SECOND WAY
            // Decorator with Library Strucotor SECOND WAY
            builder.Services.AddScoped<IProductRepository, ProductRepository>().
                Decorate<IProductRepository, ProductRepositoryCacheDecorator>().
                Decorate<IProductRepository, ProductRepositoryLoggingDecorator>();
            #endregion

            #region Register the repo in Runtime
            // Register the repository in Runtime
            builder.Services.AddScoped<IProductRepository>(sp =>
            {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var dbContext = sp.GetRequiredService<IdentityContext>();
                var productRepository = new ProductRepository(dbContext);
                var memoryCache = sp.GetRequiredService<IMemoryCache>();
                var logService = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();
                if (httpContextAccessor.HttpContext!.User.Identity!.Name == "User1")
                {
                    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
                    return cacheDecorator;
                }

                var loggingDecorator = new ProductRepositoryLoggingDecorator(productRepository, logService);
                return loggingDecorator;
            });
            #endregion


            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityContext>();

            var app = builder.Build();

            SeedData.SeedAsync(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
