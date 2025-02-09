using System.Security.Claims;
using Account.Data;
using Account.Models;
using Account.Requests.AccountRequests;
using Account.Requests.Email;
using Account.Responses;
using Account.Responses.Auth;
using Account.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


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

        [HttpPost("is_user_exists")]
        public async Task<IActionResult> Login([FromBody] string email)
        {
            try
            {
                return Ok(await _userService.IsUserExists(email));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(IsUserExists). Exception: {ex.Message}");
                return BadRequest("Something went wrong, while we were checking your email");
            }
        }

        [HttpPost("add_new_user")]
        public async Task<IActionResult> AddUserAndConfirmHisEmail(string email)
        {
            try
            {
                // Add new player to database
                Player player = new Player { Email = email };
                _db.Players.Add(player);

                // Generate code
                string code = _userService.GenerateVerificationCode();
                string encryptedCode = _encryption.Encrypt(code);

                // Save new player with encrypted code in DB
                player.VerifyCode = encryptedCode;
                _db.Players.Add(player);

                // Send verification code to user's email
                SendEmailRequest request = new(email, "Confirm your email", code);
                await _emailService.SendEmailAsync(request);

                // After success sent email
                await _db.SaveChangesAsync();

                return Ok(new RegisterUserResponse(true, "Code was successfully sent", true));
            }
            catch (ArgumentException ArgEx) // Exception with encrypting code
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(send_verification_code). {ArgEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(send_verification_code). Exception: {ex.Message}");
            }
            return BadRequest(new BaseResponse(false, "Something went wrong, while we were trying to send you email."));
        }

        [HttpPost("verify_code")]
        public IActionResult VerifyCode(VerifyCodeRequest request)
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

        [HttpGet("getUserData")]
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