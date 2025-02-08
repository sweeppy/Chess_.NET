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

        private readonly UserDbContext _db;

        public AccountController(IAccountService userService, ILogger<AccountController> logger,
                                 IEmailService emailService)
        {
            _userService = userService;
            _logger = logger;
            _emailService = emailService;
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
                _logger.LogCritical($"Exception in AccountController HttpPost(IsUserExists). Exeption: {ex.Message}");
                return BadRequest("Something went wrong, while we were checking your email");
            }
        }

        [HttpPost("send_verification_code")]
        public async Task<IActionResult> GenerateAndSendVerificationCode(string email)
        {
            try
            {
                // Add new player to database
                Player player = new Player { Email = email };
                _db.Players.Add(player);
                // Send verification code to user's email
                SendEmailRequest request = new SendEmailRequest(email, "Confirm your email", 123456.ToString());
                await _emailService.SendEmailAsync(request);

                // After success sent email
                await _db.SaveChangesAsync();

                return Ok(new BaseResponse(true, "Code was successfully sent"));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in AccountController HttpPost(send_verification_code). Exception: {ex.Message}");
                return BadRequest(new BaseResponse(false, "Something went wrong, while we were sending email."));
            }
        }

        // [HttpPost("EmailLogin")]
        // public async Task<IActionResult> LoginByEmail([FromBody] string email)
        // {
        //     try
        //     {
        //         if (!result)
        //         {
        //             SendEmailRequest request = new SendEmailRequest(email, "PixelChess", "123456");
        //             await _emailService.SendEmailAsync(request);

        //             LoginResponse response = new LoginResponse(isSuccess: true, message: "Email was sent successfully", isEmailSent: true);
        //             return Ok(response);
        //         }
        //         else
        //         {
        //             return Ok(new BaseResponse);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogCritical($"Exception in AccountController HttpPost(IsUserExists). Exeption: {ex.Message}");
        //         return BadRequest("Something went wrong, while we were checking your email");
        //     }
        // }

        [HttpPost("verify_code")]
        public async Task<IActionResult> VerifyCode(VerifyCodeRequest request)
        {
            try
            {
                Player player = _db.Players.FirstOrDefault(p => p.Email == request.email);
                if (player == null)
                {
                    throw new NullReferenceException("Player with this email was not found.");
                }

// TODO make verify code logic
                return Ok();
            }
            catch (NullReferenceException ex)
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