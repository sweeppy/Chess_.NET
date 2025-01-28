using Chess.Main.Core.Movement.Generator;
using Xunit.Abstractions;

namespace Tests;

public class PossibleMoves
{
    private readonly ITestOutputHelper _output;

    public PossibleMoves(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void KnightMovement_Check()
    {
        ulong AlliedPieces = 0UL;
        for (int i = 0; i < 64; i++)
        {
            ulong expectedMoves = KnightMoveTable.Moves[i];
            ulong pos = 1UL << i;
            ulong result = KnightMovement.Generate(pos, AlliedPieces);

            string expectedBinary = Convert.ToString((long)expectedMoves, 2).PadLeft(64, '0');
            string resultBinary = Convert.ToString((long)result, 2).PadLeft(64, '0');
            string pos2bit = InsertUnderscoresEvery8Bits(Convert.ToString((long)pos, 2).PadLeft(64, '0'));
            expectedBinary = InsertUnderscoresEvery8Bits(expectedBinary);
            resultBinary = InsertUnderscoresEvery8Bits(resultBinary);

            _output.WriteLine($"Index: {i}, Expected: {expectedBinary},\n \t      Actual: {resultBinary}, \n pos: {pos2bit}");
            Assert.Equal(expectedMoves, result);
        }
    }

    // For better reading
    private string InsertUnderscoresEvery8Bits(string binary)
    {
        return string.Join("_", Enumerable.Range(0, binary.Length / 8)
                                           .Select(i => binary.Substring(i * 8, 8)))
                     .PadRight(binary.Length + binary.Length / 8 - 1, '_');
    }
}