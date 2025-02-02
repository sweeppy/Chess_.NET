using Chess.Main.Core.Helpers;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public static class PawnMovement
    {
        public static ulong WhiteGenerate(ulong squareIndex, Board board)
        {
            ulong allPieces = board.GetAllPieces();
            ulong blackPieces = board.GetBlackPieces();


            ulong singlePush = (squareIndex << 8) & ~allPieces;
            ulong doublePush = ((singlePush << 8) & (squareIndex & Masks.WhitePawnsStartingPosition << 16)) & ~allPieces;

            ulong leftCapture = ((squareIndex & Masks.NotHFile) << 9) & blackPieces;
            ulong rightCapture = ((squareIndex & Masks.NotAFile) << 7) & blackPieces;

            ulong enPassantMove = 0UL;

            ulong? enPassantTarget = board.GetEnPassantTarget();

            if(enPassantTarget.HasValue)
            {
                enPassantMove = enPassantTarget.Value << 8;
            }

            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }

        public static ulong BlackGenerate(ulong squareIndex, Board board)
        {
            ulong allPieces = board.GetAllPieces();
            ulong whitePieces = board.GetWhitePieces();

            ulong singlePush = (squareIndex >> 8) & ~allPieces;
            ulong doublePush = ((singlePush >> 8) & (squareIndex & Masks.BlackPawnsStartingPosition >> 16)) & ~allPieces;

            ulong rightCapture = ((squareIndex & Masks.NotAFile) >> 9) & whitePieces;
            ulong leftCapture = ((squareIndex & Masks.NotHFile) >> 7) & whitePieces;

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