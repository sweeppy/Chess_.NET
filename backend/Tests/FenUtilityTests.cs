// using Chess.API.Implementations;
// using Chess.API.Interfaces;
// using Chess.Data;
// using Chess.Main.Core.FEN;
// using Chess.Main.Models;
// using Microsoft.EntityFrameworkCore;
// using Tests.Helpers;

// namespace Tests
// {
//     public class FenUtilityTests
//     {
//         [Fact]
//         public void GenerateFenFromBoard_InitialPosition_ReturnsCorrectFen()
//         {
//             // Arrange
//             Board board = new Board();

//             // Act
//             string fen = FenUtility.GenerateFenFromBoard(board);

//             // Assert
//             Assert.Equal("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", fen);
//         }


//         [Fact]
//         public void Generate()
//         {
//             string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

//             IMovement movement = new MovementAPI(null); //* NULL ONLY FOR THIS TEST, because here not engaged db

//             Board board = FenUtility.LoadBoardFromFen(fen);
//             Dictionary<int, List<int>> legalMoves = movement.GetLegalMoves(fen);

//             foreach(var moves in legalMoves)
//             {
//                 ulong moveMask = 0UL;
//                 foreach(int move in moves.Value)
//                 {
//                     moveMask |= 1UL << move;
//                 }
//                 UnitTestBitHelper.ShowOccupiedBoard(moveMask);
//             }
//         }
//     }
// }