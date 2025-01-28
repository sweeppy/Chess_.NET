namespace Chess.Main.Core.Movement
{
    public static class PieceMovement
    {
        public static void PieceMove(ref ulong bitboard, int startSquare, int targetSquare)
        {
            bitboard &= ~(1UL << startSquare);
            bitboard |= 1UL << targetSquare; 
        }

        public static void PieceCapture(ref ulong attackerBitboard, ref ulong victimBitboard,
                                        int startSquare, int targetSquare)
         {
            victimBitboard &= ~(victimBitboard << targetSquare);
            PieceMove(ref attackerBitboard, startSquare, targetSquare);
         }
    }
}