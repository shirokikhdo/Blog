using Api.Data;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Контроллер для управления новостями.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;
        private readonly UserService _userService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NewsController"/>.
        /// </summary>
        /// <param name="newsService">Сервис для работы с новостями.</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public NewsController(
            NewsService newsService,
            UserService userService)
        {
            _newsService = newsService;
            _userService = userService;
        }

        /// <summary>
        /// Получает список новостей по идентификатору автора.
        /// </summary>
        /// <param name="userId">Идентификатор автора новостей.</param>
        /// <returns>Список новостей, созданных указанным автором.</returns>
        [HttpGet("{userId}")]
        public ActionResult<List<NewsView>> GetByAuthor(int userId)
        {
            var news = _newsService.GetByAuthor(userId);
            return Ok(news);
        }

        /// <summary>
        /// Получает все новости для текущего пользователя.
        /// </summary>
        /// <returns>Список новостей для текущего пользователя.</returns>
        [HttpGet]
        public ActionResult<List<NewsView>> GetAll()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            
            if (currentUser is null)
                return NotFound();

            var news = _newsService.GetNewsForCurrentUser(currentUser.Id);
            return Ok(news);
        }

        /// <summary>
        /// Создает новую новость.
        /// </summary>
        /// <param name="news">Модель новости, содержащая информацию о новой новости.</param>
        /// <returns>Созданная модель новости.</returns>
        [HttpPost]
        public ActionResult<NewsModel> Create(
            [FromBody] NewsModel news)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            var createdNews = _newsService.Create(news, currentUser.Id);
            return Ok(createdNews);
        }

        /// <summary>
        /// Создает несколько новостей для администратора.
        /// </summary>
        /// <param name="news">Список моделей новостей, которые нужно создать.</param>
        /// <returns>Список созданных моделей новостей.</returns>
        [HttpPost("random")]
        public ActionResult<List<NewsModel>> Create(
            [FromBody] List<NewsModel> news)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            if (currentUser.Id != 1)
                return BadRequest();

            var allNews = news.Select(newsItem => 
                _newsService.Create(newsItem, 2))
                .ToList();
            return Ok(allNews);
        }

        /// <summary>
        /// Обновляет существующую новость.
        /// </summary>
        /// <param name="news">Модель новости с обновленной информацией.</param>
        /// <returns>Обновленная модель новости.</returns>
        [HttpPatch]
        public ActionResult<NewsView> Update(
            [FromBody] NewsModel news)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            var createdNews = _newsService.Update(news, currentUser.Id);
            return Ok(createdNews);
        }

        /// <summary>
        /// Удаляет новость по идентификатору.
        /// </summary>
        /// <param name="newsId">Идентификатор новости, которую нужно удалить.</param>
        /// <returns>Статус выполнения операции.</returns>
        [HttpDelete("{newsId}")]
        public IActionResult Delete(int newsId)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            _newsService.Delete(newsId, currentUser.Id);
            return Ok();
        }

        /// <summary>
        /// Устанавливает лайк на новость.
        /// </summary>
        /// <param name="newsId">Идентификатор новости, на которую ставится лайк.</param>
        /// <returns>Статус выполнения операции.</returns>
        [HttpPost("like/{newsId}")]
        public IActionResult SetLike(int newsId)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            _newsService.SetLike(newsId, currentUser.Id);
            return Ok();
        }
    }
}