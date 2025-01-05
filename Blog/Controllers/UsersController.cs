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
        public ActionResult<List<UserModel>> GetUsersByName(string name)
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
        public IActionResult Get(int userId)
        {
            var user = _userService.GetUserProfileById(userId);
            return Ok(user);
        }
    }
}