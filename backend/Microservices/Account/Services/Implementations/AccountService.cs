using Account.Data;
using Account.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Account.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserDbContext _db;

        public AccountService(UserDbContext db)
        {
            _db = db;
        }

        public string GenerateVerificationCode()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        public async Task<bool> IsUserExistsAndEmailConfirmed(string Email)
        {
            return await _db.Players
            .FirstOrDefaultAsync(p => p.Email == Email && p.IsEmailConfirmed == true) != null;
        }
    }
}