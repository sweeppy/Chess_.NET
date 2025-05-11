using Account.DTO.JwtRequests;
using Account.Models;

namespace Account.JWT.Services
{
    public interface IJwtService
    {
        public string GenerateAccessToken(GenerateAccessTokenRequest request);

        public string GenerateRefreshToken();
        public Task SaveRefreshTokenAsync(string userId, string token);
        public Task<RefreshToken?> GetRefreshToken(string token);
        public Task RevokeRefreshToken(string token);
    }
}