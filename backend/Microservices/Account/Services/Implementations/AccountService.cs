using System.Security.Claims;
using Account.Data;
using Account.DTO.AccountRequests;
using Account.DTO.JwtRequests;
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

        public async Task CreateAccount(CreateAccountRequest request, ClaimsPrincipal user)
        {
            var (IsValid, Message) = PasswordValidator.ValidatePassword(request.Password, request.ConfirmPassword);
            if (!IsValid)
            {
                throw new ArgumentException(Message);
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("Email not found in token.");
            }

            var player = await _db.Players.FirstOrDefaultAsync(p => p.Email == email);
            if (player == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            if (request.ProfileImage != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique file name
                var fileName = $"{userId}_{Guid.NewGuid()}.jpg";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfileImage.CopyToAsync(stream);
                }

                // Hash password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);

                // Save player information to DB
                player.ImagePath = $"/images/{fileName}";
                player.Username = request.Username;
                player.Password = hashedPassword;

                _db.Players.Update(player);
                await _db.SaveChangesAsync();
            }
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
                bool isAccountCreated = player.Username != null;

                bool isConfirmed = player.IsEmailConfirmed;

                string successMessage = isConfirmed ? "User exists and email confirmed"
                : "User exists, but email isn't confirmed";

                return new IsUserExistsAndEmailConfirmedResponse(
                    isSuccess: true,
                    message: successMessage,
                    isExists: true,
                    isEmailConfirmed: isConfirmed,
                    isAccountCreated: !(player.Username == null)
                );
            }
            // Player doesn't exists in DB
            return new IsUserExistsAndEmailConfirmedResponse(
                    isSuccess: true,
                    message: "User doesn't exists in DB",
                    isExists: false,
                    isEmailConfirmed: false,
                    isAccountCreated: false
            );
        }

        public async Task<LoginResponse> LoginByPassword(LoginByPasswordRequest request)
        {
            var isPasswordValidResponse = PasswordValidator.ValidatePassword(request.Password);
            if (!isPasswordValidResponse.IsValid)
            {
                throw new ArgumentException(isPasswordValidResponse.Message);
            }

            Player? player = await _db.Players.FirstOrDefaultAsync(p => p.Email == request.Email);
            if (player == null || player.Password == null || player.Username == null)
            {
                throw new KeyNotFoundException("Account with this email was not found");
            }
            string hashedPassword = player.Password;

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, hashedPassword);

            if (!isPasswordCorrect)
                throw new ArgumentException("Wrong password.");

            string jwtToken = _jwtService.GenerateAccessToken(new GenerateAccessTokenRequest
            (UserId: player.Id, Username: player.Username, Email: player.Email));

            return new LoginResponse(isSuccess: true,
                                     message: "Successfully login",
                                     jwtToken: jwtToken,
                                     userId: player.Id);
        }
    }
}