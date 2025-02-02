using Chess.Main.Core.Helpers.MagicBitboards;
using Chess.Main.Models;

namespace Chess.Main.Core.Movement.Generator
{
    public static class RookMovement
    {
        public static ulong Generate(int squareIndex, Board board)
        {
            ulong mask = MagicBitboards.MagicRookTable[squareIndex].Mask;
            ulong magic = MagicBitboards.MagicRookTable[squareIndex].MagicNumber;

            int relativeBits = MagicBitboards.MagicRookTable[squareIndex].RelevantBits;

            ulong blockers = board.GetAllPieces();

            blockers &= mask; // Blockers can be on the edge of the board (we are not interested in it)

            ulong index = (blockers * magic) >> (64 - relativeBits);

            // Rook can't capture allied pieces
            ulong alliedPieces = board.GetIsWhiteTurn() ? board.GetWhitePieces() : board.GetBlackPieces();
            return MagicBitboards.MagicRookTable[squareIndex].AttackTable[index] & ~alliedPieces;
        }
    }
}