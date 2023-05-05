using AutoMapper;
using GateNewsApi.BLL.Interfaces;
using GateNewsApi.DAL;
using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Domain;
using GateNewsApi.Dtos.Users;
using GateNewsApi.Helpers.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GateNewsApi.BLL
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, JwtSettings jwtSettings, IMapper mapper)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUserAsync(UserCreateRequest request)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var author = _mapper.Map<Author>(request.Author);
                author.UserId = user.Id;
                user.Author = author;

                await _userManager.UpdateAsync(user);
            }

            return result;
        }

        public async Task<string> LoginAsync(string usernameOrEmail, string password)
        {
            
            var user = await _userManager.FindByNameAsync(usernameOrEmail);

           
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            }

            if (user is null || !(await _userManager.CheckPasswordAsync(user, password)))
            {
                throw new UnauthorizedAccessException("Invalid username/email or password.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ChangePasswordAsync(string userId, PasswordChangeRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            return result.Succeeded;
        }

    }
}
