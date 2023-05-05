using GateNewsApi.BLL.Interfaces;
using GateNewsApi.Dtos.News;
using GateNewsApi.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace GateNewsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet("title/{title}/page/{pageNumber}")]
    public async Task<IActionResult> GetByTitleAsync(string title, int pageNumber)
    {
        var (items, totalPages) = await _newsService.GetByTitleAsync(title, pageNumber);
        return Ok(new { Items = items, TotalPages = totalPages });
    }

    [HttpGet("author/{authorFullName}/page/{pageNumber}")]
    public async Task<IActionResult> GetByAuthorAsync(string authorFullName, int pageNumber)
    {
        var (items, totalPages) = await _newsService.GetByAuthorAsync(authorFullName, pageNumber);
        return Ok(new { Items = items, TotalPages = totalPages });
    }

    [HttpGet("category/{categoryCode}/page/{pageNumber}")]
    public async Task<IActionResult> GetByCategoryAsync(int categoryCode, int pageNumber)
    {
        try
        {
            var (items, totalPages) = await _newsService.GetByCategoryAsync(categoryCode, pageNumber);
            return Ok(new { Items = items, TotalPages = totalPages });
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("category/{categoryCode}/author/{authorFullName}/page/{pageNumber}")]
    public async Task<IActionResult> GetByCategoryAndAuthorAsync(int categoryCode, string authorFullName, int pageNumber)
    {
        try
        {
            var (items, totalPages) = await _newsService.GetByCategoryAndAuthorAsync(categoryCode, authorFullName, pageNumber);
            return Ok(new { Items = items, TotalPages = totalPages });
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("date-interval/{startDate}/{endDate}/page/{pageNumber}")]
    public async Task<IActionResult> GetByDateIntervalAsync(string startDate, string endDate, int pageNumber)
    {


        string dateFormat = "dd-MM-yyyy";

        if (!DateTime.TryParseExact(startDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartDate) ||
            !@DateTime.TryParseExact(endDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndDate))
        {
            return BadRequest($"Invalid date format. Please provide dates in the format '{dateFormat}'.");
        }

        var (items, totalPages) = await _newsService.GetByDateIntervalAsync(parsedStartDate, parsedEndDate, pageNumber);
        return Ok(new { Items = items, TotalPages = totalPages });
    }

    [HttpGet("date/page/{pageNumber}")]
    public async Task<IActionResult> GetByDateAsync(int pageNumber)
    {
        var (items, totalPages) = await _newsService.GetByDateAsync(pageNumber);
        return Ok(new { Items = items, TotalPages = totalPages });
    }

   
    // POST: api/News
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateNews(NewsCreateRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var createdNews = await _newsService.CreateNews(request, userId);

            var createdUrl = Url.Action(nameof(GetByTitleAsync), new { title = createdNews.Title, pageNumber = 1 });
            return Created(createdUrl, createdNews);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (AuthorNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred. Please try again later." });
        }
    }

    // PUT: api/News/{id}
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNews(Guid id, NewsUpdateRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _newsService.UpdateNews(id, request, userId);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred. Please try again later." });
        }
    }

    // DELETE: api/News/{id}
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNews(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _newsService.DeleteNews(id, userId);

        if (result)
        {
            return Ok();
        }

        return NotFound();
    }

}
