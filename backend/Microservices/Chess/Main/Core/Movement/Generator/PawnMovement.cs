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
            ulong doublePush = ((singlePush << 8) & ~allPieces) &
            ((Masks.WhitePawnsStartingPosition & bitboardPosition) << 16);

            ulong leftCapture = ((bitboardPosition & Masks.NotHFile) << 9) & blackPieces;
            ulong rightCapture = ((bitboardPosition & Masks.NotAFile) << 7) & blackPieces;

            ulong enPassantMove = 0UL;

            ulong? enPassantTarget = board.GetEnPassantTarget();

            if(enPassantTarget.HasValue)
            {
                bool isOnEnPassantRank = squareIndex >= 32 && squareIndex <= 39;
                if (isOnEnPassantRank)
                {
                    ulong enPassantBitboard = enPassantTarget.Value;
                    ulong leftEnPassantCandidate = (enPassantBitboard << 1) & Masks.NotHFFile;
                    ulong rightEnPassantCandidate = (enPassantBitboard >> 1) & Masks.NotAFile;

                    if ((leftEnPassantCandidate == bitboardPosition) ||
                        (rightEnPassantCandidate == bitboardPosition))
                    {
                        enPassantMove = enPassantTarget.Value << 8;
                    }
                }
            }

            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }

        public static ulong BlackGenerate(int squareIndex, Board board)
        {
            ulong allPieces = board.GetAllPieces();
            ulong whitePieces = board.GetWhitePieces();

            ulong bitboardPosition = 1UL << squareIndex;

            ulong singlePush = (bitboardPosition >> 8) & ~allPieces;
            ulong doublePush = ((singlePush >> 8) & ~allPieces) &
            ((Masks.BlackPawnsStartingPosition & bitboardPosition) >> 16);

            ulong rightCapture = ((bitboardPosition & Masks.NotAFile) >> 9) & whitePieces;
            ulong leftCapture = ((bitboardPosition & Masks.NotHFile) >> 7) & whitePieces;

            ulong enPassantMove = 0UL;
            ulong? enPassantTarget = board.GetEnPassantTarget();

            if(enPassantTarget.HasValue)
            {
                bool isOnEnPassantRank = squareIndex >= 24 && squareIndex <= 31;
                if (isOnEnPassantRank)
                {
                    ulong enPassantBitboard = enPassantTarget.Value;
                    ulong leftEnPassantCandidate = (enPassantBitboard << 1) & Masks.NotAFile;
                    ulong rightEnPassantCandidate = (enPassantBitboard >> 1) & Masks.NotHFile;

                    if ((bitboardPosition == leftEnPassantCandidate) ||
                        (bitboardPosition == rightEnPassantCandidate))
                    {
                        enPassantMove = enPassantTarget.Value >> 8;
                    }
                }
            }


            return singlePush | doublePush | leftCapture | rightCapture | enPassantMove;
        }
    }
}