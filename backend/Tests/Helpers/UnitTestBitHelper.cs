namespace Tests.Helpers
{
    public static class UnitTestBitHelper
    {
        public static void ShowOccupiedBoard(ulong bitboard)
        {
            char[] board = new char[64];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int bitValue = (int)((bitboard >> (i * 8 + j)) & 1);
                    board[63 - (i * 8 + j)] = (bitValue == 1) ? 'X' : '.';
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write($"{board[i * 8 + j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------");
        }
    }
}

    