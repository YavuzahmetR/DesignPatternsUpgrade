using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Observer.Email;
using WebApp.Observer.Event;
using WebApp.Observer.Observer;

namespace WebApp.Observer.EventHandlers
{
    public class SendEmailEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<SendEmailEventHandler> _logger;
        private readonly IEmailService emailService;

        public SendEmailEventHandler(ILogger<SendEmailEventHandler> logger, IEmailService emailService)
        {
            _logger = logger;
            this.emailService = emailService;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.AppUser.Email == null)
            {
                _logger.LogInformation($"UserObserverSendEmail Email is null: {notification.AppUser.UserName}");
                throw new NotImplementedException();
            }
            await emailService.SendEmailAsync(notification.AppUser.Email, "Welcome", "Welcome to our platform", cancellationToken);
            _logger.LogInformation($"UserObserverSendEmail Email Sent: {notification.AppUser.Email}");
        }
    }
}
