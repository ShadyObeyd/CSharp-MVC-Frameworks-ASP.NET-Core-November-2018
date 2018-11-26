using Eventures.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Eventures.App.Middlewares
{
    public class SeedMiddleware
    {
        private readonly RequestDelegate next;

        public SeedMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IServiceProvider serviceProvider, SignInManager<EventuresUser> signInManager)
        {
            // Seed Roles
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var adminRoleExists = roleManager.RoleExistsAsync("Admin").Result;

            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var userRoleExists = roleManager.RoleExistsAsync("User").Result;

            if (!userRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Seed User admin
            if (!signInManager.UserManager.Users.Any())
            {
                var user = new EventuresUser
                {
                    UserName = "Pesho",
                    Email = "pesho@pesho.bg",
                    FirstName = "Peter",
                    LastName = "Petrov",
                    UCN = "9812116358"
                };

                var result = signInManager.UserManager.CreateAsync(user, "123456").Result;

                var roleResult = signInManager.UserManager.AddToRoleAsync(user, "Admin").Result;
            }

            await this.next(httpContext);
        }
    }
}