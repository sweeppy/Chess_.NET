namespace Chess.Main.Core.Helpers.BitOperation
{
    public static class BitHelper
    {
        private static readonly Random rnd = new Random();
        public static int BitsCount(ulong value)
        {
            int count = 0;
            while(value != 0)
            {
                value &= value - 1;
                count++;
            }
            return count;
        }

        public static int BitScanForward(ulong value)
        {
            return System.Numerics.BitOperations.TrailingZeroCount(value);
        }

        private static ulong GetRandomUlongNumber()
        {
            return (ulong)rnd.NextInt64() & (ulong)rnd.NextInt64() & (ulong)rnd.NextInt64();
        }
    }
}