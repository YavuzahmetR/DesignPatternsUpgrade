using IdentityBase.Models;
using WebApp.Observer.Email;

namespace WebApp.Observer.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IEmailService emailService;
        public UserObserverSendEmail(IServiceProvider serviceProvider, IEmailService emailService)
        {
            this.serviceProvider = serviceProvider;
            this.emailService = emailService;
        }
        public async Task Update(AppUser user)
        {
            var cancellationToken = new CancellationToken();    
            var logging = serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();
            if(user.Email == null)
            {
                logging.LogInformation($"UserObserverSendEmail Email is null: {user.UserName}");
                return;
            }
            await emailService.SendEmailAsync(user.Email, "Welcome", "Welcome to our platform",cancellationToken);
            logging.LogInformation($"UserObserverSendEmail Email Sent: {user.Email}");
        }
    }
}
