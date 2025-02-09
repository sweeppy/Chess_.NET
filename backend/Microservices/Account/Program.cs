using Account.Data;
using Microsoft.OpenApi.Models;
using Account.JWT.Services;
using Account.JWT.Configuration;
using Account.Services.Interfaces;
using Account.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add database
builder.Services.AddScoped<UserDbContext>();

// Add jwt configuration
builder.Services.AddJwtAuthentication(builder.Configuration);

// REGISTER DEPENDENCIES:
{
    // Jwt service
    builder.Services.AddScoped<IJwtService, JwtService>();

    // For actions with authentication
    builder.Services.AddScoped<IAccountService, AccountService>();

    // Email Service
    builder.Services.AddScoped<IEmailService, EmailService>();

    // Encryption service
    builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
}

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