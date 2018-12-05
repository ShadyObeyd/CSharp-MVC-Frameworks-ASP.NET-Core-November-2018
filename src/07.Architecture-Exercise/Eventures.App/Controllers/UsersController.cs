using Eventures.App.ViewModels.Users;
using Eventures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Eventures.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<EventuresUser> signInManager;

        public UsersController(SignInManager<EventuresUser> signInManager)
        {
            this.signInManager = signInManager;
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

            var user = this.signInManager.UserManager.Users.FirstOrDefault(u => u.UserName == model.Username);

            if (user == null)
            {
                return this.View();
            }

            if (!this.signInManager.UserManager.CheckPasswordAsync(user, model.Password).Result)
            {
                return this.View();
            }

            this.signInManager.SignInAsync(user, true).Wait();

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

            if (this.signInManager.UserManager.Users.Any(u => u.UserName == model.Username))
            {
                return this.View();
            }

            if (this.signInManager.UserManager.Users.Any(u => u.UCN == model.UCN))
            {
                return this.View();
            }

            var user = new EventuresUser
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UCN = model.UCN,
                Email = model.Email
            };

            var result = this.signInManager.UserManager.CreateAsync(user, model.Password).Result;

            if (!result.Succeeded)
            {
                return this.View();
            }

            var roleResult = this.signInManager.UserManager.AddToRoleAsync(user, "User").Result;

            if (!roleResult.Succeeded)
            {
                return this.View();
            }

            return this.RedirectToAction("Login", "Users");
        }

        public IActionResult Logout()
        {
            this.signInManager.SignOutAsync().Wait();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            List<UserViewModel> admins = new List<UserViewModel>();
            List<UserViewModel> users = new List<UserViewModel>();

            var dbUsers = this.signInManager.UserManager.Users.Where(u => u.UserName != this.User.Identity.Name);

            foreach (var user in dbUsers)
            {
                var userModel = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName
                };

                if (this.signInManager.UserManager.IsInRoleAsync(user, "Admin").Result)
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
            var admin = this.signInManager.UserManager.Users.FirstOrDefault(u => u.Id == adminId);

            this.signInManager.UserManager.RemoveFromRoleAsync(admin, "Admin").Wait();

            this.signInManager.UserManager.AddToRoleAsync(admin, "User").Wait();

            return this.RedirectToAction("All", "Users");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Promote(string userId)
        {
            var user = this.signInManager.UserManager.Users.FirstOrDefault(u => u.Id == userId);

            this.signInManager.UserManager.RemoveFromRoleAsync(user, "User").Wait();

            this.signInManager.UserManager.AddToRoleAsync(user, "Admin").Wait();

            return this.RedirectToAction("All", "Users");
        }
    }
}