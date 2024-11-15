namespace account_service.AccountActions
{
    public interface IUserService
    {
        public Task<bool> IsUserExists(string Email);
    }
}