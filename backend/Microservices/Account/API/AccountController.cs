using System.Security.Claims;
using Account.Data;
using Account.Models;
using Account.Requests.AccountRequests;
using Account.Requests.Email;
using Account.Responses;
using Account.Responses.Auth;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Account.API_controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        private readonly IEncryptionService _encryption;

        private readonly UserDbContext _db;

        public AccountController(IAccountService userService, ILogger<AccountController> logger,
                                 IEmailService emailService, UserDbContext db, IEncryptionService encryption)
        {
            _userService = userService;
            _logger = logger;
            _emailService = emailService;
            _db = db;
            _encryption = encryption;
        }

        [HttpPost("IsUserExistsAndEmailConfirmed")]
        public async Task<IActionResult> IsUserExistsAndEmailConfirmed([FromBody] string email)
        {
            try
            {
                return Ok(await _userService.IsUserExistsAndEmailConfirmed(email));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(IsUserExists). Exception: {ex.Message}");
                return BadRequest("Something went wrong, while we were checking your email");
            }
        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody]string email)
        {
            try
            {
                // Generate code
                string code = _userService.GenerateVerificationCode();
                string encryptedCode = _encryption.Encrypt(code);

                // Add new player to database
                Player player = new()
                {
                    Email = email,
                    VerifyCode = encryptedCode
                };

                // Save new player with encrypted code in DB
                _db.Players.Add(player);

                await _db.SaveChangesAsync();

                // Send verification code to user's email
                SendEmailRequest request = new(email, "Confirm your email", code);
                await _emailService.SendEmailAsync(request);

                return Ok(new RegisterUserResponse(true, "Code was successfully sent", true));
            }
            catch (ArgumentException ArgEx) // Exception with encrypting code
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(send_verification_code). {ArgEx.Message}");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogCritical($"DB error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(send_verification_code). Exception: {ex.Message}");
            }
            return BadRequest(new BaseResponse(false, "Something went wrong, while we were trying to send you email."));
        }

        [HttpPut("VerifyCode")]
        public async Task<IActionResult> VerifyCode(VerifyCodeRequest request)
        {
            try
            {
                Player? player = _db.Players.FirstOrDefault(p => p.Email == request.email);
                if (player == null)
                {
                    throw new ArgumentException("Player with this email was not found.");
                }
                // Get verification code from DB
                string decryptedCode = _encryption.Decrypt(player.VerifyCode);

                // If user's input code is wrong
                if (decryptedCode != request.code)
                    return BadRequest(new BaseResponse(false, "The code is wrong."));

                player.IsEmailConfirmed = true;
                _db.Players.Update(player);
                await _db.SaveChangesAsync();
                return Ok(new BaseResponse(true, "Email is confirmed"));
            }
            catch (ArgumentException ex)
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(send_verification_code). Exception: {ex.Message}");
                return BadRequest(new BaseResponse(false, "Something went wrong, while we were trying confirm email."));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(send_verification_code). Exception: {ex.Message}");
                return BadRequest(new BaseResponse(false, "Something went wrong, while we were trying confirm email."));
            }
        }

        [HttpGet("GetUserData")]
        public IActionResult GetData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userId != null && username != null)
            {
                return Ok(new { UserId = userId, Username = username });
            }
            return NotFound();
        }
    }
}