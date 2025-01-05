using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{name}")]
        public ActionResult<List<UserModel>> GetUsersByName(string name)
        {
            var users = _userService.GetUsersByName(name);
            return Ok(users);
        }

        [HttpPost("subscribe/{userId}")]
        public IActionResult GetUsersByName(int userId)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser.Id == userId)
                return BadRequest();

            _userService.Subscribe(currentUser.Id, userId);
            return Ok();
        }
    }
}