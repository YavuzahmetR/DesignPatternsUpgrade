using IdentityBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Composite.Models;

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
                    var appUser = new AppUser() { UserName = "User1", Email = "user1@gmail.com" };
                    userManager.CreateAsync(appUser, "Password12*").Wait();
                    userManager.CreateAsync(new AppUser() { UserName = "User2", Email = "user2@gmail.com" }, "Password12*").Wait();
                    userManager.CreateAsync(new AppUser() { UserName = "User3", Email = "user3@gmail.com" }, "Password12*").Wait();
                    userManager.CreateAsync(new AppUser() { UserName = "User4", Email = "user4@gmail.com" }, "Password12*").Wait();
                    userManager.CreateAsync(new AppUser() { UserName = "User5", Email = "user5@gmail.com" }, "Password12*").Wait();

                    var upperCategory1 = new Category() { Name = "Suç Romanları", ReferenceId = 0, UserId = appUser.Id };
                    var upperCategory2 = new Category() { Name = "Cinayet Romanları", ReferenceId = 0, UserId = appUser.Id };
                    var upperCategory3 = new Category() { Name = "Polisiye Romanları", ReferenceId = 0, UserId = appUser.Id };

                    identityContext.Categories.AddRange(upperCategory1,upperCategory2,upperCategory3);
                    identityContext.SaveChanges();

                    var subCategory1 = new Category() { Name = "Klasik Suç Romanları", ReferenceId = upperCategory1.Id, UserId = appUser.Id };
                    var subCategory2 = new Category() { Name = "Modern Cinayet Romanları", ReferenceId = upperCategory2.Id, UserId = appUser.Id };
                    var subCategory3 = new Category() { Name = "Dünya Polisiye Romanları", ReferenceId = upperCategory3.Id, UserId = appUser.Id };

                    identityContext.Categories.AddRange(subCategory1, subCategory2, subCategory3);
                    identityContext.SaveChanges();

                    var subSubberCategory1 = new Category() { Name = "Sherlock Holmes", ReferenceId = subCategory3.Id, UserId = appUser.Id };
                    var subSubberCategory2 = new Category() { Name = "Agatha Christie", ReferenceId = subCategory2.Id, UserId = appUser.Id };
                    var subSubberCategory3 = new Category() { Name = "Matt Brolly", ReferenceId = subCategory1.Id, UserId = appUser.Id };

                    identityContext.Categories.AddRange(subSubberCategory1, subSubberCategory2, subSubberCategory3);
                    identityContext.SaveChanges();
                }
                
            }
        }
    }
}
