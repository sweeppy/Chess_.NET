namespace Chess.Main.Models
{
    public sealed class Board
    {
        // Bitboards
        public ulong WhitePawns  { get; private set; }
        public ulong WhiteKnights { get; private set; }
        public ulong WhiteBishops { get; private set; }
        public ulong WhiteRooks { get; private set; }
        public ulong WhiteQueens { get; private set; }
        public ulong WhiteKings { get; private set; }

        public ulong BlackPawns  { get; private set; }
        public ulong BlackKnights { get; private set; }
        public ulong BlackBishops { get; private set; }
        public ulong BlackRooks { get; private set; }
        public ulong BlackQueens { get; private set; }
        public ulong BlackKings { get; private set; }

        // Initial position
        public void InitializeBoard()
        {
            WhitePawns = 0x00_00_00_00_00_00_FF_00;
            WhiteKnights = 0x00_00_00_00_00_00_00_42;
            WhiteBishops = 0x00_00_00_00_00_00_00_24;
            WhiteRooks = 0x00_00_00_00_00_00_00_81;
            WhiteQueens = 0x00_00_00_00_00_00_00_10;
            WhiteKings = 0x00_00_00_00_00_00_00_08;

            BlackPawns = 0x00_FF_00_00_00_00_00_00;
            BlackKnights = 0x42_00_00_00_00_00_00_00;
            BlackBishops = 0x24_00_00_00_00_00_00_00;
            BlackRooks = 0x81_00_00_00_00_00_00_00;
            BlackQueens = 0x08_00_00_00_00_00_00_00;
            BlackKings = 0x10_00_00_00_00_00_00_00;
        }
    }
}

// HOW BITBOARDS WORKS


// CHESS BOARD
/*
        a8 b8 c8 d8 e8 f8 g8 h8
        a7 b7 c7 d7 e7 f7 g7 h7
        a6 b6 c6 d6 e6 f6 g6 h6
        a5 b5 c5 d5 e5 f5 g5 h5
        a4 b4 c4 d4 e4 f4 g4 h4
        a3 b3 c3 d3 e3 f3 g3 h3
        a2 b2 c2 d2 e2 f2 g2 h2
        a1 b1 c1 d1 e1 f1 g1 h1
*/

// INDEXES OF THE BOARD
/*
        63 62 61 60 59 58 57 56
        55 54 53 52 51 50 49 48
        47 46 45 44 43 42 41 40
        39 38 37 36 35 34 33 32
        31 30 29 28 27 26 25 24
        23 22 21 20 19 18 17 16
        15 14 13 12 11 10  9  8
         7  6  5  4  3  2  1  0
 */


// Each byte describes one rank of the board.

// Examples for all pieces:

//      White Pawns                         Black Pawns
/*
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               1 1 1 1  1 1 1 1 : FF
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        1 1 1 1  1 1 1 1 : FF               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
 */

//      White Knights                       Black Knights
/*
        0 0 0 0  0 0 0 0 : 00               0 1 0 0  0 0 1 0 : 42
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 1 0 0  0 0 1 0 : 42               0 0 0 0  0 0 0 0 : 00
 */

//      White Bishops                       Black Bishops
/*
        0 0 0 0  0 0 0 0 : 00               0 0 1 0  0 1 0 0 : 24
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 1 0  0 1 0 0 : 24               0 0 0 0  0 0 0 0 : 00
 */

//      White Rooks                         Black Rooks
/*
        0 0 0 0  0 0 0 0 : 00               1 0 0 0  0 0 0 1 : 81
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        1 0 0 0  0 0 0 1 : 81               0 0 0 0  0 0 0 0 : 00
 */

//      White Queens                        Black Queens
/*
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  1 0 0 0 : 08
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 1  0 0 0 0 : 10               0 0 0 0  0 0 0 0 : 00
 */

    // White Kings                          Black Kings
/*
        0 0 0 0  0 0 0 0 : 00               0 0 0 1  0 0 0 0 : 10
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00               0 0 0 0  0 0 0 0 : 00
        0 0 0 0  1 0 0 0 : 08               0 0 0 0  0 0 0 0 : 00
 */