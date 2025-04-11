using Chess.DTO.Requests;
using Chess.DTO.Responses.GameProcess;

namespace Chess.API.Interfaces
{
    public interface IMovement
    {
        public Task<OnMoveResponse> OnMove(MoveRequest request, int playerId);

        public Dictionary<int, List<int>> GetLegalMoves(string fen);
    }
}