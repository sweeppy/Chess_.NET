using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.DTO.Requests.JwtRequests
{
    public record GenerateTokenRequest(int UserId, string Username);
}