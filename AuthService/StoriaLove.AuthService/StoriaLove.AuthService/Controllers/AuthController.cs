using Auth.BuisnessLogic;
using Microsoft.AspNetCore.Mvc;
using StoriaLove.AuthService.Models;

namespace StoriaLove.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AccountService accountService) : ControllerBase
    {
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterUserRequest request)
        {
            accountService.Register(request.UserName, request.FirstName, request.Password);
            return NoContent();
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = accountService.Login(request.UserName, request.Password);
            return Ok(token);
        }
    }
}
