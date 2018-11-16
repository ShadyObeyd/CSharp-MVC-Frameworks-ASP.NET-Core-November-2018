namespace CHUSHKA.App.Controllers
{
    using ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using CHUSKA.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    public class UsersController : BaseController
    {
        private readonly SignInManager<ChushkaUser> signIn;

        public UsersController(SignInManager<ChushkaUser> signIn)
        {
            this.signIn = signIn;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            if (this.Db.Users.Any(u => u.UserName == model.Username))
            {
                return this.View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.View();
            }

            var user = new ChushkaUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = this.signIn.UserManager.CreateAsync(user, model.Password).Result;

            if (this.signIn.UserManager.Users.Count() == 1)
            {
                var roleResult = this.signIn.UserManager.AddToRoleAsync(user, "Admin").Result;

                if (roleResult.Errors.Any())
                {
                    return this.View();
                }
            }
            else if(this.signIn.UserManager.Users.Count() > 1)
            {
                var roleResult = this.signIn.UserManager.AddToRoleAsync(user, "User").Result;

                if (roleResult.Errors.Any())
                {
                    return this.View();
                }
            }

            if (result.Succeeded)
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = this.signIn.UserManager.Users.FirstOrDefault(u => u.UserName == model.Username);

            if (user == null)
            {
                return this.View();
            }

            this.signIn.SignInAsync(user, true).Wait();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.signIn.SignOutAsync().Wait();

            return this.RedirectToAction("Index", "Home");
        }
    }
}