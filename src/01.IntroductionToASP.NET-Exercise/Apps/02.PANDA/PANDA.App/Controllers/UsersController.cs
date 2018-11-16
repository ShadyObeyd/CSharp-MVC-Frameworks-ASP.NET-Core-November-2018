using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PANDA.App.ViewModels.Users;
using PANDA.Models;
using PANDA.Models.Enums;
using PANDA.Services.Contracts;

namespace PANDA.App.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IHashService hashService;

        public UsersController(IHashService hashService)
        {
            this.hashService = hashService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            if (this.Db.Users.Any(u => u.Username == model.Username))
            {
                return this.BadRequest("User with this password already exists!");
            }

            var role = Role.User;

            if (!this.Db.Users.Any())
            {
                role = Role.Admin;
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.hashService.Hash(model.Password),
                Email = model.Email,
                Role = role
            };

            this.Db.Users.Add(user);
            this.Db.SaveChanges();

            return this.Redirect("/Users/Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var hashedPassword = this.hashService.Hash(model.Password);

            var user = this.Db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);

            this.

            return this.Redirect("/");
        }
    }
}