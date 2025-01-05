using Blog.Data;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{userId}")]
        public ActionResult<List<News>> GetByAuthor(int userId)
        {
            var news = _newsService.GetByAuthor(userId);
            return Ok(news);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] NewsModel news)
        {

        }
    }
}