using Account.Data;
using Account.DTO.Requests.GameRequests;
using Microsoft.AspNetCore.Mvc;

namespace Account.API
{
    [ApiController]
    [Route("api/user-games-service")]
    public class UserGamesServiceController : ControllerBase
    {
        private readonly UserDbContext _db;

        public UserGamesServiceController(UserDbContext db)
        {
            _db = db;
        }

        [HttpPost("startGame")]
        public async Task<IActionResult> StartGame(StartGameRequest request)
        {
            throw new NotImplementedException();
        }
    }
}