using Chess.Main.Models;

namespace Chess.Main.Search
{
    public static class SearchAlgorithm
    {
        public static MoveValues Search(Dictionary<int, List<int>> moves)
        {
            int squaresWithMoves = Math.Max(moves.Count - 1, 0);

            Random rand = new();
            int startSquare = moves.Keys.ElementAt(rand.Next(0, squaresWithMoves));
            List<int> movesOnChosenSquare = moves[startSquare];
            int movesCount = movesOnChosenSquare.Count - 1;


            int moveIndex = rand.Next(0, movesCount);
            return new MoveValues(startSquare, movesOnChosenSquare[moveIndex]);
        }
    }
}