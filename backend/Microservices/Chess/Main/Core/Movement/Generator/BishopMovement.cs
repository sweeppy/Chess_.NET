using Chess.Main.Core.Helpers.MagicBitboards;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public static class BishopMovement
    {
        public static ulong Generate(int squareIndex, ulong blockers, ulong whitePieces, int bishop)
        {
            ulong mask = MagicBitboards.MagicBishopTable[squareIndex].Mask;
            ulong magic = MagicBitboards.MagicBishopTable[squareIndex].MagicNumber;

            int relevantBits = MagicBitboards.MagicBishopTable[squareIndex].RelevantBits;
            blockers &= mask; // Blockers can be on the edge of the board (we are not interested in it)

            ulong index = (blockers * magic) >> (64 - relevantBits);

            // Allied blockers must be the same color as bishop
            ulong alliedBlockers = Piece.isWhite(bishop) ? blockers & whitePieces : blockers & ~whitePieces;


            return MagicBitboards.MagicBishopTable[squareIndex].AttackTable[index] & ~alliedBlockers;
        }
    }
}