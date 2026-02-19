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
            try
            {
                accountService.Register(request.UserName, request.FirstName, request.Password);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = accountService.Login(request.UserName, request.Password);
            return Ok(token);
        }
    }
}
