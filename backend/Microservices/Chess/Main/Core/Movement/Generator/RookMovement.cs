using Chess.Main.Core.Helpers.MagicBitboards;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public static class RookMovement
    {
        public static ulong Generate(int squareIndex, ulong blockers, ulong whitePieces, int rook)
        {
            ulong mask = MagicBitboards.MagicRookTable[squareIndex].Mask;
            ulong magic = MagicBitboards.MagicRookTable[squareIndex].MagicNumber;

            int relativeBits = MagicBitboards.MagicRookTable[squareIndex].RelevantBits;

            blockers &= mask; // Blockers can be on the edge of the board (we are not interested in it)

            ulong index = (blockers * magic) >> (64 - relativeBits);

            // Allied blockers must be the same color as rook
            ulong alliedBlockers = Piece.isWhite(rook) ? blockers & whitePieces : blockers & ~whitePieces;

            return MagicBitboards.MagicRookTable[squareIndex].AttackTable[index] & ~alliedBlockers;
        }
    }
}