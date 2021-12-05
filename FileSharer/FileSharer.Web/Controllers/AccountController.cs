using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Web.Managers.Interfaces;
using FileSharer.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FileSharer.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
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

            User user;

            if (!_userManager.IsUserExists(model.Email, model.Password, out user))
            {
                ModelState.AddModelError("", ErrorMessage.IncorrectCredentials);
                return View();
            }

            await _userManager.Authenticate(user, HttpContext);

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

            if (_userManager.IsUserExists(model.Email))
            {
                ModelState.AddModelError("", ErrorMessage.UserAlreadyExists);

                return View();
            }

            var user = _userManager.CreateUser(model.Name, model.Email, model.Password);

            await _userManager.Authenticate(user, HttpContext);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
