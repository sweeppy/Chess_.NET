using Chess.Main.Models;

namespace Chess.Main.Search
{
    public static class SearchAlgorithm
    {
        private static readonly char[,] promotePieces = {{'Q', 'R', 'B', 'N'}, {'q', 'r', 'b', 'n'}};
        public static ComputerMoveValues Search(Dictionary<int, List<int>> moves, Board board)
        {
            if (moves.Count == 0)
                throw new ArgumentException("No moves available");

            Random rand = new();
            int startSquareIndex = moves.Keys.ElementAt(rand.Next(0, moves.Count));
            List<int> movesOnChosenSquare = moves[startSquareIndex];
            int targetSquareIndex = rand.Next(0, movesOnChosenSquare.Count);
            int targetSquare = movesOnChosenSquare[targetSquareIndex];

            bool isWhiteTurn = board.GetIsWhiteTurn();
            bool isItPawnMove = isWhiteTurn ? 
                                (board.GetWhitePawns() & (1UL << startSquareIndex)) != 0 :
                                (board.GetBlackPawns() & (1UL << startSquareIndex)) != 0;

            char? promotionPiece = null;
            bool isItPromotionPawnMove = IsItPromotionPawnMove(isWhiteTurn, targetSquare, isItPawnMove);
            if (isItPromotionPawnMove)
            {
                promotionPiece = isWhiteTurn ? promotePieces[0, 0] : promotePieces[1, 0];
            }

            return new ComputerMoveValues(startSquareIndex, targetSquare, isItPromotionPawnMove, promotionPiece);
        }

        private static bool IsItPromotionPawnMove(bool IsPlayerPlayWhite, int targetSquare, bool isPawnMove)
        {
            if (!isPawnMove)
                return false;
            ulong promotionRank = IsPlayerPlayWhite ? 0xff_00_00_00_00_00_00_00 : 0x00_00_00_00_00_00_00_ff;
            if ((promotionRank & (1UL << targetSquare)) == 0)
                return false;

            return true;
        }
    }
}