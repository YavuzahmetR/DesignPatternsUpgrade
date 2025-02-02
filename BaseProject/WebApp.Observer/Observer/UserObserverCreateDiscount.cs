using IdentityBase.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider serviceProvider;
        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task Update(AppUser user)
        {
            var logging = serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                await context.AddAsync(new Discount { UserId = user.Id, Rate = 10 });
                await context.SaveChangesAsync();
            }
            logging.LogInformation($"UserObserverCreateDiscount Created : {user.UserName} - {10}");
        }
    }
}
