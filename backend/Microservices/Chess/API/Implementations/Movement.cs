using System.Text;
using Chess.API.Interfaces;
using Chess.Data;
using Chess.DTO.Requests;
using Chess.Models;
using Chess.Main.Core.FEN;
using Chess.Main.Core.Helpers.BitOperation;
using Chess.Main.Core.Helpers.Squares;
using Chess.Main.Core.Movement.Generator;
using Chess.Main.Models;
using Chess.DTO.Responses.GameProcess;


namespace Chess.API.Implementations
{
    public class Movement : IMovement
    {
        private readonly GamesDbContext _db;

        public Movement(GamesDbContext db)
        {
            _db = db;
        }

        public Dictionary<int, List<int>> GetLegalMoves(string fen)
        {
            Dictionary<int, List<int>> legalMoves = [];

            Board board = FenUtility.LoadBoardFromFen(fen);

            // TODO Use it in future for mate flag
            // bool isInCheck = KingMovement.IsKingUnderAttack(board);

            Dictionary<char, ulong> piecesCollections = GetActivePieces(board);

            for (int square = 0; square < 64; square++)
                {
                    foreach (ulong pieces in piecesCollections.Values)
                    {
                        if ((pieces & (1UL << square)) != 0)
                        {
                            char pieceSymbol = piecesCollections.FirstOrDefault(x => (x.Value & (1UL << square)) != 0).Key;

                            ulong rawMoves = GetMovesByPieceSymbol(square, pieceSymbol, board);

                            List<int> validMoves = [];
                            foreach (int targetSquare in BitHelper.SquareIndexesFromBitboard(rawMoves))
                            {
                                Board tempBoard = FenUtility.LoadBoardFromFen(fen);
                                tempBoard.MakeMove(square, targetSquare, ref tempBoard);
                                
                                if (KingMovement.WillKingBeInSafeAfterImagineMove(tempBoard))
                                {
                                    validMoves.Add(targetSquare);
                                }
                            }
                            
                            if (validMoves.Count > 0)
                            {
                                legalMoves[square] = validMoves;
                            }
                        }
                    }
                }

            return legalMoves;
        }

        private static Dictionary<char, ulong> GetActivePieces(Board board)
        {
            Dictionary<char, ulong> pieces = [];

            if(board.GetIsWhiteTurn())
            {
                pieces.Add('P', board.GetWhitePawns());
                pieces.Add('N', board.GetWhiteKnights());
                pieces.Add('B', board.GetWhiteBishops());
                pieces.Add('R', board.GetWhiteRooks());
                pieces.Add('Q', board.GetWhiteQueens());
                pieces.Add('K', board.GetWhiteKing());
            }
            else
            {
                pieces.Add('p', board.GetBlackPawns());
                pieces.Add('n', board.GetBlackKnights());
                pieces.Add('b', board.GetBlackBishops());
                pieces.Add('r', board.GetBlackRooks());
                pieces.Add('q', board.GetBlackQueens());
                pieces.Add('k', board.GetBlackKing());
            }

            return pieces;
        }

        private static ulong GetMovesByPieceSymbol(int squareIndex, char pieceSymbol, Board board)
        {
            return pieceSymbol switch
            {
                'P' => PawnMovement.WhiteGenerate(squareIndex, board),
                'N' => KnightMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'B' => BishopMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'R' => RookMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'Q' => QueenMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'K' => KingMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'p' => PawnMovement.BlackGenerate(squareIndex, board),
                'n' => KnightMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'b' => BishopMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'r' => RookMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'q' => QueenMovement.Generate(squareIndex, board, board.GetIsWhiteTurn()),
                'k' => KingMovement.Generate(squareIndex, board),
                _ => 0,
            };
        }

        public async Task<OnMoveResponse> OnMove(MoveRequest request, int playerId)
        {
            Board board = FenUtility.LoadBoardFromFen(request.FenBeforeMove);

            StringBuilder moveNotation = new();
            // Append piece symbol
            moveNotation.Append(SquaresHelper.GetPieceSymbolFromSquare(board, request.StartSquare));

            // Make move (change board bitboards)
            board.MakeMove(request.StartSquare, request.TargetSquare, ref board);

            // Find current game in db
            GameInfo? game = _db.Games.FirstOrDefault(g => g.FirstPlayerId == playerId || g.SecondPlayerId == playerId && g.IsActiveGame);

            // Create move notation
            // If it capture move append 'x'
            if (SquaresHelper.IsPieceOnSquare(board, request.TargetSquare))
                moveNotation.Append('x');

            // Append target square
            if (SquaresHelper.SquareIndexToStringSquare.TryGetValue(request.TargetSquare, out var value))
                moveNotation.Append(value);
            else
                moveNotation.Append("[unknown_square]");

            // Append '+' if king under attack
            if (KingMovement.IsKingUnderAttack(board))
            {
                moveNotation.Append('+');
            }

            // Generate fen from updated board
            string fenAfterMove = FenUtility.GenerateFenFromBoard(board);

            if (game != null) // update game info
            {
                game.Fens.Add(fenAfterMove);
                game.Moves.Add(moveNotation.ToString());
                await _db.SaveChangesAsync();

                List<string> moveNotations = game.Moves;
                var response = new OnMoveResponse(fenAfterMove, moveNotations);

                return response;
            }
            // ? Maybe change this response
            return new OnMoveResponse(null, null);
        }
    }
}