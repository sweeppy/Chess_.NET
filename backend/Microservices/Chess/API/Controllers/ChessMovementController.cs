using System.Threading.Tasks;
using Chess.API.Interfaces;
using Chess.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers
{
    [ApiController]
    [Route("api/ChessMovement")]
    public class ChessMovementController : ControllerBase
    {
        private readonly IMovement _movementAPI;


        public ChessMovementController(IMovement movementAPI)
        {
            _movementAPI = movementAPI;
        }

        [HttpPost("move")]
        public async Task<IActionResult> Move([FromBody] MoveRequest request)
        {
            string fenAfterMove = await _movementAPI.OnMove(request);
            return Ok(fenAfterMove);
        }

        public IActionResult GetLegalMoves([FromBody] string fen)
        {
            Dictionary<int, List<int>> legalMoves = _movementAPI.GetLegalMoves(fen);
            return Ok(legalMoves);
        }

        
    }
}