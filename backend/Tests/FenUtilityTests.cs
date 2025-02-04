using Chess.Main.Core.FEN;
using Chess.Main.Models;
using Tests.Helpers;

namespace Tests
{
    public class FenUtilityTests
    {
        [Fact]
        public void GenerateFenFromBoard_InitialPosition_ReturnsCorrectFen()
        {
            // Arrange
            Board board = new Board();

            // Act
            string fen = FenUtility.GenerateFenFromBoard(board);

            // Assert
            Assert.Equal("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", fen);
        }


        [Fact]
        public void Generate()
        {
            string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

            Board board = FenUtility.LoadBoardFromFen(fen);

            UnitTestBitHelper.ShowOccupiedBoard(board.GetBlackBishops());
        }
    }
}