using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blog.Data;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BlogDbContext _dbContext;
        private readonly UserService _userService;

        public AccountController(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
            _userService = new UserService(_dbContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);

            if (currentUser is null)
                return NotFound();

            var user = new UserModel
            {
                Id = currentUser.Id,
                Name = currentUser.Name,
                Email = currentUser.Email,
                Description = currentUser.Description,
                Photo = currentUser.Photo,
            };
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserModel> Create(UserModel user)
        {
            var createdUser = _userService.Create(user);
            return Ok(createdUser);
        }

        [HttpPatch]
        public IActionResult Update(UserModel user)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            if (currentUser != null 
                && currentUser.Id != user.Id)
                return BadRequest();
            
            var updatedUser = _userService.Update(currentUser, user);
            return Ok(updatedUser);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            _userService.Delete(currentUser);
            return Ok();
        }

        [HttpPost]
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