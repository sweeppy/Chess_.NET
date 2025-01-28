using Chess.Main.Models;

namespace Chess.Main
{
    public static class SD // for static details
    {
        public  static Dictionary<char, int> pieceTypeFromSymbols = new Dictionary<char, int>()
        {
            ['k'] = Piece.King,
            ['n'] = Piece.Knight,
            ['b'] = Piece.Bishop,
            ['r'] = Piece.Rook,
            ['q'] = Piece.Queen,
            ['p'] = Piece.Pawn
        };
    }
}