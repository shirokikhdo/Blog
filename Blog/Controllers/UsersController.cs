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
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all/{name}")]
        public ActionResult<List<UserShortModel>> GetUsersByName(string name)
        {
            var users = _userService.GetUsersByName(name);
            return Ok(users);
        }

        [HttpPost("subscribe/{userId}")]
        public IActionResult Subscribe(int userId)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser.Id == userId)
                return BadRequest();

            _userService.Subscribe(currentUser.Id, userId);
            return Ok();
        }

        [HttpGet("{userId}")]
        public ActionResult<UserProfile> Get(int userId)
        {
            var user = _userService.GetUserProfileById(userId);
            return Ok(user);
        }

        [HttpPost("random")]
        public ActionResult<List<UserModel>> CreateUsers(
            [FromBody] List<UserModel> users)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser.Id != 1)
                return BadRequest();

            var allUsers = users.Select(user =>
                _userService.Create(user))
                .ToList();
            return Ok(allUsers);
        }
    }
}