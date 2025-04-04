using Chess.Main.Core.Helpers.BitOperation;
using Chess.Main.Models;

namespace Chess.Main.Core.Helpers.Castling
{
    public static class CastleHelper
    {
        public static bool IsCastleMove(int startSquare, int TargetSquare, Board board)
        {

            bool isTwoSquaresMove = Math.Abs(TargetSquare - startSquare) == 2; // piece moved 2 squares
            if (isTwoSquaresMove == false) return false; // return false if piece haven't been moved 2 squares

            if (board.GetIsWhiteTurn()) // checking if the white king is moving
            {
                bool isWhiteKingMoved = BitHelper.SquareIndexesFromBitboard(board.GetWhiteKing())[0] == startSquare;
                if (!isWhiteKingMoved) return false;
                return true;
            }
            else // checking is the black king is moving
            {
                bool isBlackKingMoved = BitHelper.SquareIndexesFromBitboard(board.GetBlackKing())[0] == startSquare;
                if (!isBlackKingMoved) return false;
                return true;
            }
        }
        public static bool IsKingCastle(int startSquare, int TargetSquare)
        {
            return startSquare > TargetSquare;
        }
    }
}