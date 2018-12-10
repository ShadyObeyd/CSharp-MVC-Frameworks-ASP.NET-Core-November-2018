using Eventures.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Eventures.Services
{
    public class UserService
    {
        private readonly SignInManager<EventuresUser> signInManager;

        public UserService(SignInManager<EventuresUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public EventuresUser GetUserByUsername(string username)
        {
            return this.signInManager.UserManager.Users.FirstOrDefault(u => u.UserName == username);
        }

        public bool PasswordIsValid(EventuresUser user, string password)
        {
            return this.signInManager.UserManager.CheckPasswordAsync(user, password).Result;
        }

        public void SignInUser(EventuresUser user)
        {
            this.signInManager.SignInAsync(user, true).Wait();
        }

        public bool ModelIsInvalid(string username, string ucn)
        {
            bool userExists = this.signInManager.UserManager.Users.Any(u => u.UserName == username);

            bool ucnNotUnique = this.signInManager.UserManager.Users.Any(u => u.UCN == ucn);

            if (userExists || ucnNotUnique)
            {
                return true;
            }

            return false;
        }

        public void CreateUser(string username, string password, string firstName, string lastname, string email, string ucn)
        {
            var user = new EventuresUser
            {
                UserName = username,
                FirstName = firstName,
                LastName = lastname,
                UCN = ucn,
                Email = email
            };

            var result = this.signInManager.UserManager.CreateAsync(user, password).Result;

            var roleResult = this.signInManager.UserManager.AddToRoleAsync(user, "User").Result;
        }

        public void SignOutUser()
        {
            this.signInManager.SignOutAsync().Wait();
        }

        public IQueryable<EventuresUser> GetAll(string username)
        {
            return this.signInManager.UserManager.Users.Where(u => u.UserName != username);
        }

        public bool UserIsAdmin(EventuresUser user)
        {
            return this.signInManager.UserManager.IsInRoleAsync(user, "Admin").Result;
        }

        public EventuresUser GetUserById(string id)
        {
            return this.signInManager.UserManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public void DemoteAdmin(EventuresUser admin)
        {
            this.signInManager.UserManager.RemoveFromRoleAsync(admin, "Admin").Wait();

            this.signInManager.UserManager.AddToRoleAsync(admin, "User").Wait();
        }

        public void PromoteUser(EventuresUser user)
        {
            this.signInManager.UserManager.RemoveFromRoleAsync(user, "User").Wait();

            this.signInManager.UserManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}