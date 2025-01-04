using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
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
        public IActionResult GetToken()
        {
            throw new NotImplementedException();
        }
    }
}