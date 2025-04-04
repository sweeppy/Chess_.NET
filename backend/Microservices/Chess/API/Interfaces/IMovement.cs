using Chess.DTO.Requests;

namespace Chess.API.Interfaces
{
    public interface IMovement
    {
        public Task<string> OnMove(MoveRequest request, int playerId);

        public Dictionary<int, List<int>> GetLegalMoves(string fen);
    }
}