using IdentityBase.Models;
using MediatR;

namespace WebApp.Observer.Event
{
    public sealed class UserCreatedEvent : INotification
    {
        public AppUser AppUser { get; set; }
    }
}
