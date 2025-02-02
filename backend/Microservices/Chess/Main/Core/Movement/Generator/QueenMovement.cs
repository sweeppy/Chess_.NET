using Chess.Main.Core.Helpers.MagicBitboards;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public class QueenMovement
    {
        public static ulong Generate(int squareIndex, Board board)
        {
            ulong bishopMask = MagicBitboards.MagicBishopTable[squareIndex].Mask;
            ulong rookMask = MagicBitboards.MagicRookTable[squareIndex].Mask;

            ulong bishopMagic = MagicBitboards.MagicBishopTable[squareIndex].MagicNumber;
            ulong rookMagic = MagicBitboards.MagicRookTable[squareIndex].MagicNumber;

            int relevantBishopBits = MagicBitboards.MagicBishopTable[squareIndex].RelevantBits;
            int relevantRookBits = MagicBitboards.MagicRookTable[squareIndex].RelevantBits;

            ulong blockers = board.GetAllPieces();

            blockers &= bishopMask | rookMask;

            ulong bishopIndex = (blockers * bishopMagic) >> (64 - relevantBishopBits);
            ulong rookIndex = (blockers * rookMagic) >> (64 - relevantRookBits);

            ulong bishopMoves = MagicBitboards.MagicBishopTable[squareIndex].AttackTable[bishopIndex];
            ulong rookMoves = MagicBitboards.MagicRookTable[squareIndex].AttackTable[rookIndex];

            ulong alliedPieces = board.GetIsWhiteTurn() ? board.GetWhitePieces() : board.GetBlackPieces();

            return (bishopMoves | rookMoves) & ~alliedPieces;
        }
    }
}