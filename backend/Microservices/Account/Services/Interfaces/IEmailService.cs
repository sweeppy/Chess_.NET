using Account.DTO.VerificationCodeRequests;

namespace Account.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(SendEmailRequest request);
    }
}