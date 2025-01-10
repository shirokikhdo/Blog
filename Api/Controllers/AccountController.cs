using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    /// <summary>
    /// Контроллер для управления учетными записями пользователей.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AccountController"/>.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получает профиль текущего пользователя.
        /// </summary>
        /// <returns>Профиль пользователя.</returns>
        [HttpGet]
        public ActionResult<UserProfile> Get()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            var profile = _userService.ToProfile(currentUser);
            return Ok(profile);
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="user">Модель пользователя, содержащая информацию о новом пользователе.</param>
        /// <returns>Созданная модель пользователя.</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UserModel> Create(
            [FromBody] UserModel user)
        {
            var createdUser = _userService.Create(user);
            return Ok(createdUser);
        }

        /// <summary>
        /// Обновляет информацию о текущем пользователе.
        /// </summary>
        /// <param name="user">Модель пользователя с обновленной информацией.</param>
        /// <returns>Обновленная модель пользователя.</returns>
        [HttpPatch]
        public ActionResult<UserModel> Update(UserModel user)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            if (currentUser != null 
                && currentUser.Id != user.Id)
                return BadRequest();
            
            var updatedUser = _userService.Update(currentUser, user);
            return Ok(updatedUser);
        }

        /// <summary>
        /// Удаляет текущего пользователя.
        /// </summary>
        /// <returns>Статус выполнения операции.</returns>
        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            _userService.Delete(currentUser);
            return Ok();
        }

        /// <summary>
        /// Получает токен аутентификации для пользователя.
        /// </summary>
        /// <returns>Токен аутентификации.</returns>
        [HttpPost("token")]
        [AllowAnonymous]
        public ActionResult<AuthToken> GetToken()
        {
            var userData = _userService.GetUserLoginPassFromBasicAuth(Request);
            (ClaimsIdentity claims, int id)? identity = _userService.GetIdentity(userData.login, userData.password);
            
            if (identity is null)
                return NotFound("Login or password is incorrect");

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity?.claims.Claims,
                expires: now.AddMinutes(AuthOptions.LIFETIME),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), 
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler()
                .WriteToken(jwt);
            var tokenModel = new AuthToken(
                AuthOptions.LIFETIME, 
                encodedJwt, 
                userData.login, 
                identity.Value.id);

            return Ok(tokenModel);
        }
    }
}