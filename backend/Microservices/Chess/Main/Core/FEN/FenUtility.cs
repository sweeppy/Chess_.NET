using System.Text;
using Chess.Main.Core.Helpers.BitOperation;
using Chess.Main.Models;

namespace Chess.Main.Core.FEN
{
    public static class FenUtility
    {
        public static string GenerateFenFromBoard(Board board)
        {
            Dictionary<char, ulong> allPiecesBitboards = GetBoardBitboards(board);
            StringBuilder fenBuilder = new();

            // Generate positions string
            int emptyCount = 0;
            for (int rank = 7; rank >= 0; rank--)
            {
                for (int file = 7; file >= 0; file--)
                {
                    int squareIndex = rank * 8 + file;
                    char? pieceSymbol = GetPieceSymbolFromSquare(allPiecesBitboards, squareIndex);

                    if (!pieceSymbol.HasValue) emptyCount++;
                    else
                    {
                        if (emptyCount > 0)
                        {
                            fenBuilder.Append(emptyCount);
                            emptyCount = 0;
                        }
                        fenBuilder.Append(pieceSymbol.Value);
                    }
                }
                if (emptyCount > 0)
                {
                    fenBuilder.Append(emptyCount);
                    emptyCount = 0;
                }
                if (rank > 0) fenBuilder.Append('/');
            }

            // Whose turn
            fenBuilder.Append(board.GetIsWhiteTurn() ? " w" : " b");

            // Castling
            StringBuilder castling = new();
            if (board.GetCanWhiteKingCastle()) castling.Append('K');
            if (board.GetCanWhiteQueenCastle()) castling.Append('Q');
            if (board.GetCanBlackKingCastle()) castling.Append('k');
            if (board.GetCanBlackQueenCastle()) castling.Append('q');
            fenBuilder.Append(' ').Append(castling.Length == 0 ? '-' : castling);

            // En passant
            ulong? enPassantTarget = board.GetEnPassantTarget();
            if(enPassantTarget.HasValue)
            {
                int square = BitHelper.GetFirstBitIndex(enPassantTarget.Value);
                string squareName = squareToIndex.FirstOrDefault(x => x.Value == square).Key;
                fenBuilder.Append(' ').Append(squareName);
            }
            else fenBuilder.Append(" -");

            // Moves without capture
            fenBuilder.Append(' ').Append(board.GetMovesWithoutCapture());

            // Coming move count
            fenBuilder.Append(' ').Append(board.GetComingMoveCount());

            return fenBuilder.ToString();
        }
        
        public static Board LoadBoardFromFen(string fen)
        {
            string[] fenArr = fen.Split(' ');
            string positions = fenArr[0];
            string whoseTurn = fenArr[1];
            string castling = fenArr[2];
            string strEnPassantSquare = fenArr[3];
            string strMovesWithoutCapture = fenArr[4];
            string strComingMoveCount = fenArr[5];


            int bitPosition = 63;

            ulong whitePawns = 0UL, whiteKnights = 0UL, whiteBishops = 0UL, whiteRooks = 0UL, whiteQueens = 0UL, whiteKing = 0UL;
            ulong blackPawns = 0UL, blackKnights = 0UL, blackBishops = 0UL, blackRooks = 0UL, blackQueens = 0UL, blackKing = 0UL;

            foreach (char symbol in positions)
            {
                // New rank
                if(symbol == '/')
                {
                    bitPosition -= 8;
                    continue;
                }
                // Skip squares
                if (char.IsDigit(symbol))
                {
                    bitPosition -= (int)char.GetNumericValue(symbol);
                    continue;
                }

                int pieceType = Piece.GetPieceTypeFromSymbol(symbol);

                // Upper - white, lower - black
                bool isWhite = char.IsUpper(symbol);

                if(isWhite)
                {
                    switch (pieceType)
                    {
                        case Piece.Pawn: whitePawns |= 1UL << bitPosition; break;
                        case Piece.Knight: whiteKnights |= 1UL << bitPosition; break;
                        case Piece.Bishop: whiteBishops |= 1UL << bitPosition; break;
                        case Piece.Rook: whiteRooks |= 1UL << bitPosition; break;
                        case Piece.Queen: whiteQueens |= 1UL << bitPosition; break;
                        case Piece.King: whiteKing |= 1UL << bitPosition; break;
                    }
                }
                else
                {
                    switch (pieceType)
                    {
                        case Piece.Pawn: blackPawns |= 1UL << bitPosition; break;
                        case Piece.Knight: blackKnights |= 1UL << bitPosition; break;
                        case Piece.Bishop: blackBishops |= 1UL << bitPosition; break;
                        case Piece.Rook: blackRooks |= 1UL << bitPosition; break;
                        case Piece.Queen: blackQueens |= 1UL << bitPosition; break;
                        case Piece.King: blackKing |= 1UL << bitPosition; break;
                    }
                }
                bitPosition--;
            }

            bool isWhiteTurn = whoseTurn[0] == 'w';

            bool canWhiteKingCastle = castling.Contains('K');
            bool canWhiteQueenCastle = castling.Contains('Q');
            bool canBlackKingCastle = castling.Contains('k');
            bool canBlackQueenCastle = castling.Contains('q');


            int? enPassantSquare = squareToIndex.TryGetValue(strEnPassantSquare, out int value) ? value : null;

            int.TryParse(strMovesWithoutCapture, out int movesWithoutCapture);
            int.TryParse(strComingMoveCount, out int comingMoveCount);
  
            // ! DO NOT CHANGE THE ORDER
            return new Board(whitePawns, whiteKnights, whiteBishops, whiteRooks, whiteQueens, whiteKing,
                             canWhiteKingCastle, canWhiteQueenCastle,

                             blackPawns, blackKnights, blackBishops, blackRooks, blackQueens, blackKing,
                             canBlackKingCastle, canBlackQueenCastle,
                             
                             isWhiteTurn, enPassantSquare, movesWithoutCapture, comingMoveCount);
        }

