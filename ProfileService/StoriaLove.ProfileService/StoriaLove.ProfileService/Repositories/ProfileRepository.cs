using StoriaLove.ProfileService.DB;
using StoriaLove.ProfileService.Models;

namespace StoriaLove.ProfileService.Services;
public class ProfileRepository
{
    private readonly ProfileDbContext _context;
    public ProfileRepository(ProfileDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Init profil.
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    public async Task<bool> InitProfileAsync(Profile profile)
    {
        try
        {
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return true;
        }
        catch 
        {
            return false;
        }
    }
}