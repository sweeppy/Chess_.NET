
using account_service.Contracts;
using account_service.Data;

namespace account_service.AccountServices
{
    public class VerifyService : IVerifyService
    {
        private readonly UserDbContext _db;

        public VerifyService(UserDbContext db)
        {
            _db = db;
        }


        public async Task<BaseResponse> LoginByPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
        // Send by Twilio
        public async Task<BaseResponse> SendEmailConfirmation(string email)
        {
            var accountSid = "";
            throw new   NotImplementedException();
        }

        public async Task<BaseResponse> VerifyConfirmationCode(string email, string verificationCode)
        {
            throw new NotImplementedException();
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(0, 999999).ToString("D6");
        }
    }
}