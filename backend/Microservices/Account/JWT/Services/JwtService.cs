
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Account.DTO.Requests.JwtRequests;
using Microsoft.IdentityModel.Tokens;

namespace Account.JWT.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(GenerateTokenRequest request)
        {
            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:SignKey").Value));
            var credentials = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString()),
                new Claim(ClaimTypes.Name, request.Username)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToByte(_configuration["JwtSettings:ExpireHours"])),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}