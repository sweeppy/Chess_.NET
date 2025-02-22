using Account.DTO.EmailRequests;

namespace Account.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(SendEmailRequest request);
    }
}