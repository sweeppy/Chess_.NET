using Account.Services;
using Account.Data;
using Account.JWT;
using Account.Config.JWT;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Dependencies
builder.Services.AddScoped<UserDbContext>();

// Add jwt configuration
CustomJwtConfiguration.AddCustomJwtAuthentication(builder.Services, builder.Configuration);
builder.Services.AddScoped<IJwtService, JwtService>();
// For actions with authentication
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Account_Chess API", Version = "v1.0.0" });
});

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options.WithOrigins("http://localhost:5173")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account_Chess API v1.0.0");
    });
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.MapControllers();
app.Run();