namespace Account.DTO.Requests.JwtRequests
{
    public record GenerateTokenRequest(int UserId, string Username, string Email);
}