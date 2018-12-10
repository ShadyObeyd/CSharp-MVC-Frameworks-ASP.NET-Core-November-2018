using Eventures.App.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Eventures.App.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = Constants.UsernameLengthErrorMessage)]
        [RegularExpression(@"^[A-Za-z0-9-_\.*`]+$", ErrorMessage = Constants.UsernameRegexErrorMessage)]
        public string Username { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = Constants.PasswordLengthErrorMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = Constants.UcnErrorMessage)]
        public string UCN { get; set; }
    }
}
