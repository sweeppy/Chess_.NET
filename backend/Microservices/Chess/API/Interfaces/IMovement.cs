using Chess.DTO.Requests;

namespace Chess.API.Interfaces
{
    public interface IMovement
    {
        public Task<string> OnMove(MoveRequest request);

        public Dictionary<int, List<int>> GetLegalMoves(string fen);
    }
}