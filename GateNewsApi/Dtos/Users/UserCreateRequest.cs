using GateNewsApi.Dtos.Authors;

namespace GateNewsApi.Dtos.Users
{
    public class UserCreateRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public AuthorCreateRequest Author { get; set; }
    }
}
