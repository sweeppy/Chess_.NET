using System.Threading.Tasks;
using Chess.API.Interfaces;
using Chess.Data;
using Chess.DTO.Requests;
using Chess.GeneralModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Chess.API.Controllers
{
    [ApiController]
    [Route("api/ChessMovement")]
    public class ChessMovementController : ControllerBase
    {
        private readonly IMovement _movementAPI;

        private readonly GamesDbContext _db;


        public ChessMovementController(IMovement movementAPI, GamesDbContext db)
        {
            _movementAPI = movementAPI;
            _db = db;
        }

        [HttpPost("makeMove")]
        public async Task<IActionResult> MakeMove([FromBody] MoveRequest request)
        {
            string fenAfterMove = await _movementAPI.OnMove(request);
            return Ok(fenAfterMove);
        }
        [HttpPost("getLegalMoves")]
        public IActionResult GetLegalMoves([FromBody] string fen)
        {
            Dictionary<int, List<int>> legalMoves = _movementAPI.GetLegalMoves(fen);
            return Ok(legalMoves);
        }

        [HttpGet]
        public async Task<IActionResult> OnGameStart()
        {
            GameInfo newGame = new GameInfo
            {
                Fens = new List<FenEntry> 
                {
                    new FenEntry {Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"}
                },
                Moves = new List<string> { }
            };


            _db.Games.Add(newGame);
            await _db.SaveChangesAsync();

            return Ok( new {GameId = newGame.Id});
        }

        
    }
}