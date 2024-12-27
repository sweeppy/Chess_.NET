namespace account_service.AccountActions
{
    public interface IAccountService
    {
        public Task<bool> IsUserExists(string Email);
    }
}