using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using account_service.Contracts;
using account_service.Models;

namespace account_service.AccountServices
{
    public interface IVerifyService
    {
        public Task<BaseResponse> LoginByPassword(string email, string password);
        public Task<BaseResponse> SendEmailConfirmation(string email);
        public Task<BaseResponse> VerifyConfirmationCode(string email, string verificationCode);
    }
}