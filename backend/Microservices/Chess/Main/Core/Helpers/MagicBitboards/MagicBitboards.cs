using Chess.Main.Core.Helpers.BitOperation;

namespace Chess.Main.Core.Helpers.MagicBitboards
{
    public static class MagicBitboards
    {
        public static ulong[] GenerateAllBlockerCombinations(ulong mask)
        {
            int bitsInMask = BitHelper.BitsCount(mask);
            int variations = 1 << bitsInMask; // 2^bitInMask possible variations

            ulong[] result = new ulong[variations];

            for (int i = 0; i < result.Length; i++)
            {
                ulong blocker = 0;
                int bitIndex = 0;
                
                for (ulong bit = 1; bit != 0; bit <<= 1)
                {
                    if ((mask & bit) != 0) // if blocker can be on this bit
                    {
                        if ((i & (1 << bitIndex)) != 0) // if blocker bit must be included in this combination
                        {
                            // Console.WriteLine($"BitIndex: {bitIndex}");
                            // Console.WriteLine($"bit: {bit}");
                            blocker |= bit;
                        }
                            
                        bitIndex++;
                    }
                }

                result[i] = blocker;
            }

            return result;
        }

        public static ulong GenerateBishopMask(int squareIndex)
        {
            ulong mask = 0UL;

            int rank = squareIndex / 8;
            int file = squareIndex % 8;

            // (↖)
            for (int r = rank + 1, f = file + 1; r < 7 && f < 7; r++, f++)
                mask |= 1UL << (r * 8 + f);

            // (↗)
            for (int r = rank + 1, f = file - 1; r < 7 && f > 0; r++, f--)
                mask |= 1UL << (r * 8 + f);

            // (↙)
            for (int r = rank - 1, f = file + 1; r > 0 && f < 7; r--, f++)
                mask |= 1UL << (r * 8 + f);

            // (↘)
            for (int r = rank - 1, f = file - 1; r > 0 && f > 0; r--, f--)
                mask |= 1UL << (r * 8 + f);

            return mask;
        }


        public static ulong GenerateRookMask(int squareIndex)
        {
            ulong mask = 0UL;
            int rank = squareIndex / 8;
            int file = squareIndex % 8;

            // Up
            for (int r = rank + 1; r < 7; r++)
                mask |= 1UL << (r * 8 + file);

            // Down
            for (int r = rank - 1; r > 0; r--)
                mask |= 1UL << (r * 8 + file);

            // Right
            for (int f = file + 1; f < 7; f++)
                mask |= 1UL << (rank * 8 + f);

            // Left
            for (int f = file - 1; f > 0; f--)
                mask |= 1UL << (rank * 8 + f);

            return mask;
        }

        public static ulong GenerateBishopAttacks(int squareIndex, ulong blocker)
        {
            ulong attack = 0UL;
            int rank = squareIndex / 8;
            int file = squareIndex % 8;

            // (↖)
            for (int r = rank + 1, f = file + 1; r < 8 && f < 8; r++, f++)
            {
                attack |= 1UL << (r * 8 + f);
                if ((blocker & (1UL << (r * 8 + file))) != 0) break; // blocker on target square
            }

            // (↗)
            for (int r = rank + 1, f = file - 1; r < 8 && f >= 0; r++, f--)
            {
                attack |= 1UL << (r * 8 + f);
                if ((blocker & (1UL << (r * 8 + file))) != 0) break; // blocker on target square
            }

            // (↙)
            for (int r = rank - 1, f = file + 1; r >= 0 && f < 8; r--, f++)
            {
                attack |= 1UL << (r * 8 + f);
                if ((blocker & (1UL << (r * 8 + file))) != 0) break; // blocker on target square
            }

            // (↘)
            for (int r = rank - 1, f = file - 1; r >= 0 && f >= 0; r--, f--)
            {
                attack |= 1UL << (r * 8 + f);
                if ((blocker & (1UL << (r * 8 + file))) != 0) break; // blocker on target square
            }

            return attack;
        }

        public static ulong GenerateRookAttacks(int squareIndex, ulong blocker)
        {
            ulong attack = 0UL;
            int rank = squareIndex / 8;
            int file = squareIndex % 8;

            // Up
            for (int r = rank + 1; r < 8; r++)
            {
                attack |= 1UL << (r * 8 + file);
                if ((blocker & (1UL << (r * 8 + file))) != 0) break; // blocker on target square
            }
            // Down
            for (int r = rank - 1; r >= 0; r--)
            {
                attack |= 1UL << (r * 8 + file);
                if ((blocker & (1UL << (r * 8 + file))) != 0) break; // blocker on target square
            }

            // Left
            for (int f = file + 1; f < 8; f++)
            {
                attack |= 1UL << (rank * 8 + f);
                if ((blocker & (1UL << (rank * 8 + f))) != 0) break; // blocker on target square
            }

            // Right
            for (int f = file - 1; f >= 0; f--)
            {
                attack |= 1UL << (rank * 8 + f);
                if ((blocker & (1UL << (rank * 8 + f))) != 0) break; // blocker on target square
            }

            return attack;
        }
    }
}