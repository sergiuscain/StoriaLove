using Auth.Persistence;
using Auth.Persistence.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.BuisnessLogic;

public class AccountService(AccountRepository accountRepository, JwtService jwtService)
{
    public void Register(string userName, string firstName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username is required");
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required");
        var existingAccount = accountRepository.GetByUserName(userName);
        if (existingAccount != null)
            throw new InvalidOperationException("User already exists");

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
    public string Login(string userName, string password)
    {
        var account = accountRepository.GetByUserName(userName);
        var result = new PasswordHasher<Account>().
            VerifyHashedPassword(account, account.PasswordHash, password);
        if (result == PasswordVerificationResult.Success)
        {
            return jwtService.GenerateToken(account);
        }
        else
        {
            throw new Exception("Unauthorized");
        }
    }
}