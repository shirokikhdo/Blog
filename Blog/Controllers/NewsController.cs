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
        private readonly UserService _userService;

        public NewsController(
            NewsService newsService,
            UserService userService)
        {
            _newsService = newsService;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public ActionResult<List<News>> GetByAuthor(int userId)
        {
            var news = _newsService.GetByAuthor(userId);
            return Ok(news);
        }

        [HttpGet]
        public ActionResult<List<NewsModel>> GetAll()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            var news = _newsService.GetNewsForCurrentUser(currentUser.Id);
            return Ok(news);
        }

        [HttpPost]
        public ActionResult<NewsModel> Create(
            [FromBody] NewsModel news)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            var createdNews = _newsService.Create(news, currentUser.Id);
            return Ok(createdNews);
        }

        [HttpPatch]
        public ActionResult<NewsModel> Update(
            [FromBody] NewsModel news)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            var createdNews = _newsService.Update(news, currentUser.Id);
            return Ok(createdNews);
        }

        [HttpDelete("{newsId}")]
        public IActionResult Delete(int newsId)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            _newsService.Delete(newsId, currentUser.Id);
            return Ok();
        }

        [HttpPost("like/{newsId}")]
        public IActionResult SetLike(int newsId)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            _newsService.SetLike(newsId, currentUser.Id);
            return Ok();
        }
    }
}