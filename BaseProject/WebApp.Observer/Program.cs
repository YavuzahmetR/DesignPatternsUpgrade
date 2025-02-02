using IdentityBase.Models;
using IdentityBase.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using WebApp.Observer.Email;
using WebApp.Observer.Observer;

namespace IdentityBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IUserObserverSubject, UserObserverSubject>(sp =>
            {
                var userObserverSubject = new UserObserverSubject();
                var emailService = sp.GetRequiredService<IEmailService>();

                userObserverSubject.RegisterObserver(new UserObserverConsoleData(sp));
                userObserverSubject.RegisterObserver(new UserObserverCreateDiscount(sp));
                userObserverSubject.RegisterObserver(new UserObserverSendEmail(sp,emailService));
                return userObserverSubject;
            });

            var smtpConfig = builder.Configuration.GetSection("SmtpSettings");
            builder.Services.AddFluentEmail(smtpConfig["FromAddress"]).AddSmtpSender(new SmtpClient(smtpConfig["Host"])
            {
                Port = int.Parse(smtpConfig["Port"]!),
                Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["AppPassword"]),
                EnableSsl = true
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
