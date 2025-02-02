using Microsoft.AspNetCore.Identity;

namespace IdentityBase.Models
{
    public sealed class AppUser : IdentityUser<string>
    {
        public AppUser()
        {
            Id = Guid.NewGuid().ToString().Substring(0,6);
        }
    }
}
