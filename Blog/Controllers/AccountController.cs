using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult<UserModel> Create(UserModel user)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult Update(UserModel user)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
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