namespace Account.DTO.AccountRequests
{
    public record VerifyCodeRequest(string email, string code);
}