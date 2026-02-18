
using Auth.Persistence.Models;

namespace Auth.Persistence;

public class AccountRepository
{
    public void Add(Account account)
    {
        // Add account to any DataBase
    }

    public Account GetByUserName(string userName)
    {
        // Returns a certain account by user name
        return new Account(); 
    }
}
