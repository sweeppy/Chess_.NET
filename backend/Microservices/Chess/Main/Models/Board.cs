using Chess.Main.Core.Helpers.Castling;
using Chess.Main.Core.Movement;

namespace Chess.Main.Models
{
    public sealed class Board
    {
        // Bitboards
        private ulong WhitePawns;
        private ulong WhiteKnights;
        private ulong WhiteBishops;
        private ulong WhiteRooks;
        private ulong WhiteQueens;
        private ulong WhiteKing;

        private ulong BlackPawns ;
        private ulong BlackKnights;
        private ulong BlackBishops;
        private ulong BlackRooks;
        private ulong BlackQueens;
        private ulong BlackKing;

        private ulong WhitePieces;
        private ulong BlackPieces;

        private ulong allPieces;

        private bool CanWhiteKingCastle;
        private bool CanWhiteQueenCastle;
        private bool CanBlackKingCastle;
        private bool CanBlackQueenCastle;

        private bool IsWhiteTurn;

        private ulong? EnPassantTarget;

        private int MovesWithoutCapture;

        private int ComingMoveCount;

        public Board()
        {
            InitializeBoard();
        }

        public Board(ulong whitePawns, ulong whiteKnights, ulong whiteBishops, ulong whiteRooks,
                     ulong whiteQueens, ulong whiteKing, bool canWhiteKingCastle, bool canWhiteQueenCastle,

                     ulong blackPawns, ulong blackKnights, ulong blackBishops, ulong blackRooks,
                     ulong blackQueens, ulong blackKing, bool canBlackKingCastle, bool canBlackQueenCastle,
                     
                     bool isWhiteTurn, int? enPassantSquare, int movesWithoutCapture, int comingMoveCount)
        {
            WhitePawns = whitePawns;
            WhiteKnights = whiteKnights;
            WhiteBishops = whiteBishops;
            WhiteRooks = whiteRooks;
            WhiteQueens = whiteQueens;
            WhiteKing = whiteKing;
            WhitePieces = whitePawns | whiteKnights | whiteBishops | whiteRooks | whiteQueens | whiteKing;
            CanWhiteKingCastle = canWhiteKingCastle;
            CanWhiteQueenCastle = canWhiteQueenCastle;

            BlackPawns = blackPawns;
            BlackKnights = blackKnights;
            BlackBishops = blackBishops;
            BlackRooks = blackRooks;
            BlackQueens = blackQueens;
            BlackKing = blackKing;
            BlackPieces = blackPawns | blackKnights | blackBishops | blackRooks | blackQueens | blackKing;
            CanBlackKingCastle = canBlackKingCastle;
            CanBlackQueenCastle = canBlackQueenCastle;

            allPieces = WhitePieces | BlackPieces;

            IsWhiteTurn = isWhiteTurn;
            MovesWithoutCapture = movesWithoutCapture;
            ComingMoveCount = comingMoveCount;
            
            if(enPassantSquare.HasValue)
            {
                EnPassantTarget = 1UL << enPassantSquare.Value;
            }
        }

        // Initial position
        public void InitializeBoard()
        {
            WhitePawns = 0x00_00_00_00_00_00_FF_00;
            WhiteKnights = 0x00_00_00_00_00_00_00_42;
            WhiteBishops = 0x00_00_00_00_00_00_00_24;
            WhiteRooks = 0x00_00_00_00_00_00_00_81;
            WhiteQueens = 0x00_00_00_00_00_00_00_10;
            WhiteKing = 0x00_00_00_00_00_00_00_08;
            WhitePieces = WhitePawns | WhiteKnights | WhiteBishops | WhiteRooks | WhiteQueens | WhiteKing;

            BlackPawns = 0x00_FF_00_00_00_00_00_00;
            BlackKnights = 0x42_00_00_00_00_00_00_00;
            BlackBishops = 0x24_00_00_00_00_00_00_00;
            BlackRooks = 0x81_00_00_00_00_00_00_00;
            BlackQueens = 0x10_00_00_00_00_00_00_00;
            BlackKing = 0x08_00_00_00_00_00_00_00;
            BlackPieces = BlackPawns | BlackKnights | BlackBishops | BlackRooks | BlackQueens | BlackKing;

            allPieces = WhitePieces | BlackPieces;

            // Set initial game state
            IsWhiteTurn = true;
            CanWhiteKingCastle = true;
            CanWhiteQueenCastle = true;
            CanBlackKingCastle = true;
            CanBlackQueenCastle = true;
            EnPassantTarget = null;
            MovesWithoutCapture = 0;
            ComingMoveCount = 1;
        }

