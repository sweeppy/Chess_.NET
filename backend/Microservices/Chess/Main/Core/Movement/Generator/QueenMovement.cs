using Chess.Main.Core.Helpers.MagicBitboards;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public class QueenMovement
    {
        public static ulong Generate(int squareIndex, ulong blockers, ulong alliedPieces)
        {
            ulong bishopMask = MagicBitboards.MagicBishopTable[squareIndex].Mask;
            ulong rookMask = MagicBitboards.MagicRookTable[squareIndex].Mask;

            ulong bishopMagic = MagicBitboards.MagicBishopTable[squareIndex].MagicNumber;
            ulong rookMagic = MagicBitboards.MagicRookTable[squareIndex].MagicNumber;

            int relevantBishopBits = MagicBitboards.MagicBishopTable[squareIndex].RelevantBits;
            int relevantRookBits = MagicBitboards.MagicRookTable[squareIndex].RelevantBits;

            blockers &= bishopMask | rookMask;

            ulong bishopIndex = (blockers * bishopMagic) >> (64 - relevantBishopBits);
            ulong rookIndex = (blockers * rookMagic) >> (64 - relevantRookBits);

            ulong bishopMoves = MagicBitboards.MagicBishopTable[squareIndex].AttackTable[bishopIndex];
            ulong rookMoves = MagicBitboards.MagicRookTable[squareIndex].AttackTable[rookIndex];

            return (bishopMoves | rookMoves) & ~alliedPieces;
        }
    }
}