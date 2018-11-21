using System.ComponentModel.DataAnnotations;

namespace Eventures.App.ViewModels.Users
{
    public class RegisterViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string UCN { get; set; }
    }
}
