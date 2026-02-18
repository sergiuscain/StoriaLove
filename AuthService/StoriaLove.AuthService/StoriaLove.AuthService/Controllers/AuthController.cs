using Auth.BuisnessLogic;
using Microsoft.AspNetCore.Mvc;
using StoriaLove.AuthService.Models;

namespace StoriaLove.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AccountService accountService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody]RegisterUserRequest request)
        {
            accountService.Register(request.UserName, request.FirstName, request.Password);
        }
    }
}
