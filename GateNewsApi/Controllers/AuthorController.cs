using GateNewsApi.DAL;
using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Domain;
using GateNewsApi.Dtos.Authors;
using GateNewsApi.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GateNewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {

        public AuthorController()
        {
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return null;
        }

        [HttpGet("name/{fullName}")]
        public async Task<IActionResult> GetByFullNameAsync(string fullName)
        {
            return null;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserCreateRequest request)
        {
            return null;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            
            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, AuthorUpdateRequest request)
        {
            return null;
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(PasswordChangeRequest request)
        {
            return null;
        }
    }

}
