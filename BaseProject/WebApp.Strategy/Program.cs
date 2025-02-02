using WebApp.Strategy.Models;
using IdentityBase.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Repositories;
using System.Security.Claims;

namespace IdentityBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IProductRepository>(sp =>
            {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var claims = httpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type == Settings.ClaimDataBaseType).FirstOrDefault();
                var dbContext = sp.GetRequiredService<IdentityContext>();
                if(claims == null) return new ProductRepositorySqlServer(dbContext);

                var databaseType = (EDatabaseType)int.Parse(claims.Value);

                return databaseType switch
                {
                    EDatabaseType.SqlServer=> new ProductRepositorySqlServer(dbContext),
                    EDatabaseType.MongoDb => new ProductRepositoryMongoDb(builder.Configuration),
                    _ => throw new NotImplementedException("Database type not implemented")
                };
            });

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
