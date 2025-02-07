using System.Security.Claims;
using Account.Services;
using Microsoft.AspNetCore.Mvc;

namespace Account.API_controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // [HttpPost]
        // public async Task<IActionResult> Login()
        // {
        //     return NoContent();
        // }

        [HttpPost("EmailLogin")]
        public async Task<IActionResult> LoginByEmail([FromBody] string email)
        {
            try
            {
                // Check user in db
                bool result = await _userService.IsUserExists(email);
                if (!result)
                {

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption in AccountController HttpPost(IsUserExists). Exeption: {ex.Message}");
                return BadRequest("Something went wrong, while we were checking your email");
            }
        }

        // [HttpPost]
        // public async Task<IActionResult> VerifyCode(string Code)
        // {
        //     return NoContent();
        // }

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