namespace Account.DTO.VerificationCodeRequests
{
    public record SendEmailRequest(string ToEmail, string Subject, string Body);
}