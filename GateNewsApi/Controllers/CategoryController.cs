using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GateNewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
   

        public CategoryController()
        {
      
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return null;
        }
    }
}
