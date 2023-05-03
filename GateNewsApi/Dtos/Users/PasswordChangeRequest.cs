namespace GateNewsApi.Dtos.Users
{
    public class PasswordChangeRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
