using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoriaLove.ProfileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("One")]
        public string GetOne()
        {
            return "Test1";
        }
        [HttpGet("Two")]
        [Authorize]
        public string GetTwo()
        {
            return "Test2";
        }
        [HttpGet("Three")]
        [Authorize]
        public string GetThree()
        {
            return "Test3";
        }
    }
}
