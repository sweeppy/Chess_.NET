namespace Chess.Main.Core.Movement
{
    public static class PieceMovement
    {
        public static void PieceMove(ref ulong moveBitboard, ulong startBit, ulong targetBit)
        {
            moveBitboard &= ~startBit;
            moveBitboard |= targetBit;
        }

        public static void CapturePiece(ref ulong captureBitboard, ulong targetBit)
        {
            captureBitboard &= ~targetBit;
        }


        public static void EnPassantMove(ref ulong moveBitboard, ulong startBit, ulong targetBit,
            ref ulong enemyPawnsBitboard, ref ulong enemyPieces, ref ulong allPieces, ulong enPassantBitboard)
        {
            moveBitboard &= ~startBit;
            moveBitboard |= targetBit;

            enemyPawnsBitboard &= ~enPassantBitboard;
            enemyPieces &= ~enPassantBitboard;
            allPieces &= ~enPassantBitboard;
        }

    }
}