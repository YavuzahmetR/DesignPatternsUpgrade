using IdentityBase.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverSubject : IUserObserverSubject
    {
        private readonly List<IUserObserver> _userObservers;
        public UserObserverSubject()
        {
            _userObservers = new List<IUserObserver>();
        }

        public void NotifyObservers(AppUser user)
        {
            _userObservers.ForEach(observer => observer.Update(user));
        }

        public void RegisterObserver(IUserObserver observer)
        {
            _userObservers.Add(observer);
        }

        public void RemoveObserver(IUserObserver observer)
        {
            _userObservers.Remove(observer);
        }
    }
}
