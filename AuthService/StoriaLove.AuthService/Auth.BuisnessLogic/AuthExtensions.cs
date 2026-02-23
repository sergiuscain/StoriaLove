
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.BuisnessLogic;
public static class AuthExtensions
{
    public static IServiceCollection AddAuth(
        this IServiceCollection servicesCollection,
        IConfiguration configuration)
    {
        var authSettings = configuration.GetSection(nameof(AuthSettings))
            .Get<AuthSettings>();
        servicesCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey))
                };
            });
        servicesCollection.AddAuthorization();
        return servicesCollection;
    }
}
