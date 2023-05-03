using GateNewsApi.DAL;
using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Domain;
using GateNewsApi.Dtos.News;
using Microsoft.AspNetCore.Mvc;

namespace GateNewsApi.Controllers;

[Route("api/[controller]")]
[Route("[controller]")]
public class NewsController : ControllerBase
{

    public NewsController()
    {
       
    }

    // GET: api/News/title/{title}/page/{pageNumber}
    [HttpGet("title/{title}/page/{pageNumber}")]
    public async Task<IActionResult> GetByTitleAsync(string title, int pageNumber)
    {
        return null;
    }

    // GET: api/News/author/{authorFullName}/page/{pageNumber}
    [HttpGet("author/{authorFullName}/page/{pageNumber}")]
    public async Task<IActionResult> GetByAuthorAsync(string authorFullName, int pageNumber)
    {
        return null;
    }

    // GET: api/News/category/{categoryId}/page/{pageNumber}
    [HttpGet("category/{categoryId}/page/{pageNumber}")]
    public async Task<IActionResult> GetByCategoryAsync(int categoryCode, int pageNumber)
    {
        return null;
    }

    // GET: api/News/category/{categoryId}/author/{authorFullName}/page/{pageNumber}
    [HttpGet("category/{categoryId}/author/{authorFullName}/page/{pageNumber}")]
    public async Task<IActionResult> GetByCategoryAndAuthorAsync(int categoryCode, string authorFullName, int pageNumber)
    {
        return null;
    }

    // GET: api/News/date-interval/{startDate}/{endDate}/page/{pageNumber}
    [HttpGet("date-interval/{startDate}/{endDate}/page/{pageNumber}")]
    public async Task<IActionResult> GetByDateIntervalAsync(DateTime startDate, DateTime endDate, int pageNumber)
    {
        return null;
    }

    // GET: api/News/date/page/{pageNumber}
    [HttpGet("date/page/{pageNumber}")]
    public async Task<IActionResult> GetByDateAsync(int pageNumber)
    {
        return null;
    }

    // POST: api/News/words/page/{pageNumber}
    [HttpPost("words/page/{pageNumber}")]
    public async Task<IActionResult> GetByWordsAsync([FromBody] WordListRequest wordListRequest, int pageNumber)
    {
        return null;
    }

    // POST: api/News
    [HttpPost]
    public async Task<IActionResult> CreateNews(NewsCreateRequest request)
    {
        return null;
    }

    // PUT: api/News/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNews(Guid id, NewsUpdateRequest request)
    {
        return null;
    }

    // DELETE: api/News/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNews(Guid id)
    {
        return null;
    }
}
