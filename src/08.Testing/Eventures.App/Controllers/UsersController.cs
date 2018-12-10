using Eventures.App.ViewModels.Users;
using Eventures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Eventures.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = this.userService.GetUserByUsername(model.Username);

            if (user == null)
            {
                return this.View();
            }

            if (!this.userService.PasswordIsValid(user, model.Password))
            {
                return this.View();
            }

            this.userService.SignInUser(user);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (this.userService.ModelIsInvalid(model.Username, model.UCN))
            {
                return this.View();
            }

            this.userService.CreateUser(model.Username, model.Password, model.FirstName, model.LastName, model.Email, model.UCN);

            return this.RedirectToAction("Login", "Users");
        }

        public IActionResult Logout()
        {
            this.userService.SignOutUser();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var dbUsers = this.userService.GetAll(this.User.Identity.Name);

            List<UserViewModel> admins = new List<UserViewModel>();
            List<UserViewModel> users = new List<UserViewModel>();

            foreach (var user in dbUsers)
            {
                var userModel = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName
                };

                if (this.userService.UserIsAdmin(user))
                {
                    userModel.Role = "Admin";
                    admins.Add(userModel);
                }
                else
                {
                    userModel.Role = "User";
                    users.Add(userModel);
                }
            }

            var model = new AllUsersViewModel
            {
                Users = users,
                Admins = admins
            };

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Demote(string adminId)
        {
            var admin = this.userService.GetUserById(adminId);

            this.userService.DemoteAdmin(admin);

            return this.RedirectToAction("All", "Users");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Promote(string userId)
        {
            var user = this.userService.GetUserById(userId);

            this.userService.PromoteUser(user);

            return this.RedirectToAction("All", "Users");
        }
    }
}