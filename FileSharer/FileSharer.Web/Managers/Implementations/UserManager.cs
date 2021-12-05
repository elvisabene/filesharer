using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Common.Extensions;
using FileSharer.Web.Managers.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileSharer.Web.Managers.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public bool IsUserExists(string email)
        {
            var user = _userService.GetByEmail(email);

            return user != null;
        }

        public bool IsUserExists(string email, string password, out User user)
        {
            user = null;

            if (!IsUserExists(email))
            {
                return false;
            }

            user = _userService.GetByEmail(email);
            var passwordHash = password.GetHashSHA256();

            if (user.PasswordHash != passwordHash)
            {
                user = null;

                return false;
            }

            return true;
        }

        public async Task Authenticate(User user, HttpContext httpContext)
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

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public User CreateUser(string name, string email, string password)
        {
            var passwordHash = password.GetHashSHA256();

            var newUser = new User()
            {
                Name = name,
                Email = email,
                RoleId = Default.RoleId,
                PasswordHash = passwordHash,
            };

            return newUser;
        }
    }
}
