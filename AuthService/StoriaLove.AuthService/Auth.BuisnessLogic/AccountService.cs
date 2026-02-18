using Auth.Persistence;
using Auth.Persistence.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.BuisnessLogic;

public class AccountService(AccountRepository accountRepository)
{
    public void Register(string userName, string firstName, string password)
    {
        var account = new Account()
        {
            UserName = userName,
            FirstName = firstName,
            Id = Guid.NewGuid(),
        };
        var passHash = new PasswordHasher<Account>().HashPassword(account, password);
        account.PasswordHash = passHash;
        accountRepository.Add(account);
    }
    public void Login(string userName, string password)
    {
        var account = accountRepository.GetByUserName(userName);
        var result = new PasswordHasher<Account>().
            VerifyHashedPassword(account, account.PasswordHash, password);
        if (result == PasswordVerificationResult.Success)
        {
            // Generate Token

        }
        else
        {
            throw new Exception("Unauthorized");
        }
    }
}