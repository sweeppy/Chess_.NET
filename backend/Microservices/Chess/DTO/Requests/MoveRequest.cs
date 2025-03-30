namespace Chess.DTO.Requests
{
    public record MoveRequest
    {
        public required int GameId { get; init; }
        public required int StartSquare { get; init; }
        public required int TargetSquare { get; init; }
        public required string Fen { get; init; }
        public required bool IsCastleMove { get; init; }
        public required bool IsKingCastle { get; init; }
    }
}