namespace ChessBots.Main.Models
{
    public sealed class Board
    {
        // Bitboards
        public ulong WhitePawns  { get; set; }
        public ulong WhiteKnights { get; set; }
        public ulong WhiteBishops { get; set; }
        public ulong WhiteRooks { get; set; }
        public ulong WhiteQueens { get; set; }
        public ulong WhiteKings { get; set; }

        public ulong BlackPawns  { get; set; }
        public ulong BlackKnights { get; set; }
        public ulong BlackBishops { get; set; }
        public ulong BlackRooks { get; set; }
        public ulong BlackQueens { get; set; }
        public ulong BlackKings { get; set; }

        
        public void InitializeBoard()
        {
            WhitePawns = 0x0000_0000_0000_FF00;
            WhitePawns = 0x00FF_0000_0000_FF00;
        }
    }
}

// HOW BITBOARDS WORKS

// INDEXES OF THE BOARD

/*
        56 57 58 59 60 61 62 63
        48 49 50 51 52 53 54 55
        40 41 42 43 44 45 46 47
        32 33 34 35 36 37 38 39
        24 25 26 27 28 29 30 31
        16 17 18 19 20 21 22 23 
         8  9 10 11 12 13 14 15
         0  1  2  3  4  5  6  7
 */

// White Pawns
/*
        0 0 0 0  0 0 0 0 : 00
        1 1 1 1  1 1 1 1 :: FF
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
 */

// White Knights
/*
        0 1 0 0  0 0 1 0 : 42
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
 */

 // White Knights
/*
        0 1 0 0  0 0 1 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
        0 0 0 0  0 0 0 0 : 00
 */