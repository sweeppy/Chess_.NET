using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using account_service.Data;
using Microsoft.EntityFrameworkCore;

namespace account_service.AccountActions
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _db;

        public UserService(UserDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsUserExists(string Email)
        {
            if (await _db.Players.FirstOrDefaultAsync(p => p.Email == Email) == null)
            {
                return false;
            }
            return true;
        }
    }
}