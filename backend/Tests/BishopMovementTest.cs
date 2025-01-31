using Chess.Main.Core.Helpers.MagicBitboards;
using Chess.Main.Core.Movement.Generator;
using Chess.Main.Models;
using Tests.Helpers;

namespace Tests
{
    public class BishopMovementTests
    {
        [Fact]
        public void Generate_BishopMoves_AllSquares()
        {
            for (int squareIndex = 0; squareIndex < 64; squareIndex++)
            {

                ulong blockers = GenerateTestBlockers(squareIndex);

                ulong expectedMoves = GenerateExpectedBishopMoves(squareIndex, blockers);

                ulong actualMoves = BishopMovement.Generate(squareIndex, blockers, 0, Piece.WhiteBishop); // 0 - means that all blockers are black

                Console.WriteLine($"Testing square: {squareIndex}");
                Console.WriteLine("Blockers:");
                UnitTestBitHelper.ShowOccupiedBoard(blockers);

                Console.WriteLine("Expected moves:");
                UnitTestBitHelper.ShowOccupiedBoard(expectedMoves);

                Console.WriteLine("Actual moves:");
                UnitTestBitHelper.ShowOccupiedBoard(actualMoves);


                Assert.Equal(expectedMoves, actualMoves);
            }
            // ulong[] table = MagicBitboards.MagicBishopTable[2].AttackTable;
            //     for (int index = 0; index < table.Length; index++)
            //     {
            //         Console.WriteLine(index);
            //         UnitTestBitHelper.ShowOccupiedBoard(table[index]);
            //     }
        }

        private ulong GenerateTestBlockers(int squareIndex)
        {
            ulong blockers = 0;

            int rank = squareIndex / 8;
            int file = squareIndex % 8;

            if (rank > 1 && file > 1) blockers |= 1UL << ((rank - 2) * 8 + (file - 2));
            if (rank > 1 && file < 6) blockers |= 1UL << ((rank - 2) * 8 + (file + 2));
            if (rank < 6 && file > 1) blockers |= 1UL << ((rank + 2) * 8 + (file - 2));
            if (rank < 6 && file < 6) blockers |= 1UL << ((rank + 2) * 8 + (file + 2));

            return blockers;
        }

        private ulong GenerateExpectedBishopMoves(int squareIndex, ulong blockers)
        {
            return MagicBitboards.GenerateBishopAttacks(squareIndex, blockers);
        }
    }
}