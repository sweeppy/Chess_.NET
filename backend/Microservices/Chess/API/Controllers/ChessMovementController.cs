using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Chess.API.Interfaces;
using Chess.Data;
using Chess.DTO.Requests;
using Chess.Models;
using Chess.Responses;
using Chess.Responses.GameProcess;
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

        [HttpPost("MakeMove")]
        [Authorize]
        public async Task<IActionResult> MakeMove([FromBody] MoveRequest request)
        {
            int playerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string fenAfterMove = await _movement.OnMove(request, playerId);
            return Ok(fenAfterMove);
        }

        [HttpPost("OnGameStart")]
        [Authorize]
        public async Task<IActionResult> OnGameStart([FromBody] GameStartRequest request)
        {
            try
            {
                string fen;
                if (request.IsPlayerPlayWhite)
                    fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
                else
                {
                    // TODO here will be a computer move and fen after this move will assign below
                    fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1";
                }
                            
                GameInfo newGame = new()
                {
                    Fens = [fen],
                    Moves = [],
                    IsActiveGame = true,
                    FirstPlayerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    SecondPlayerId = 0
                };
                var legalMoves = _movement.GetLegalMoves(fen);
                
                _db.Games.Add(newGame);
                await _db.SaveChangesAsync();

                GameStartResponse response = new(
                    isSuccess: true, message: "Game successfully started", fen: fen, legalMoves: legalMoves);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                BaseResponse response = new(
                    isSuccess: false, "Something went wrong while trying to create the game");
                return BadRequest(response);
            }
        } 
    }
}