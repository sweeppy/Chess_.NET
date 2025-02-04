using Chess.Main.Core.Helpers;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public static class PawnMovement
    {
        public static ulong WhiteGenerate(int squareIndex, Board board)
        {
            ulong allPieces = board.GetAllPieces();
            ulong blackPieces = board.GetBlackPieces();

            ulong bitboardPosition = 1UL << squareIndex;

            ulong singlePush = (bitboardPosition << 8) & ~allPieces;
            ulong doublePush = ((singlePush << 8) & (bitboardPosition & Masks.WhitePawnsStartingPosition << 16)) & ~allPieces;

            ulong leftCapture = ((bitboardPosition & Masks.NotHFile) << 9) & blackPieces;
            ulong rightCapture = ((bitboardPosition & Masks.NotAFile) << 7) & blackPieces;

            ulong enPassantMove = 0UL;

            ulong? enPassantTarget = board.GetEnPassantTarget();

            if(enPassantTarget.HasValue)
            {
                enPassantMove = enPassantTarget.Value << 8;
            }

            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }

        public static ulong BlackGenerate(int squareIndex, Board board)
        {
            ulong allPieces = board.GetAllPieces();
            ulong whitePieces = board.GetWhitePieces();

            ulong bitboardPosition = 1UL << squareIndex;

            ulong singlePush = (bitboardPosition >> 8) & ~allPieces;
            ulong doublePush = ((singlePush >> 8) & (bitboardPosition & Masks.BlackPawnsStartingPosition >> 16)) & ~allPieces;

            ulong rightCapture = ((bitboardPosition & Masks.NotAFile) >> 9) & whitePieces;
            ulong leftCapture = ((bitboardPosition & Masks.NotHFile) >> 7) & whitePieces;

            ulong enPassantMove = 0UL;
            ulong? enPassantTarget = board.GetEnPassantTarget();

            if(enPassantTarget.HasValue)
            {
                enPassantMove = enPassantTarget.Value >> 8;
            }


            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }
    }
}