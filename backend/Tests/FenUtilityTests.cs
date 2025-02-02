using Chess.Main.Core.FEN;
using Chess.Main.Models;

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
    }
}