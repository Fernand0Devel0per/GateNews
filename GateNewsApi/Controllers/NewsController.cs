using GateNewsApi.BLL.Interfaces;
using GateNewsApi.Dtos.News;
using GateNewsApi.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Globalization;
using System.Security.Claims;
using static Dapper.SqlMapper;

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

    /// <summary>
    /// Retrieve news by its title with pagination.
    /// </summary>
    /// <param name="title">The title of the news.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <returns>A list of news with the specified title and the total number of pages.</returns>
    [HttpGet("title/{title}/page/{pageNumber}")]
    public async Task<IActionResult> GetByTitleAsync(string title, int pageNumber)
    {
        var (items, totalPages) = await _newsService.GetByTitleAsync(title, pageNumber);
        return Ok(new { Items = items, TotalPages = totalPages });
    }

    /// <summary>
    /// Retrieve news by its author's full name with pagination.
    /// </summary>
    /// <param name="authorFullName">The full name of the author.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <returns>A list of news written by the specified author and the total number of pages.</returns>
    [HttpGet("author/{authorFullName}/page/{pageNumber}")]
    public async Task<IActionResult> GetByAuthorAsync(string authorFullName, int pageNumber)
    {
        var (items, totalPages) = await _newsService.GetByAuthorAsync(authorFullName, pageNumber);
        return Ok(new { Items = items, TotalPages = totalPages });
    }

    /// <summary>
    /// Retrieve news by its category code with pagination.
    /// </summary>
    /// <param name="categoryCode">The category code of the news.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <returns>A list of news in the specified category and the total number of pages.</returns>
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

    /// <summary>
    /// Retrieve news by its category code and author's full name with pagination.
    /// </summary>
    /// <param name="categoryCode">The category code of the news.</param>
    /// <param name="authorFullName">The full name of the author.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <returns>A list of news in the specified category and written by the specified author, and the total number of pages.</returns>
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

    /// <summary>
    /// Retrieve news by date interval with pagination.
    /// </summary>
    /// <param name="startDate">The start date of the date interval (format: dd-MM-yyyy).</param>
    /// <param name="endDate">The end date of the date interval (format: dd-MM-yyyy).</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <returns>A list of news within the specified date interval and the total number of pages.</returns>
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

    /// <summary>
    /// Retrieve news by date with pagination.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <returns>A list of news ordered by date and the total number of pages.</returns>
    [HttpGet("date/page/{pageNumber}")]
    public async Task<IActionResult> GetByDateAsync(int pageNumber)
    {
        var (items, totalPages) = await _newsService.GetByDateAsync(pageNumber);
        return Ok(new { Items = items, TotalPages = totalPages });
    }


    /// <summary>
    /// Create a news.
    /// </summary>
    /// <param name="request">The request object containing the news data.</param>
    /// <returns>The created news and its URL.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateNews(NewsCreateRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var createdNews = await _newsService.CreateNews(request, userId);

           
            return StatusCode(StatusCodes.Status201Created, createdNews);
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

    /// <summary>
    /// Update a news.
    /// </summary>
    /// <param name="id">The ID of the news to update.</param>
    /// <param name="request">The request object containing the updated news data.</param>
    /// <returns>A status code indicating the result of the update operation.</returns>
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

    /// <summary>
    /// Delete a news article.
    /// </summary>
    /// <param name="id">The ID of the news to delete.</param>
    /// <returns>A status code indicating the result of the delete operation.</returns>
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
