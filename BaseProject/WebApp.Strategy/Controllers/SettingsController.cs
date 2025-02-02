using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager)
        {
            this._userManager = _userManager;
            this._signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            Settings settings = new Settings();
            var databaseClaim = User.Claims.FirstOrDefault(x => x.Type == Settings.ClaimDataBaseType);

            if (databaseClaim != null)
            {
                settings.DatabaseType = (EDatabaseType)int.Parse(databaseClaim.Value);
            }
            else
            {
                settings.DatabaseType = settings.GetDatabaseType;
            }

            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.GetUserAsync(User);
            var newClaim = new Claim(Settings.ClaimDataBaseType, databaseType.ToString());
            var claims = await _userManager.GetClaimsAsync(user!);
            var hasDatabaseTypeClaim = claims.FirstOrDefault(x => x.Type == Settings.ClaimDataBaseType);
            if(hasDatabaseTypeClaim != null)
            {
                await _userManager.ReplaceClaimAsync(user!, hasDatabaseTypeClaim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user!, newClaim);
            }
            await _signInManager.SignOutAsync();
            var authenticationResult = await HttpContext.AuthenticateAsync();
            await _signInManager.SignInAsync(user!, authenticationResult.Properties!);

            return RedirectToAction(nameof(SettingsController.Index));
        }
    }
}

