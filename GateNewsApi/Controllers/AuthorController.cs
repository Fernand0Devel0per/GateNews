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

        /// <summary>
        /// Retrieve an author by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>An author object if found; otherwise, a NotFound response.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return Ok(author);
        }

        /// <summary>
        /// Retrieve an author by their full name.
        /// </summary>
        /// <param name="fullName">The full name of the author.</param>
        /// <returns>An author object if found; otherwise, a NotFound response.</returns>
        [HttpGet("name/{fullName}")]
        public async Task<IActionResult> GetByFullNameAsync(string fullName)
        {
            var author = await _authorService.GetByFullNameAsync(fullName);
            return Ok(author);
        }

        /// <summary>
        /// Delete the authenticated author.
        /// </summary>
        /// <returns>An Ok response if the author is deleted; otherwise, a NotFound response.</returns>
        [Authorize]
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

        /// <summary>
        /// Update the authenticated author's information.
        /// </summary>
        /// <param name="request">The updated author information.</param>
        /// <returns>An Ok response if the author is updated; otherwise, a NotFound response.</returns>
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
