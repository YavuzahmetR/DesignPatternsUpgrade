using IdentityBase.Models;

namespace WebApp.Observer.Observer
{
    public interface IUserObserver
    {
        Task Update(AppUser user);
    }
}
