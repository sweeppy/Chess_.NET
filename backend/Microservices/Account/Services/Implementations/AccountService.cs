using Account.Data;
using Account.DTO.Requests.JwtRequests;
using Account.JWT.Services;
using Account.Models;
using Account.Responses.Auth;
using Account.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Account.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IJwtService _jwtService;
        private readonly UserDbContext _db;

        public AccountService(UserDbContext db, IJwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        public string GenerateVerificationCode()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        public async Task<IsUserExistsAndEmailConfirmedResponse> IsUserExistsAndEmailConfirmed(string Email)
        {
            Player? player = await _db.Players
            .FirstOrDefaultAsync(p => p.Email == Email);

            // If player exists
            if (player != null)
            {
                GenerateTokenRequest request = player.Username == null
                ? new GenerateTokenRequest(player.Id, "", player.Email)
                : new GenerateTokenRequest(player.Id, player.Username, player.Email);

                string token = _jwtService.GenerateToken(request);
                bool isConfirmed = player.IsEmailConfirmed;

                string successMessage = isConfirmed ? "User exists and email confirmed"
                : "User exists, but email isn't confirmed";

                return new IsUserExistsAndEmailConfirmedResponse(
                    isSuccess: true,
                    message: successMessage,
                    isExists: true,
                    isEmailConfirmed: isConfirmed,
                    jwtToken: isConfirmed ? token : null
                );
            }
            // Player doesn't exists in DB
            return new IsUserExistsAndEmailConfirmedResponse(
                    isSuccess: true,
                    message: "User doesn't exists in DB",
                    isExists: false,
                    isEmailConfirmed: false,
                    jwtToken: null
            );
        }
    }
}