using Microsoft.AspNetCore.Identity;

namespace WebApp.Strategy.Models
{
    public sealed class AppUser : IdentityUser<string>
    {
        public AppUser()
        {
            Id = Guid.NewGuid().ToString().Substring(0,6);
        }
    }
}
