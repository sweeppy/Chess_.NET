
using System.Runtime.CompilerServices;
using account_service.AccountActions;
using account_service.Contracts;
using account_service.Dto;
using Microsoft.AspNetCore.Mvc;

namespace account_service.API_controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // [HttpPost]
        // public async Task<IActionResult> Login()
        // {
        //     return NoContent();
        // }

        [HttpPost("IsUserExists")]
        public async Task<IActionResult> IsUserRegistered([FromBody]string email)
        {
            try
            {
                bool result = await _userService.IsUserExists(email);
                return Ok(result);
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
    }
}