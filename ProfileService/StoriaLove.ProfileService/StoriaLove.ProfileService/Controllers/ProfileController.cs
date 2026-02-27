using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoriaLove.ProfileService.Models;
using StoriaLove.ProfileService.Services;

namespace StoriaLove.ProfileService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly ProfileRepository _repository;
    public ProfileController(ProfileRepository repository)
    {
        _repository = repository;
    }

    [HttpPut("InitProfile")]
    public async Task<IActionResult> InitProfile(Profile profile)
    {
        var result = await _repository.InitProfileAsync(profile);
        if (result is true)
            return Ok(result);
        else
            return BadRequest(result);
    }
}