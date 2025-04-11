namespace Chess.Main.Core.Movement
{
    public static class PieceMovement
    {
        public static void PieceMove(ref ulong moveBitBoard, ulong startBit, ulong targetBit)
        {
            moveBitBoard &= ~startBit;
            moveBitBoard |= targetBit; 
        }

        public static void PieceCapture(ref ulong captureBitboard, ulong targetBit)
        {
            captureBitboard &= ~targetBit;
        }

    }
}