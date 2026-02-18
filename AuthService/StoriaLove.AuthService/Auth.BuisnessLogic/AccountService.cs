using Auth.Persistence;

namespace Auth.BuisnessLogic;

public class AccountService(AccountRepository accountRepository)
{
    public void Register(string userName, string firstName, string password)
    {
        accountRepository.Add();
    }
    public void Login(string userName, string firstName, string password)
    {
        // TODO
    }
}