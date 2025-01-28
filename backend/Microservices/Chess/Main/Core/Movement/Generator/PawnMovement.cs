using Chess.Main.Core.Helpers;

namespace Chess.Main.Core.Movement.Generator
{
    public static class PawnMovement
    {
        public static ulong WhiteGenerate(ulong whitePawn, ulong allPieces, ulong blackPieces,
                                          ulong enPassantTarget)
        {
            ulong singlePush = (whitePawn << 8) & ~allPieces;
            ulong doublePush = ((singlePush << 8) & (whitePawn & Masks.WhitePawnsStartingPosition << 16)) & ~allPieces;

            ulong leftCapture = (whitePawn << 9) & Masks.NotHFile & blackPieces;
            ulong rightCapture = (whitePawn << 7) & Masks.NotAFile & blackPieces;


            ulong enPassantMove = enPassantTarget << 8;


            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }

        public static ulong BlackGenerate(ulong BlackPawn, ulong allPieces, ulong WhitePieces,
                                          ulong enPassantTarget)
        {
            ulong singlePush = (BlackPawn >> 8) & ~allPieces;
            ulong doublePush = ((singlePush >> 8) & (BlackPawn & Masks.BlackPawnsStartingPosition >> 16)) & ~allPieces;

            ulong rightCapture = (BlackPawn >> 9) & Masks.NotAFile & WhitePieces;
            ulong leftCapture = (BlackPawn >> 7) & Masks.NotHFile & WhitePieces;


            ulong enPassantMove = enPassantTarget >> 8;


            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }
    }
}