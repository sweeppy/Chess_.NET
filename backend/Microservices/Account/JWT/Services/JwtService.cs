
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Account.Data;
using Account.DTO.JwtRequests;
using Account.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Tnef;

namespace Account.JWT.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _db;

        public JwtService(IConfiguration configuration, UserDbContext db)
        {
            _db = db;
            _configuration = configuration;
        }

        public string GenerateAccessToken(GenerateAccessTokenRequest request)
        {
            var secretKey = _configuration["JWT_SECRET"];
            if (secretKey == null)
            {
                throw new InvalidOperationException("Set 'JWT_SECRET' in dotnet secrets.");
            }
            
            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            
            var credentials = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            [
                new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.Username)
            ];

            var token = new JwtSecurityToken(
                issuer: "AccountService",
                audience: "ChessService",
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToByte(_configuration["EXPIRE_HOURS"])),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task SaveRefreshTokenAsync(string userId, string token)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                Expires = DateTime.Now.AddDays(14).ToUniversalTime()
            };

            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            return await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token && rt.IsActive);
        }

        public async Task RevokeRefreshToken(string token)
        {
            var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken != null)
            {
                _db.RefreshTokens.Remove(refreshToken);
                await _db.SaveChangesAsync();
            }
        }
    }
}