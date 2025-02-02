using IdentityBase.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Observer.Event;
using WebApp.Observer.Models;
using WebApp.Observer.Observer;

namespace WebApp.Observer.EventHandlers
{
    public class CreateDiscountEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreateDiscountEventHandler> _logger;
        private readonly IdentityContext _identityContext;
        public CreateDiscountEventHandler(IdentityContext _identityContext, ILogger<CreateDiscountEventHandler> _logger)
        {
            this._identityContext = _identityContext;
            this._logger = _logger;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _identityContext.AddAsync(new Discount { UserId = notification.AppUser.Id, Rate = 10 });
            await _identityContext.SaveChangesAsync();
            _logger.LogInformation($"CreateDiscountEventHandler Created : {notification.AppUser.UserName} - {10}");
        }
    }
}
