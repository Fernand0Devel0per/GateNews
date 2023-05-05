using GateNewsApi.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GateNewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retrieve all categories.
        /// </summary>
        /// <returns>A list of category objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
    }
}
