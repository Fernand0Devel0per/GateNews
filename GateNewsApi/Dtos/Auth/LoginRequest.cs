using System.ComponentModel.DataAnnotations;

namespace GateNewsApi.Dtos.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username or email is required.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
