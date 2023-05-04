using GateNewsApi.BLL.Interfaces;
using GateNewsApi.Dtos.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GateNewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {

        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return Ok(author);
        }

        [HttpGet("name/{fullName}")]
        public async Task<IActionResult> GetByFullNameAsync(string fullName)
        {
            var author = await _authorService.GetByFullNameAsync(fullName);
            return Ok(author);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _authorService.DeleteAuthorAsync(Guid.Parse(userId));

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(AuthorUpdateRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _authorService.UpdateAuthorAsync(Guid.Parse(userId), request);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

       

    }

}
