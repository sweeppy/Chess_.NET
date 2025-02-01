using Chess.Main.Core.Helpers;
using Chess.Main.Core.Helpers.BitOperation;
using Chess.Main.Models;


namespace Chess.Main.Core.Movement.Generator
{
    public static class KingMovement
    {
        private static readonly ulong[] lookUpDefaultMoves = new ulong[64];

        static KingMovement()
        {
            InitializeDefaultMovesTable();
        }
        public static ulong Generate(int squareIndex, ulong alliedPieces, bool isWhite, bool checkSafety)
        {
            ulong result = lookUpDefaultMoves[squareIndex];

            if (checkSafety)
                result &= GetKingSafeMask(alliedPieces, isWhite);

            return result;
        }

        private static void InitializeDefaultMovesTable()
        {
            for (int i = 0; i < 64; i++)
            {
                ulong moves = 0Ul;

                int rank = i / 8;
                int file = i % 8;

                for (int dr = -1; dr <= 1; dr++)
                {
                    for (int df = -1; df <= 1; df++)
                    {
                        if (dr == df && dr == 0) continue; // Skip king's square

                        int newRank = rank + dr;
                        int newFile = file + df;
                        if (newRank >= 0 && newRank < 8 && newFile >= 0 && newFile < 8)
                        {
                            moves |= 1UL << (newRank * 8 + newFile);
                        }
                    }
                }
                lookUpDefaultMoves[i] = moves;
            }
        }

        private static ulong GetKingSafeMask(ulong alliedPieces, bool isWhite)
        {
            // Get all enemy pieces bitboards
            Board board = new Board();
            ulong enemyPawns = isWhite ? board.GetBlackPawns() : board.GetWhitePawns();
            ulong enemyKnights = isWhite ? board.GetBlackKnights() : board.GetWhiteKnights();
            ulong enemyBishops = isWhite ? board.GetBlackBishops() : board.GetWhiteBishops();
            ulong enemyRooks = isWhite ? board.GetBlackRooks() : board.GetWhiteRooks();
            ulong enemyQueens = isWhite ? board.GetBlackQueens() : board.GetWhiteQueens();
            ulong enemyKing = isWhite ? board.GetBlackKings() : board.GetWhiteKings();

            ulong enemyPieces = isWhite ? board.GetBlackPieces() : board.GetWhitePieces();
            ulong allPieces = board.GetWhitePieces() | board.GetBlackPieces();

            ulong attackedMask = 0UL;

            // Add to attacked mask pawns attack
            ulong pawnsAttack = isWhite
            ? (enemyPawns & Masks.NotAFile) >> 7 | (enemyPawns & Masks.NotHFile) >> 9
            : (enemyPawns & Masks.NotHFile) << 7 | (enemyPawns & Masks.NotAFile) << 9;
            attackedMask |= pawnsAttack;

            attackedMask |= KnightMovement.Generate(enemyKnights, alliedPieces);

            // Add to attacked mask bishops and queens diagonal attacks
            ulong bishopsAndQueens = enemyBishops | enemyQueens;
            while(bishopsAndQueens != 0)
            {
                int squareindex = BitHelper.GetFirstBitIndex(bishopsAndQueens);
                attackedMask |= BishopMovement.Generate(squareindex, allPieces, alliedPieces);
                bishopsAndQueens &= bishopsAndQueens - 1; // delete first bit
            }

            // Add to attacked mask rooks and queens orthogonal attacks
            ulong rooksAndQueens = enemyRooks | enemyQueens;
            while(rooksAndQueens != 0)
            {
                int squareIndex = BitHelper.GetFirstBitIndex(rooksAndQueens);
                attackedMask |= RookMovement.Generate(squareIndex, allPieces, alliedPieces);
                rooksAndQueens &= rooksAndQueens - 1;
            }

            // Add to attacked mask king attack
            int enemyKingSquareIndex = BitHelper.GetFirstBitIndex(enemyKing);
            attackedMask |= Generate(enemyKingSquareIndex, enemyPieces, !isWhite, false);

            // Return safety mask for king
            return ~attackedMask;
        }
    }
}