using Chess.DTO.Requests;
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

        private ulong BlackPawns;
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

        private int DrawMoves;

        private int ComingMoveCount;

        public Board()
        {
            InitializeBoard();
        }

        public Board(ulong whitePawns, ulong whiteKnights, ulong whiteBishops, ulong whiteRooks,
                     ulong whiteQueens, ulong whiteKing, bool canWhiteKingCastle, bool canWhiteQueenCastle,

                     ulong blackPawns, ulong blackKnights, ulong blackBishops, ulong blackRooks,
                     ulong blackQueens, ulong blackKing, bool canBlackKingCastle, bool canBlackQueenCastle,

                     bool isWhiteTurn, int? enPassantSquare, int drawMoves, int comingMoveCount)
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
            DrawMoves = drawMoves;
            ComingMoveCount = comingMoveCount;

            if (enPassantSquare.HasValue)
            {
                EnPassantTarget = 1UL << enPassantSquare.Value;
            }
        }
        public Board(Board board)
        {
            // Copy bitboards
            WhitePawns = board.WhitePawns;
            WhiteKnights = board.WhiteKnights;
            WhiteBishops = board.WhiteBishops;
            WhiteRooks = board.WhiteRooks;
            WhiteQueens = board.WhiteQueens;
            WhiteKing = board.WhiteKing;
            WhitePieces = board.WhitePieces;

            BlackPawns = board.BlackPawns;
            BlackKnights = board.BlackKnights;
            BlackBishops = board.BlackBishops;
            BlackRooks = board.BlackRooks;
            BlackQueens = board.BlackQueens;
            BlackKing = board.BlackKing;
            BlackPieces = board.BlackPieces;

            allPieces = board.allPieces;

            // Copy castling rights
            CanWhiteKingCastle = board.CanWhiteKingCastle;
            CanWhiteQueenCastle = board.CanWhiteQueenCastle;
            CanBlackKingCastle = board.CanBlackKingCastle;
            CanBlackQueenCastle = board.CanBlackQueenCastle;

            // Copy game state
            IsWhiteTurn = board.IsWhiteTurn;
            EnPassantTarget = board.EnPassantTarget;
            DrawMoves = board.DrawMoves;
            ComingMoveCount = board.ComingMoveCount;
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
            DrawMoves = 0;
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

        public int GetDrawMoves() => DrawMoves;

        public int GetComingMoveCount() => ComingMoveCount;

        public void MakeMove(int startSquare, int targetSquare, ref Board board)
        {
            // For FEN
            board.ComingMoveCount++;
            if (IsItMoveForDraw(startSquare, targetSquare, board))
                board.DrawMoves++;
            else
                board.DrawMoves = 0;

            bool isKingCastle = CastleHelper.IsKingCastle(startSquare, targetSquare);
            if (CastleHelper.IsCastleMove(startSquare, targetSquare, board))
            {
                MakeCastleMove(ref board, isKingCastle);
            }
            else
            {
                ulong targetBit = 1UL << targetSquare;
                if (((targetBit & WhitePieces) != 0) || ((targetBit & BlackPieces) != 0))
                {
                    MakeMoveWithCapture(startSquare, targetSquare, ref board);
                }
                else MakeMoveWithoutCapture(startSquare, targetSquare, ref board);
            }

            board.IsWhiteTurn = !IsWhiteTurn;

        }

        private static void MakeMoveWithoutCapture(int startSquare, int targetSquare, ref Board board)
        {
            ulong startBit = 1Ul << startSquare;
            ulong targetBit = 1UL << targetSquare;

            bool isUnsetEnPassant = true;

            if (board.IsWhiteTurn)
            {
                if ((board.WhitePawns & startBit) != 0)
                {
                    bool isEnPassantMove = (targetBit >> 8) == board.EnPassantTarget;
                    if (isEnPassantMove && board.EnPassantTarget.HasValue)
                        PieceMovement.EnPassantMove(ref board.WhitePawns, startBit, targetBit,
                        ref board.BlackPawns, ref board.BlackPieces, ref board.allPieces, board.EnPassantTarget.Value);
                    else
                    {
                        PieceMovement.PieceMove(ref board.WhitePawns, startBit, targetBit);
                        if ((startSquare + 16) == targetSquare) board.EnPassantTarget = targetBit;
                        isUnsetEnPassant = false;
                    }
                }
                else if ((board.WhiteKnights & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteKnights, startBit, targetBit);
                else if ((board.WhiteBishops & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteBishops, startBit, targetBit);
                else if ((board.WhiteRooks & startBit) != 0)
                {
                    PieceMovement.PieceMove(ref board.WhiteRooks, startBit, targetBit);
                    if (startSquare == 0) board.CanWhiteKingCastle = false;
                    else if (startSquare == 7) board.CanWhiteQueenCastle = false;
                }
                else if ((board.WhiteQueens & startBit) != 0) PieceMovement.PieceMove(ref board.WhiteQueens, startBit, targetBit);
                else if ((board.WhiteKing & startBit) != 0)
                {
                    PieceMovement.PieceMove(ref board.WhiteKing, startBit, targetBit);
                    board.CanWhiteKingCastle = false;
                    board.CanWhiteQueenCastle = false;
                }
                board.allPieces &= ~startBit;
                board.WhitePieces &= ~startBit;

                board.allPieces |= startBit;
                board.BlackPieces |= startBit;

                if (isUnsetEnPassant) board.EnPassantTarget = null;
            }
            else
            {
                if ((board.BlackPawns & startBit) != 0)
                {
                    bool isEnPassantMove = (targetBit << 8) == board.EnPassantTarget;
                    if (isEnPassantMove && board.EnPassantTarget.HasValue)
                        PieceMovement.EnPassantMove(ref board.BlackPawns, startBit, targetBit,
                        ref board.WhitePawns, ref board.WhitePieces, ref board.allPieces, board.EnPassantTarget.Value);
                    else
                    {
                        PieceMovement.PieceMove(ref board.BlackPawns, startBit, targetBit);
                        if ((startSquare - 16) == targetSquare) board.EnPassantTarget = targetBit;
                        isUnsetEnPassant = false;
                    }
                }
                else if ((board.BlackKnights & startBit) != 0) PieceMovement.PieceMove(ref board.BlackKnights, startBit, targetBit);
                else if ((board.BlackBishops & startBit) != 0) PieceMovement.PieceMove(ref board.BlackBishops, startBit, targetBit);
                else if ((board.BlackRooks & startBit) != 0)
                {
                    PieceMovement.PieceMove(ref board.BlackRooks, startBit, targetBit);
                    if (startSquare == 56) board.CanBlackKingCastle = false;
                    else if (startSquare == 63) board.CanBlackQueenCastle = false;
                }
                else if ((board.BlackQueens & startBit) != 0) PieceMovement.PieceMove(ref board.BlackQueens, startBit, targetBit);
                else if ((board.BlackKing & startBit) != 0)
                {
                    PieceMovement.PieceMove(ref board.BlackKing, startBit, targetBit);
                    board.CanBlackKingCastle = false;
                    board.CanBlackQueenCastle = false;
                }    

                board.allPieces = (board.allPieces & ~startBit) | targetBit;
                board.BlackPieces = (board.BlackPieces & ~startBit) | targetBit;

                if (isUnsetEnPassant) board.EnPassantTarget = null;
            }

        }
        private static void MakeMoveWithCapture(int startSquare, int targetSquare, ref Board board)
        {
            ulong targetBit = 1UL << targetSquare;

            MakeMoveWithoutCapture(startSquare, targetSquare, ref board);

            if (board.IsWhiteTurn)
            {
                if ((board.BlackPawns & targetBit) != 0) PieceMovement.PieceCaptured(ref board.BlackPawns, targetBit);
                else if ((board.BlackKnights & targetBit) != 0) PieceMovement.PieceCaptured(ref board.BlackKnights, targetBit);
                else if ((board.BlackBishops & targetBit) != 0) PieceMovement.PieceCaptured(ref board.BlackBishops, targetBit);
                else if ((board.BlackRooks & targetBit) != 0) PieceMovement.PieceCaptured(ref board.BlackRooks, targetBit);
                else if ((board.BlackQueens & targetBit) != 0) PieceMovement.PieceCaptured(ref board.BlackQueens, targetBit);
                board.BlackPieces &= ~targetBit;
            }
            else
            {
                if ((board.WhitePawns & targetBit) != 0) PieceMovement.PieceCaptured(ref board.WhitePawns, targetBit);
                else if ((board.WhiteKnights & targetBit) != 0) PieceMovement.PieceCaptured(ref board.WhiteKnights, targetBit);
                else if ((board.WhiteBishops & targetBit) != 0) PieceMovement.PieceCaptured(ref board.WhiteBishops, targetBit);
                else if ((board.WhiteRooks & targetBit) != 0) PieceMovement.PieceCaptured(ref board.WhiteRooks, targetBit);
                else if ((board.WhiteQueens & targetBit) != 0) PieceMovement.PieceCaptured(ref board.WhiteQueens, targetBit);

                board.WhitePieces &= ~targetBit;
            }
        }


        private static void MakeCastleMove(ref Board board, bool isKingCastle)
        {
            if (board.IsWhiteTurn) // White castle
            {
                ulong kingStartBit = board.WhiteKing;
                ulong kingTargetBit = isKingCastle ? 1UL << 1 : 1UL << 5;

                ulong rookStartBit = isKingCastle ? 1UL : 1UL << 7;
                ulong rookTargetBit = isKingCastle ? 1UL << 2 : 1UL << 4;

                PieceMovement.PieceMove(ref board.WhiteKing, kingStartBit, kingTargetBit);
                PieceMovement.PieceMove(ref board.WhiteRooks, rookStartBit, rookTargetBit);

                // Delete castling pieces in all pieces and <color>Pieces in bitboards
                board.allPieces &= ~kingStartBit;
                board.WhitePieces &= ~kingStartBit;
                board.allPieces &= ~rookStartBit;
                board.WhitePieces &= ~rookStartBit;

                // Add castling pieces in all pieces and <color>Pieces in bitboards
                board.allPieces |= kingTargetBit;
                board.WhitePieces |= kingTargetBit;
                board.allPieces |= rookTargetBit;
                board.WhitePieces |= rookTargetBit;

                board.CanWhiteKingCastle = false;
                board.CanWhiteQueenCastle = false;
            }
            else // Black castle
            {
                ulong kingStartBit = 1UL << 59;
                ulong kingTargetBit = isKingCastle ? 1UL << 57 : 1UL << 61;

                ulong rookStartBit = isKingCastle ? 1UL << 56 : 1UL << 63;
                ulong rookTargetBit = isKingCastle ? 1UL << 58 : 1UL << 60;

                PieceMovement.PieceMove(ref board.BlackKing, kingStartBit, kingTargetBit);
                PieceMovement.PieceMove(ref board.BlackRooks, rookStartBit, rookTargetBit);

                // Delete castling pieces in all pieces and <color>Pieces in bitboards
                board.allPieces &= ~kingStartBit;
                board.BlackPieces &= ~kingStartBit;
                board.allPieces &= ~rookStartBit;
                board.BlackPieces &= ~rookStartBit;

                // Add castling pieces in all pieces and <color>Pieces in bitboards
                board.allPieces |= kingTargetBit;
                board.BlackPieces |= kingTargetBit;
                board.allPieces |= rookTargetBit;
                board.BlackPieces |= rookTargetBit;

                board.CanBlackKingCastle = false;
                board.CanBlackQueenCastle = false;

                board.EnPassantTarget = 0UL;
            }
        }

        public void PromotePawn(int startSquare, int targetSquare, char chosenPiece, ref Board board)
        {
            if (board.IsWhiteTurn)
            {
                board.WhitePawns ^= 1UL << startSquare;
                board.WhitePieces ^= 1UL << startSquare;

                switch (chosenPiece)
                {
                    case 'Q':
                        board.WhiteQueens |= 1UL << targetSquare;
                        break;
                    case 'R':
                        board.WhiteRooks |= 1UL << targetSquare;
                        break;
                    case 'B':
                        board.WhiteBishops |= 1UL << targetSquare;
                        break;
                    case 'N':
                        board.WhiteKnights |= 1UL << targetSquare;
                        break;
                }
            }
            else
            {
                board.BlackPawns ^= 1UL << startSquare;
                board.BlackPieces ^= 1UL << startSquare;

                switch (chosenPiece)
                {
                    case 'q':
                        board.BlackQueens |= 1UL << targetSquare;
                        break;
                    case 'r':
                        board.BlackRooks |= 1UL << targetSquare;
                        break;
                    case 'b':
                        board.BlackBishops |= 1UL << targetSquare;
                        break;
                    case 'n':
                        board.BlackKnights |= 1UL << targetSquare;
                        break;
                }
            }
            board.allPieces ^= 1UL << startSquare;

            board.IsWhiteTurn = !board.IsWhiteTurn;
        }

        private static bool IsItMoveForDraw(int startSquare, int targetSquare, Board board)
        {
            ulong startBit = 1UL << startSquare;
            ulong targetBit = 1UL << targetSquare;

            // If it's a pawn move, it's NOT a draw move
            if (board.IsWhiteTurn)
            {
                if ((startBit & board.WhitePawns) != 0) return false;
            }
            else
            {
                if ((startBit & board.BlackPawns) != 0) return false;
            }

            // If it's a capture, it's NOT a draw move
            if ((targetBit & (board.IsWhiteTurn ? board.BlackPieces : board.WhitePieces)) != 0)
                return false;

            // Otherwise, it's a draw move
            return true;

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