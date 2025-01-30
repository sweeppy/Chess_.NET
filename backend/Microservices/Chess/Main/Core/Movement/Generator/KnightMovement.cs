using Chess.Main.Core.Helpers;

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

        public static ulong Generate(ulong WhiteKnight, ulong AlliedPieces)
        { 
            // Clockwise
            ulong NNE = ((WhiteKnight & Masks.NotHFile) << 15) & ~AlliedPieces;
            ulong NEE = ((WhiteKnight & Masks.NotGHFile) << 6) & ~AlliedPieces;

            ulong SEE = ((WhiteKnight & Masks.NotGHFile) >> 10) & ~AlliedPieces;
            ulong SSE = ((WhiteKnight & Masks.NotHFile) >> 17) & ~AlliedPieces;

            ulong SSW = ((WhiteKnight & Masks.NotAFile) >> 15) & ~AlliedPieces;
            ulong SWW = ((WhiteKnight & Masks.NotABFile) >> 6) & ~AlliedPieces;

            ulong NWW = ((WhiteKnight & Masks.NotABFile) << 10) & ~AlliedPieces;
            ulong NNW = ((WhiteKnight & Masks.NotAFile) << 17) & ~AlliedPieces;

            return NNE | NEE | SEE | SSE | SSW | SWW  | NWW | NNW;
        }
    }
}