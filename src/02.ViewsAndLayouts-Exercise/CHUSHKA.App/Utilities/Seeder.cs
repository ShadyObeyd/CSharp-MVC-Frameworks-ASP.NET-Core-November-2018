using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CHUSHKA.App.Utilities
{
    public static class Seeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var adminRoleExists = roleManager.RoleExistsAsync("Admin").Result;

            if (!adminRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var userRoleExists = roleManager.RoleExistsAsync("User").Result;

            if (!userRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole("User"));
            }
        }
    }
}