        public ulong GetWhitePawns() => WhitePawns;
        public ulong GetWhiteKnights() => WhiteKnights;
        public ulong GetWhiteBishops() => WhiteBishops;
        public ulong GetWhiteRooks() => WhiteRooks;
        public ulong GetWhiteQueens() => WhiteQueens;
        public ulong GetWhiteKing() => WhiteKing;

        public ulong GetBlackPawns() => BlackPawns;
        public ulong GetBlackKnights() => BlackKnights;
        public ulong GetBlackBishops() => BlackBishops;
        public ulong GetBlackRooks() => BlackRooks;
        public ulong GetBlackQueens() => BlackQueens;
        public ulong GetBlackKing() => BlackKing;


        public ulong GetWhitePieces() => WhitePieces;
        public ulong GetBlackPieces() => BlackPieces;

        public ulong GetAllPieces() => allPieces;

        public bool GetIsWhiteTurn() => IsWhiteTurn;

        public bool GetCanWhiteKingCastle() => CanWhiteKingCastle;
        public bool GetCanWhiteQueenCastle() => CanWhiteQueenCastle;
        public bool GetCanBlackKingCastle() => CanBlackKingCastle;
        public bool GetCanBlackQueenCastle() => CanBlackQueenCastle;

        public ulong? GetEnPassantTarget() => EnPassantTarget;

        public int GetMovesWithoutCapture() => MovesWithoutCapture;

        public int GetComingMoveCount() => ComingMoveCount;

        public void MakeMove(int startSquare, int targetSquare, ref Board board)
        {
            bool isKingCastle = CastleHelper.IsKingCastle(startSquare, targetSquare);
            if (CastleHelper.IsCastleMove(startSquare, targetSquare, board) && isKingCastle)
            {
                MakeCastleMove(board, isKingCastle);
            }
            else
            {
                ulong targetBit = 1UL << targetSquare;
                if (((targetBit & WhitePieces) != 0) || ((targetBit & BlackPieces) != 0))
                {
                    MakeMoveWithCapture(startSquare, targetSquare, ref board);
                }
                else MakeMoveWithoutCapture(startSquare, targetSquare, board);

                board.IsWhiteTurn = !IsWhiteTurn;
            }


        }

