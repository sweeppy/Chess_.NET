namespace Account.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> IsUserExists(string Email);
    }
}