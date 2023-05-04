using GateNewsApi.Dtos.Users;
using Microsoft.AspNetCore.Identity;

namespace GateNewsApi.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateUserAsync(UserCreateRequest request);
        Task<string> LoginAsync(string usernameOrEmail, string password);
        Task<bool> ChangePasswordAsync(string userId, PasswordChangeRequest request);
    }
}
