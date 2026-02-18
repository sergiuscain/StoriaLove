using Auth.BuisnessLogic;
using Auth.Persistence;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StoriaLove API",
        Version = "v1",
        Description = "API для приложения знакомств"
    });
});
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<JWTService>();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // отдает JSON по /openapi/v1.json
    app.MapOpenApi();

    //Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "StoriaLove API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();