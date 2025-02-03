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
        public static ulong Generate(int squareIndex,  Board board, bool checkSafety)
        {
            ulong result = lookUpDefaultMoves[squareIndex];

            if (checkSafety)
            {
                ulong squaresUnderAttack = GetUnderAttackSquares(board);
                result |= GetCastlingMask(board, squaresUnderAttack);
                result &= ~squaresUnderAttack;
            }

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

        private static ulong GetUnderAttackSquares(Board board)
        {
            //Get all enemy pieces bitboards
            bool isWhite = board.GetIsWhiteTurn();
            ulong enemyPawns = isWhite ? board.GetBlackPawns() : board.GetWhitePawns();
            ulong enemyKnights = isWhite ? board.GetBlackKnights() : board.GetWhiteKnights();
            ulong enemyBishops = isWhite ? board.GetBlackBishops() : board.GetWhiteBishops();
            ulong enemyRooks = isWhite ? board.GetBlackRooks() : board.GetWhiteRooks();
            ulong enemyQueens = isWhite ? board.GetBlackQueens() : board.GetWhiteQueens();
            ulong enemyKing = isWhite ? board.GetBlackKing() : board.GetWhiteKing();

            ulong attackedMask = 0UL;

            // Add to attacked mask pawns attack
            ulong pawnsAttack = isWhite
            ? (enemyPawns & Masks.NotAFile) >> 7 | (enemyPawns & Masks.NotHFile)    >> 9
            : (enemyPawns & Masks.NotHFile) << 7 | (enemyPawns & Masks.NotAFile) << 9;
            attackedMask |= pawnsAttack;

            attackedMask |= KnightMovement.Generate(board);

            // Add to attacked mask bishops and queens diagonal attacks
            ulong bishopsAndQueens = enemyBishops | enemyQueens;
            while(bishopsAndQueens != 0)
            {
                int squareindex = BitHelper.GetFirstBitIndex(bishopsAndQueens);
                attackedMask |= BishopMovement.Generate(squareindex, board);
                bishopsAndQueens &= bishopsAndQueens - 1; // delete first bit
            }

            // Add to attacked mask rooks and queens orthogonal attacks
            ulong rooksAndQueens = enemyRooks | enemyQueens;
            while(rooksAndQueens != 0)
            {
                int squareIndex = BitHelper.GetFirstBitIndex(rooksAndQueens);
                attackedMask |= RookMovement.Generate(squareIndex, board);
                rooksAndQueens &= rooksAndQueens - 1;
            }

            // Add to attacked mask king attack
            int enemyKingSquareIndex = BitHelper.GetFirstBitIndex(enemyKing);
            attackedMask |= Generate(enemyKingSquareIndex, board, false);

            // Return safety mask for king
            return attackedMask;
        }


        private static ulong GetCastlingMask(Board board, ulong attackedSquares)
        {
            ulong result = 0UL;

            ulong blockers = board.GetAllPieces();

            bool isWhiteTurn = board.GetIsWhiteTurn();

            int kingSquareIndex = isWhiteTurn ? 3 : 59;

            // The squares that the king must pass through
            ulong kingCastleSquares = isWhiteTurn ? 0x00_00_00_00_00_00_00_03UL : 0x03_00_00_00_00_00_00_00UL;
            ulong queenCastleSquares = isWhiteTurn ? 0x00_00_00_00_00_00_00_03UL : 0x03_00_00_00_00_00_00_00UL;

            // Check if the king is under attack now
            bool isKingUnderAttack = (attackedSquares & (1UL << kingSquareIndex)) != 0;

            // Check if the squares that the king must pass through are not under attack
            bool isKingCastleNotUnderAttack = (kingCastleSquares & attackedSquares) == 0;
            bool isQueenCastleNotUnderAttack = (queenCastleSquares & attackedSquares) == 0;

            // Check if the squares that the king must pass through are free
            bool isKingCastleFree = (blockers & kingCastleSquares) == 0;
            bool isQueenCastleFree = (blockers & queenCastleSquares) == 0;

            if(!isKingUnderAttack)
            {
                if (board.GetCanWhiteKingCastle() && isKingCastleNotUnderAttack && isKingCastleFree)
                    result |= Masks.WhiteKingCastleMask;
                if (board.GetCanWhiteQueenCastle() && isQueenCastleNotUnderAttack && isQueenCastleFree)
                    result |= Masks.WhiteQueenCastleMask;
            }

            return result;
        }
    }
}