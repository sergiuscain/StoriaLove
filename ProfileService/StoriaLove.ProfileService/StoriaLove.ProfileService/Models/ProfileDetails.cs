using StoriaLove.ProfileService.Models.Enums;

namespace StoriaLove.ProfileService.Models;
public class ProfileDetails
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Gender? Gender { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public string? HairColor { get; set; }

    public Profile Profile { get; set; }

}
