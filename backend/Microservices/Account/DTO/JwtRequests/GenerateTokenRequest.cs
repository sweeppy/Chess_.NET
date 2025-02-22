namespace Account.DTO.JwtRequests
{
    public record GenerateTokenRequest(int UserId, string Username, string Email);
}