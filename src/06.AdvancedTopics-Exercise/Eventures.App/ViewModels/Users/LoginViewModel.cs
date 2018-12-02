using Eventures.App.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Eventures.App.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = Constants.UsernameLengthErrorMessage)]
        [RegularExpression(@"^[A-Za-z0-9-_\.*`]+$", ErrorMessage = Constants.UsernameRegexErrorMessage)]
        public string Username { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = Constants.PasswordLengthErrorMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
