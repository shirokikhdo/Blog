using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UsersController"/>.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получает список пользователей по имени.
        /// </summary>
        /// <param name="name">Имя, по которому будет осуществляться поиск пользователей.</param>
        /// <returns>Список пользователей, соответствующих заданному имени.</returns>
        [HttpGet("all/{name}")]
        public ActionResult<List<UserShortModel>> GetUsersByName(string name)
        {
            var users = _userService.GetUsersByName(name);
            return Ok(users);
        }

        /// <summary>
        /// Подписывает текущего пользователя на другого пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, на которого подписывается текущий пользователь.</param>
        /// <returns>Статус выполнения операции.</returns>
        [HttpPost("subscribe/{userId}")]
        public IActionResult Subscribe(int userId)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            if (currentUser.Id == userId)
                return BadRequest();

            _userService.Subscribe(currentUser.Id, userId);
            return Ok();
        }

        /// <summary>
        /// Получает профиль пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, профиль которого нужно получить.</param>
        /// <returns>Профиль запрашиваемого пользователя.</returns>
        [HttpGet("{userId}")]
        public ActionResult<UserProfile> Get(int userId)
        {
            var user = _userService.GetUserProfileById(userId);
            return Ok(user);
        }

        /// <summary>
        /// Создает нескольких пользователей. Доступно только для администратора.
        /// </summary>
        /// <param name="users">Список моделей пользователей, которые нужно создать.</param>
        /// <returns>Список созданных моделей пользователей.</returns>
        [HttpPost("random")]
        public ActionResult<List<UserModel>> CreateUsers(
            [FromBody] List<UserModel> users)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            if (currentUser.Id != 1)
                return BadRequest();

            var allUsers = users.Select(user =>
                _userService.Create(user))
                .ToList();
            return Ok(allUsers);
        }
    }
}