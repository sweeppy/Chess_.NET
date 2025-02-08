using Account.Requests.Email;

namespace Account.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(SendEmailRequest request);
    }
}