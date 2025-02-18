namespace Account.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> IsUserExistsAndEmailConfirmed(string Email);

        public string GenerateVerificationCode();
    }
}