using Microsoft.AspNetCore.Mvc;

namespace GateNewsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController : ControllerBase
{

    public NewsController()
    {
       
    }

    public IEnumerable<> GetAllNewsAsync()
    {
      return null
    }
}