        private static Dictionary<string, int> squareToIndex = new()
        {
            { "a8", 63 }, { "b8", 62 }, { "c8", 61 }, { "d8", 60 }, { "e8", 59 }, { "f8", 58 }, { "g8", 57 }, { "h8", 56 },
            { "a7", 55 }, { "b7", 54 }, { "c7", 53 }, { "d7", 52 }, { "e7", 51 }, { "f7", 50 }, { "g7", 49 }, { "h7", 48 },
            { "a6", 47 }, { "b6", 46 }, { "c6", 45 }, { "d6", 44 }, { "e6", 43 }, { "f6", 42 }, { "g6", 41 }, { "h6", 40 },
            { "a5", 39 }, { "b5", 38 }, { "c5", 37 }, { "d5", 36 }, { "e5", 35 }, { "f5", 34 }, { "g5", 33 }, { "h5", 32 },
            { "a4", 31 }, { "b4", 30 }, { "c4", 29 }, { "d4", 28 }, { "e4", 27 }, { "f4", 26 }, { "g4", 25 }, { "h4", 24 },
            { "a3", 23 }, { "b3", 22 }, { "c3", 21 }, { "d3", 20 }, { "e3", 19 }, { "f3", 18 }, { "g3", 17 }, { "h3", 16 },
            { "a2", 15 }, { "b2", 14 }, { "c2", 13 }, { "d2", 12 }, { "e2", 11 }, { "f2", 10 }, { "g2", 9  }, { "h2", 8  },
            { "a1", 7  }, { "b1", 6  }, { "c1", 5  }, { "d1", 4  }, { "e1", 3  }, { "f1", 2  }, { "g1", 1  }, { "h1", 0  }
        };

        private static Dictionary<char, ulong> GetBoardBitboards(Board board)
        {
            return new Dictionary<char, ulong>()
            {
                {'P', board.GetWhitePawns()},
                {'N', board.GetWhiteKnights()},
                {'B', board.GetWhiteBishops()},
                {'R', board.GetWhiteRooks()},
                {'Q', board.GetWhiteQueens()},
                {'K', board.GetWhiteKing()},

                {'p', board.GetBlackPawns()},
                {'n', board.GetBlackKnights()},
                {'b', board.GetBlackBishops()},
                {'r', board.GetBlackRooks()},
                {'q', board.GetBlackQueens()},
                {'k', board.GetBlackKing()},
            };
        }

        private static char? GetPieceSymbolFromSquare(Dictionary<char, ulong> bitboards, int square)
        {
            ulong squareMask = 1UL << square;

            foreach (var entry in bitboards)
            {
                if ((entry.Value & squareMask) != 0)
                {
                    return entry.Key;
                }
            }
            return null;
        }
    }

    
}