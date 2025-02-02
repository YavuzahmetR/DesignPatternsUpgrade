﻿using IdentityBase.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using WebApp.Observer.Event;
using WebApp.Observer.Models;
using WebApp.Observer.Observer;

namespace IdentityBase.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        //private readonly IUserObserverSubject _userObserverSubject;
        private readonly IMediator mediator;
        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, /*IUserObserverSubject _userObserverSubject,*/ IMediator mediator)
        {
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            //this._userObserverSubject = _userObserverSubject;
            this.mediator = mediator;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);
            if(hasUser == null)
            {
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(hasUser, password, true, false);
            if (!result.Succeeded)
            {
                return View();
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateViewModel model)
        {
            var newUser = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            
            if (result.Succeeded)
            {
                await mediator.Publish(new UserCreatedEvent() { AppUser = newUser });
                //_userObserverSubject.NotifyObservers(newUser);
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Error";
            }
            return View();
        }
    }
}
