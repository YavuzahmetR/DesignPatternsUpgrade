using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.ChainOfResponsibility.Models;

namespace IdentityBase.Models
{
    public sealed class IdentityContext : IdentityDbContext<AppUser, IdentityRole,string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
