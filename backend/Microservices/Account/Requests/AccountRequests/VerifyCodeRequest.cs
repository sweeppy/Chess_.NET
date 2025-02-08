namespace Account.Requests.AccountRequests
{
    public record VerifyCodeRequest(string email, string code);
}