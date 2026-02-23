
using Auth.Persistence.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.BuisnessLogic;
public class JwtService(IOptions<AuthSettings> options)
{
    public string GenerateToken(Account account)
    {
        var claims = new List<Claim>
        {
            new Claim("userName", account.UserName),
            new Claim("firstName", account.FirstName),
            new Claim("id", account.Id.ToString()),
        };
        //Add role to claims
        foreach (var role in account.Roles)
            claims.Add(new Claim(role, role.ToString()));

        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)), 
                SecurityAlgorithms.HmacSha256)
            );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
