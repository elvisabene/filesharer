﻿using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Common.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileSharer.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;

        private readonly IRoleService _roleService;

        public AccountService(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
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

            var role = _roleService.GetById(user.RoleId);

            var claims = new List<Claim>()
            {
                new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, role.Name),
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
                RoleId = Defaults.RoleId,
                PasswordHash = passwordHash,
            };

            _userService.Add(newUser);

            return newUser;
        }
    }
}
