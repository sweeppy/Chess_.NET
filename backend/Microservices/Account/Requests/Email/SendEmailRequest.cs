namespace Account.Requests.Email
{
    public record SendEmailRequest(string toEmail, string subject, string body);
}