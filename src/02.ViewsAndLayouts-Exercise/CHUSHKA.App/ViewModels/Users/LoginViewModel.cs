﻿using System.ComponentModel.DataAnnotations;

namespace CHUSHKA.App.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
