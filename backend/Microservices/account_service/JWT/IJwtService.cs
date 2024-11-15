using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace account_service.JWT
{
    public interface IJwtService
    {
        public string GenerateToken(string Email);
    }
}