namespace Account.DTO.EmailRequests
{
    public record SendEmailRequest(string toEmail, string subject, string body);
}