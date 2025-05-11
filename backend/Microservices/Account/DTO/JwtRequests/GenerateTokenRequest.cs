namespace Account.DTO.JwtRequests
{
    public record GenerateAccessTokenRequest(int UserId, string Username, string Email);
}