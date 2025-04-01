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
using Microsoft.EntityFrameworkCore;


namespace Chess.API.Implementations
{
    public class MovementAPI : IMovement
    {
        private readonly GamesDbContext _db;

        public MovementAPI(GamesDbContext db)
        {
            _db = db;
        }

        public Dictionary<int, List<int>> GetLegalMoves(string fen)
        {
            Dictionary<int, List<int>> legalMoves = [];

            Board board = FenUtility.LoadBoardFromFen(fen);

            Dictionary<char, ulong> piecesCollections = GetActivePieces(board);

            for (int square = 0; square < 64; square++)
            {
                foreach (ulong pieces in piecesCollections.Values)
                {
                    if ((pieces & (1UL << square)) != 0)
                    {
                        char pieceSymbol = piecesCollections.FirstOrDefault(x => (x.Value & (1UL << square)) != 0).Key;

                        ulong legalMovesForSquare = GetMovesByPieceSymbol(square, pieceSymbol, board);
                        
                        legalMoves.Add(square, BitHelper.SquareIndexesFromBitboard(legalMovesForSquare));
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
                'N' => KnightMovement.Generate(squareIndex, board),
                'B' => BishopMovement.Generate(squareIndex, board),
                'R' => RookMovement.Generate(squareIndex, board),
                'Q' => QueenMovement.Generate(squareIndex, board),
                'K' => KingMovement.Generate(squareIndex, board),
                'p' => PawnMovement.BlackGenerate(squareIndex, board),
                'n' => KnightMovement.Generate(squareIndex, board),
                'b' => BishopMovement.Generate(squareIndex, board),
                'r' => RookMovement.Generate(squareIndex, board),
                'q' => QueenMovement.Generate(squareIndex, board),
                'k' => KingMovement.Generate(squareIndex, board),
                _ => 0,
            };
        }

        public async Task<string> OnMove(MoveRequest request)
        {
            Board board = FenUtility.LoadBoardFromFen(request.FenBeforeMove);


            StringBuilder stringMove = new();
            stringMove.Append(SquaresHelper.GetPieceSymbolFromSquare(board, request.StartSquare));
            stringMove.Append(SquaresHelper.squareIndexToStringSquare.TryGetValue(request.TargetSquare, out var value));

            board.MakeMove(request.StartSquare, request.TargetSquare, board, request.IsCastleMove, request.IsKingCastle);
            
            string fenAfterMove = FenUtility.GenerateFenFromBoard(board);

            GameInfo? game = await _db.Games.FirstOrDefaultAsync(g => g.Id == request.GameId);



            if(game != null)
            {
                game.Fens.Add(fenAfterMove);
                game.Moves.Add(stringMove.ToString());
                await _db.SaveChangesAsync();
            }

            return fenAfterMove;
        }
    }
}