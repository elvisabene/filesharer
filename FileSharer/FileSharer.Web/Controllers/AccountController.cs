using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Web.Helpers.LoggingHelpers;
using FileSharer.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FileSharer.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        private readonly ILogger<AccountController> _logger;

        private readonly ILogHelper _logHelper;

        public AccountController(
            IAccountService userManager,
            ILogger<AccountController> logger,
            ILogHelper logHelper)
        {
            _accountService = userManager;
            _logger = logger;
            _logHelper = logHelper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "File", nameof(Login)));

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

            if (!_accountService.IsUserExists(model.Email, model.Password, out user))
            {
                ModelState.AddModelError("", ErrorMessages.IncorrectCredentials);
                return View();
            }

            await _accountService.Authenticate(user, HttpContext);

            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "Account", nameof(Login), "POST"));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "Account", nameof(Register)));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_accountService.IsUserExists(model.Email))
            {
                ModelState.AddModelError("", ErrorMessages.UserAlreadyExists);

                return View();
            }

            var user = _accountService.CreateUser(model.Name, model.Email, model.Password);

            await _accountService.Authenticate(user, HttpContext);

            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "Account", nameof(Register), "POST"));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "Account", nameof(Logout)));

            return RedirectToAction("Index", "Home");
        }
    }
}
