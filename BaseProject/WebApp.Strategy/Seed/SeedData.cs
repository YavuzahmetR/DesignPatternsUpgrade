using WebApp.Strategy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityBase.Seed
{
    public static class SeedData
    {
        public static void SeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                identityContext.Database.Migrate();
                if (!userManager.Users.Any())
                {
                     userManager.CreateAsync(new AppUser() { UserName = "User1", Email = "user1@gmail.com" }, "Password12*").Wait();
                     userManager.CreateAsync(new AppUser() { UserName = "User2", Email = "user2@gmail.com" }, "Password12*").Wait();
                     userManager.CreateAsync(new AppUser() { UserName = "User3", Email = "user3@gmail.com" }, "Password12*").Wait();
                     userManager.CreateAsync(new AppUser() { UserName = "User4", Email = "user4@gmail.com" }, "Password12*").Wait();
                     userManager.CreateAsync(new AppUser() { UserName = "User5", Email = "user5@gmail.com" }, "Password12*").Wait();
                }
            }
        }
    }
}
