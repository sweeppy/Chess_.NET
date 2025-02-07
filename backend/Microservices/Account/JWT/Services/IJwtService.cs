using Account.DTO.Requests.JwtRequests;

namespace Account.JWT.Services
{
    public interface IJwtService
    {
        public string GenerateToken(GenerateTokenRequest request);
    }
}