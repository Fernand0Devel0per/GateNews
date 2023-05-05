using GateNewsApi.Dtos.Authors;
using System.ComponentModel.DataAnnotations;

namespace GateNewsApi.Dtos.Users
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("Password", ErrorMessage = "Password and password confirmation do not match.")]
        public string ConfirmPassword { get; set; }
        public AuthorCreateRequest Author { get; set; }
    }
}
