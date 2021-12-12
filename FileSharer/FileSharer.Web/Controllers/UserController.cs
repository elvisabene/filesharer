using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Web.Helpers.LoggingHelpers;
using FileSharer.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FileSharer.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IRoleService _roleService;

        private readonly ILogger<HomeController> _logger;

        private readonly ILogHelper _logHelper;

        public UserController(
            IUserService userService,
            IRoleService roleService,
            ILogger<HomeController> logger,
            ILogHelper logHelper)
        {
            _userService = userService;
            _roleService = roleService;
            _logger = logger;
            _logHelper = logHelper;
        }

        [HttpGet]
        public IActionResult List()
        {
            var users = _userService.GetAll();

            var userModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userModel = new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = _roleService.GetById(user.RoleId).Name,
                };

                userModels.Add(userModel);
            }

            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "User", nameof(List)));

            return View(userModels);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            var role = _roleService.GetById(user.RoleId);

            var editModel = new EditUserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = role.Name,
            };

            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "User", nameof(Edit)));

            return View(editModel);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return Edit(editModel.Id);
            }

            var userByEmail = _userService.GetByEmail(editModel.Email);
            if (userByEmail != null && userByEmail.Id != editModel.Id)
            {
                ModelState.AddModelError("", ErrorMessages.UserAlreadyExists);
            }

            var role = _roleService.GetByName(editModel.Role);

            var user = new User()
            {
                Name = editModel.Name,
                Email = editModel.Email,
                RoleId = role.Id,
            };

            _userService.Update(editModel.Id, user);

            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "User", nameof(Edit), "POST"));

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);

            _logger.LogInformation(
               _logHelper.GetUserActionString(User, "User", nameof(Delete), "POST"));

            return RedirectToAction(nameof(List));
        }
    }
}
