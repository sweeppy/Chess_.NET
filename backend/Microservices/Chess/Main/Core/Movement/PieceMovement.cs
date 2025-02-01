namespace Chess.Main.Core.Movement
{
    public static class PieceMovement
    {
        public static void PieceMove(ref ulong bitboard, int startSquare, int targetSquare)
        {
            bitboard &= ~(1UL << startSquare);
            bitboard |= 1UL << targetSquare; 
        }

        public static void PieceCapture(ref ulong allPiecesBitboard,
                                        int startSquare, int targetSquare)
        {
            allPiecesBitboard &= ~(1Ul << targetSquare);
            PieceMove(ref allPiecesBitboard, startSquare, targetSquare);
        }
    }
}