
namespace Auth.Persistence.Models;
public class Account
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string PasswordHash { get; set; }
    // В БУДУЩЕМ НАДО ПЕРЕПИСАТЬ ЭТУ ЧАСТЬ ТАК, ЧТО БЫ БЫЛА ОТДЕЛЬНАЯ ТАБЛИЦА С РОЛЯМИ И РАЗРЕШЕНИЯМИ ДЛЯ ЭТОЙ РОЛИ.
    // Связь Roles и Permissions многие ко многим. И так же, связь Account и Roles многие ко многим. И контроллер для выдачи ролей
    public List<string> Roles { get; set; } = new List<string> { RolesEnum.User.ToString() }; // By default, User
}
