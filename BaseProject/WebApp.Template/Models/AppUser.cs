using Microsoft.AspNetCore.Identity;

namespace WebApp.Template.Models
{
    public sealed class AppUser : IdentityUser<string>
    {
        public AppUser()
        {
            Id = Guid.NewGuid().ToString().Substring(0,6);
        }
        public string  PictureUrl { get; set; }
        public string  Description { get; set; }

 
    }
}
