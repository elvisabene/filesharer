using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using FileSharer.Common.Extensions;

namespace FileSharer.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetByEmail(model.Email);

            if (user is null)
            {
                ModelState.AddModelError("", "Incorrect email or password");
                return View();
            }

            var passwordHash = model.Password.GetHashSHA256();

            if (passwordHash != user.PasswordHash)
            {
                ModelState.AddModelError("", "Incorrect email or password");
                return View();
            }

            await Authenticate(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = _userService.GetByEmail(model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "User with such Email already exists!");

                return View();
            }

            var passwordHash = model.Password.GetHashSHA256();

            User user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                RoleId = 1,
                PasswordHash = passwordHash,
            };

            _userService.Add(user);

            await Authenticate(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var identity = new ClaimsIdentity(claims, "AuthenticationCookie");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
