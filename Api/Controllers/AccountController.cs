using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<UserModel> Get()
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
        [AllowAnonymous]
        public ActionResult<UserModel> Create(
            [FromBody] UserModel user)
        {
            var createdUser = _userService.Create(user);
            return Ok(createdUser);
        }

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

        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.GetUserByLogin(currentUserEmail);
            _userService.Delete(currentUser);
            return Ok();
        }

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