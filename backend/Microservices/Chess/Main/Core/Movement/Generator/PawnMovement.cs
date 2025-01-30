using Chess.Main.Core.Helpers;

namespace Chess.Main.Core.Movement.Generator
{
    public static class PawnMovement
    {
        public static ulong WhiteGenerate(ulong squareIndex, ulong allPieces, ulong blackPieces,
                                          ulong enPassantTarget)
        {
            ulong singlePush = (squareIndex << 8) & ~allPieces;
            ulong doublePush = ((singlePush << 8) & (squareIndex & Masks.WhitePawnsStartingPosition << 16)) & ~allPieces;

            ulong leftCapture = ((squareIndex & Masks.NotHFile) << 9) & blackPieces;
            ulong rightCapture = ((squareIndex & Masks.NotAFile) << 7) & blackPieces;


            ulong enPassantMove = enPassantTarget << 8;


            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }

        public static ulong BlackGenerate(ulong squareIndex, ulong allPieces, ulong WhitePieces,
                                          ulong enPassantTarget)
        {
            ulong singlePush = (squareIndex >> 8) & ~allPieces;
            ulong doublePush = ((singlePush >> 8) & (squareIndex & Masks.BlackPawnsStartingPosition >> 16)) & ~allPieces;

            ulong rightCapture = ((squareIndex & Masks.NotAFile) >> 9) & WhitePieces;
            ulong leftCapture = ((squareIndex & Masks.NotHFile) >> 7) & WhitePieces;


            ulong enPassantMove = enPassantTarget >> 8;


            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }
    }
}