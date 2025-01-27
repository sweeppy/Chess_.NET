using Account.Responses;

namespace Account.Services
{
    public interface IVerifyService
    {
        public Task<BaseResponse> LoginByPassword(string email, string password);
        public Task<BaseResponse> SendEmailConfirmation(string email);
        public Task<BaseResponse> VerifyConfirmationCode(string email, string verificationCode);
    }
}