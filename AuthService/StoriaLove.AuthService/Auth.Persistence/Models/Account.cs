
namespace Auth.Persistence.Models;
public class Account
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string PasswordHash { get; set; }
    public List<string> Roles { get; set; } = new List<string> { RolesEnum.User.ToString() }; // By default, User
}
