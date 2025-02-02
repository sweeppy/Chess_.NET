using Chess.Main.Core.Helpers;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public static class KnightMovement
    {
        // Knight moves (just for better understanding)
        /*
        k - knight position
        x - possible moves
                . . . . . . . .
                . . . x . x . . 
                . . x . . . x . 
                . . . . k . . . 
                . . x . . . x . 
                . . . x . x . . 
                . . . . . . . . 
                . . . . . . . .
                ---------------
                A B C D E F G H
        */

        public static ulong Generate(Board board)
        {
            ulong knights = 0Ul;

            bool isWhiteTurn = board.GetIsWhiteTurn();
            knights = isWhiteTurn ? board.GetWhiteKnights() : board.GetBlackKnights();
            if (knights == 0) return 0; // if there are no allied knights on the board

            ulong alliedPieces = isWhiteTurn ? board.GetWhitePieces() : board.GetBlackPieces();

            // Clockwise
            ulong NNE = ((knights & Masks.NotHFile) << 15) & ~alliedPieces;
            ulong NEE = ((knights & Masks.NotGHFile) << 6) & ~alliedPieces;

            ulong SEE = ((knights & Masks.NotGHFile) >> 10) & ~alliedPieces;
            ulong SSE = ((knights & Masks.NotHFile) >> 17) & ~alliedPieces;

            ulong SSW = ((knights & Masks.NotAFile) >> 15) & ~alliedPieces;
            ulong SWW = ((knights & Masks.NotABFile) >> 6) & ~alliedPieces;

            ulong NWW = ((knights & Masks.NotABFile) << 10) & ~alliedPieces;
            ulong NNW = ((knights & Masks.NotAFile) << 17) & ~alliedPieces;

            return NNE | NEE | SEE | SSE | SSW | SWW  | NWW | NNW;
        }
    }
}