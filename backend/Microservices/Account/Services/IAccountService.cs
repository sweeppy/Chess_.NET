namespace Account.Services
{
    public interface IAccountService
    {
        public Task<bool> IsUserExists(string Email);
    }
}