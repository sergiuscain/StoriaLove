using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoriaLove.ProfileService.Models;

namespace StoriaLove.ProfileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("NotAuth")]
        public string GetNotAuth()
        {
            return "An unauthorized user's test";
        }
        [HttpGet("User")]
        [Authorize(nameof(RolePoliciesEnum.UserPolicy))]
        public string GetUser()
        {
            return "Authorized 'Admin' Test";
        }
        [HttpGet("Admin")]
        [Authorize(nameof(RolePoliciesEnum.AdminPolicy))]
        public string GetAdmin()
        {
            return "Authorized 'Admin' Test";
        }
    }
}
