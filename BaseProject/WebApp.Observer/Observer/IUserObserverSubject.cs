using IdentityBase.Models;

namespace WebApp.Observer.Observer
{
    public interface IUserObserverSubject
    {
        void RegisterObserver(IUserObserver observer);
        void RemoveObserver(IUserObserver observer);
        void NotifyObservers(AppUser user);
    }
}