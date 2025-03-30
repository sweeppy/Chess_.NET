using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Chess.API.Interfaces;
using Chess.Data;
using Chess.DTO.Requests;
using Chess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers
{
    [ApiController]
    [Route("api/ChessMovement")]
    public class ChessMovementController : ControllerBase
    {
        private readonly IMovement _movement;
        private readonly GamesDbContext _db;
        private readonly ILogger<ChessMovementController> _logger;


        public ChessMovementController(IMovement movementAPI, GamesDbContext db, ILogger<ChessMovementController> logger)
        {
            _movement = movementAPI;
            _db = db;
            _logger = logger;
        }

        [HttpPost("makeMove")]
        public async Task<IActionResult> MakeMove([FromBody] MoveRequest request)
        {
            string fenAfterMove = await _movement.OnMove(request);
            return Ok(fenAfterMove);
        }
        [HttpPost("getLegalMoves")]
        public IActionResult GetLegalMoves([FromBody] string fen)
        {
            Dictionary<int, List<int>> legalMoves = _movement.GetLegalMoves(fen);
            return Ok(legalMoves);
        }

        [HttpPost("OnGameStart")]
        [Authorize]
        public async Task<IActionResult> OnGameStart()
        {
            try
            {
                GameInfo newGame = new()
                {
                    Fens = ["rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"],
                    Moves = [],
                    IsActiveGame = true,
                    FirstPlayerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    SecondPlayerId = 0
                };
                
                _db.Games.Add(newGame);
                await _db.SaveChangesAsync();

                return Ok(new { GameId = newGame.Id });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw;
            }
        }
[AllowAnonymous]
[HttpGet("ValidateTest")]
public IActionResult ValidateTest([FromHeader] string authorization)
{
    try
    {
        var token = authorization.Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        
        return Ok(new {
            ValidTo = jwt.ValidTo,
            ServerTime = DateTime.UtcNow,
            IsExpired = jwt.ValidTo < DateTime.UtcNow,
            Issuer = jwt.Issuer,
            Audience = jwt.Audiences.FirstOrDefault()
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
        
    }
}