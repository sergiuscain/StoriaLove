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
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                //JTC - name of cookies for JWT TOKEN (JWT Token Cookie - JTC)
                var token = accountService.Login(request.UserName, request.Password);
                HttpContext.Response.Cookies.Append("JTC", token);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            // Удаляем куку с токеном
            HttpContext.Response.Cookies.Append("JTC", "", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1), // Setting the date in the past
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { message = "Logged out successfully" });
        }
    }
}
