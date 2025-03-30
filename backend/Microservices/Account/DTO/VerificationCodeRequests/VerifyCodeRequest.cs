namespace Account.DTO.VerificationCodeRequests
{
    public record VerifyCodeRequest(string Email, string Code);
}