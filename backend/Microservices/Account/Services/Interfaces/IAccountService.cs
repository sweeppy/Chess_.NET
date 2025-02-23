

using System.Security.Claims;
using Account.DTO.AccountRequests;
using Account.Responses.Auth;

namespace Account.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IsUserExistsAndEmailConfirmedResponse> IsUserExistsAndEmailConfirmed(string Email);

        public string GenerateVerificationCode();

        public Task CreateAccount(CreateAccountRequest request, ClaimsPrincipal user);
    }
}