using GateNewsApi.BLL.Interfaces;
using GateNewsApi.Dtos.Auth;
using GateNewsApi.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GateNewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="request">The user registration information.</param>
        /// <returns>A status indicating the result of the registration process.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserCreateRequest request)
        {
            var result = await _authService.CreateUserAsync(request);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Log in with an existing user.
        /// </summary>
        /// <param name="loginRequest">The user's login credentials.</param>
        /// <returns>A JWT token if the login is successful.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var token = await _authService.LoginAsync(loginRequest.UsernameOrEmail, loginRequest.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username/email or password.");
            }
        }

        /// <summary>
        /// Change the password of the authenticated user.
        /// </summary>
        /// <param name="request">The password change information.</param>
        /// <returns>A status indicating the result of the password change process.</returns>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(PasswordChangeRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _authService.ChangePasswordAsync(userId, request);

            if (result)
            {
                return Ok();
            }

            return BadRequest("Failed to change password");
        }
    }
}
