using IdentityBase.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverConsoleData : IUserObserver
    {
        private readonly IServiceProvider serviceprovider;
        public UserObserverConsoleData(IServiceProvider serviceprovider)
        {
            this.serviceprovider = serviceprovider;
        }
        public Task Update(AppUser user)
        {
            var logging = serviceprovider.GetRequiredService<ILogger<UserObserverConsoleData>>();
            logging.LogInformation($"UserObserverConsoleData Written: {user.Email}");
            return Task.CompletedTask;
        }
    }
}
