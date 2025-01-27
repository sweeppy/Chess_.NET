using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.JWT
{
    public interface IJwtService
    {
        public string GenerateToken(string Email);
    }
}