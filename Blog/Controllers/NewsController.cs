using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        [HttpGet("{userId}")]
        public IActionResult GetByAuthor(int userId)
        {
            return Ok(userId);
        }
    }
}