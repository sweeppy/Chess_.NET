using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace auth_service.Configuration.JWT
{
    public static class CustomJwtConfiguration
    {
        public static void AddCustomJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var SignKey = Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SignKey").Value);
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SignKey),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true
                };
            });
        }
    }
}