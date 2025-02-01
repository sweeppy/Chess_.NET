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

        public static ulong Generate(ulong knight, ulong AlliedPieces)
        { 
            // Clockwise
            ulong NNE = ((knight & Masks.NotHFile) << 15) & ~AlliedPieces;
            ulong NEE = ((knight & Masks.NotGHFile) << 6) & ~AlliedPieces;

            ulong SEE = ((knight & Masks.NotGHFile) >> 10) & ~AlliedPieces;
            ulong SSE = ((knight & Masks.NotHFile) >> 17) & ~AlliedPieces;

            ulong SSW = ((knight & Masks.NotAFile) >> 15) & ~AlliedPieces;
            ulong SWW = ((knight & Masks.NotABFile) >> 6) & ~AlliedPieces;

            ulong NWW = ((knight & Masks.NotABFile) << 10) & ~AlliedPieces;
            ulong NNW = ((knight & Masks.NotAFile) << 17) & ~AlliedPieces;

            return NNE | NEE | SEE | SSE | SSW | SWW  | NWW | NNW;
        }
    }
}