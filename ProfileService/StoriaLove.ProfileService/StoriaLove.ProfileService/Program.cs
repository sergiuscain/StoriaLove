using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using StoriaLove.ProfileService.DB;
using StoriaLove.ProfileService.Models;
using StoriaLove.ProfileService.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProfileDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<ProfileRepository>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StoriaLove API",
        Version = "v1",
        Description = "API для приложения знакомств"
    });
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RolePoliciesEnum.UserPolicy.ToString(), policy =>
    {
        policy.RequireClaim(RolesEnum.User.ToString(), RolesEnum.User.ToString());
    });
    options.AddPolicy(RolePoliciesEnum.ModeratorPolicy.ToString(), policy =>
    {
        policy.RequireClaim(RolesEnum.Moderator.ToString(), RolesEnum.Moderator.ToString());
    });
    options.AddPolicy(RolePoliciesEnum.AdminPolicy.ToString(), policy =>
    {
        policy.RequireClaim(RolesEnum.Admin.ToString(), RolesEnum.Admin.ToString());
    });
});
var authSettings = builder.Configuration.GetSection(nameof(AuthSettings))
            .Get<AuthSettings>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["JTC"];
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "StoriaLove API V1");
    });
}

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
