using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Observer.Event;
using WebApp.Observer.Observer;

namespace WebApp.Observer.EventHandlers
{
    public class CreatedUserWriteConsoleEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreatedUserWriteConsoleEventHandler> _logger;
        public CreatedUserWriteConsoleEventHandler(ILogger<CreatedUserWriteConsoleEventHandler> _logger)
        {
            this._logger = _logger;
        }
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {       
            _logger.LogInformation($"UserObserverConsoleData Written: {notification.AppUser.Email}");
            return Task.CompletedTask; ;
        }
    }
}