        private static void MakeMoveWithoutCapture(int startSquare, int targetSquare, Board board)
        {
            ulong startBit = 1Ul << startSquare;
            ulong targetBit = 1UL << targetSquare;

            if (board.IsWhiteTurn)
            {
                if ((board.WhitePawns & startBit) != 0) PieceMovement.PieceMove(ref board.WhitePawns, startBit, targetBit);
                else if ((board.WhiteKnights & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteKnights, startBit, targetBit);
                else if ((board.WhiteBishops & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteBishops, startBit, targetBit);
                else if ((board.WhiteRooks & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteRooks, startBit, targetBit);
                else if ((board.WhiteQueens & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteQueens, startBit, targetBit);
                else if ((board.WhiteKing & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteKing, startBit, targetBit);
                board.allPieces &= ~startBit;
                board.WhitePieces &= ~startBit;
                
                board.allPieces |= targetBit;
                board.BlackPieces |= startBit;
            }
            else
            {
                if ((board.BlackPawns & startBit) != 0) PieceMovement.PieceMove(ref board.BlackPawns, startBit, targetBit);
                else if ((board.BlackKnights & startBit) != 0) PieceMovement.PieceMove(ref board.BlackKnights, startBit, targetBit);
                else if ((board.BlackBishops & startBit) != 0) PieceMovement.PieceMove(ref board.BlackBishops, startBit, targetBit);
                else if ((board.BlackRooks & startBit) != 0) PieceMovement.PieceMove(ref board.BlackRooks, startBit, targetBit);
                else if ((board.BlackQueens & startBit) != 0) PieceMovement.PieceMove(ref board.BlackQueens, startBit, targetBit);
                else if ((board.BlackKing & startBit) != 0) PieceMovement.PieceMove(ref board.BlackKing, startBit, targetBit);
                board.allPieces &= ~startBit;
                board.BlackPieces &= ~startBit;

                board.allPieces |= targetBit;
                board.BlackPieces |= startBit;
            }
        
        }
        private static void MakeMoveWithCapture(int startSquare, int targetSquare, ref Board board)
        {
            ulong startBit = 1Ul << startSquare;
            ulong targetBit = 1UL << targetSquare;
            if (board.IsWhiteTurn)
            {
                if ((board.WhitePawns & startBit) != 0)PieceMovement.PieceCapture(ref board.WhitePawns, targetBit);
                else if ((board.WhiteKnights & startBit) != 0) PieceMovement.PieceCapture(ref board.WhiteKnights, targetBit);
                else if ((board.WhiteBishops & startBit) != 0) PieceMovement.PieceCapture(ref board.WhiteBishops, targetBit);
                else if ((board.WhiteRooks & startBit) != 0) PieceMovement.PieceCapture(ref board.WhiteRooks, targetBit);
                else if ((board.WhiteQueens & startBit) != 0) PieceMovement.PieceCapture(ref board.WhiteQueens, targetBit);
                else if ((board.WhiteKing & startBit) != 0) PieceMovement.PieceCapture(ref board.WhiteKing, targetBit);
            }
            else
            {
                if ((board.BlackPawns & startBit) != 0) PieceMovement.PieceCapture(ref board.BlackPawns, targetBit);
                else if ((board.BlackKnights & startBit) != 0) PieceMovement.PieceCapture(ref board.BlackKnights, targetBit);
                else if ((board.BlackBishops & startBit) != 0) PieceMovement.PieceCapture(ref board.BlackBishops, targetBit);
                else if ((board.BlackRooks & startBit) != 0) PieceMovement.PieceCapture(ref board.BlackRooks, targetBit);
                else if ((board.BlackQueens & startBit) != 0) PieceMovement.PieceCapture(ref board.BlackQueens, targetBit);
                else if ((board.BlackKing & startBit) != 0) PieceMovement.PieceCapture(ref board.BlackKing, targetBit);
            }
            MakeMoveWithoutCapture(startSquare, targetSquare, board);
        }


        private static void MakeCastleMove(Board board, bool isKingCastle)
        {
            if (board.IsWhiteTurn) // White castle
            {
                ulong kingStartBit = 1UL << 3;
                ulong kingTargetBit = isKingCastle ? 1UL << 1 : 1UL << 5;

                ulong rookStartBit = isKingCastle ? 1UL : 1UL << 7;
                ulong rookTargetBit = isKingCastle ? 1UL << 2 : 1UL << 4;



                PieceMovement.PieceMove(ref board.allPieces, kingStartBit, kingTargetBit);
                PieceMovement.PieceMove(ref board.allPieces, rookStartBit, rookTargetBit);

                board.CanWhiteKingCastle = false;
                board.CanWhiteQueenCastle = false;
            }
            else // Black castle
            {
                ulong kingStartBit = 1UL << 59;
                ulong kingTargetBit = isKingCastle ? 1UL << 57 : 1UL << 61;

                ulong rookStartBit = isKingCastle ? 1UL << 56 : 1UL << 63;
                ulong rookTargetBit = isKingCastle ? 1UL << 58 : 1UL << 60;
                
                PieceMovement.PieceMove(ref board.allPieces, kingStartBit, kingTargetBit);
                PieceMovement.PieceMove(ref board.allPieces, rookStartBit, rookTargetBit);

                board.CanBlackKingCastle = false;
                board.CanBlackQueenCastle = false;
            }
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

// Examples for initial position:

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