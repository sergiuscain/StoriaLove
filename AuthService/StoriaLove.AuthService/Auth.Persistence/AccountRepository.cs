
using Auth.Persistence.Models;

namespace Auth.Persistence;

public class AccountRepository
{
    // Временный список аккаунтов. Потом в базе данных всё будет хранится!!
    private List<Account> accounts = new List<Account>();
    public void Add(Account account)
    {
        // Add account to any DataBase
        accounts.Add(account);
    }

    public Account GetByUserName(string userName)
    {
        // Returns a certain account by user name
        return accounts.FirstOrDefault(a => a.UserName == userName);
    }
}
