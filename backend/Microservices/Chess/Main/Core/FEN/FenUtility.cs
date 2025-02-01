using Chess.Main.Models;

namespace Chess.Main.Core.FEN
{
    public static class FenUtility
    {
        // public static string GenerateFenFromBoard()
        // {

        // }
        
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

            bool canWhiteKingCastle = castling[0] != '-';
            bool canWhiteQueenCastle = castling[1] != '-';
            bool canBlackKingCastle = castling[2] != '-';
            bool canBlackQueenCastle = castling[3] != '-';

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
    }

    
}