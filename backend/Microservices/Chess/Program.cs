using Chess.API.Implementations;
using Chess.API.Interfaces;
using Chess.Data;
using Chess.JWT;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Chess_bots API", Version = "v1.0" });
});

DotNetEnv.Env.Load();

// Add environment variables
builder.Configuration.AddEnvironmentVariables();

// Add jwt authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Register dependencies
builder.Services.AddScoped<GamesDbContext>();

builder.Services.AddScoped<IMovement, Movement>();

// Add cors
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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Chess_bots API v1.0.0");
    });
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();