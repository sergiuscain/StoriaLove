using Microsoft.EntityFrameworkCore;
using StoriaLove.ProfileService.Models;

namespace StoriaLove.ProfileService.DB;
public class ProfileDbContext : DbContext
{
    public DbSet<Profile> Profiles { get; set; }
    public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}