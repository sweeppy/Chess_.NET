namespace Chess.DTO.Requests
{
    public record MoveRequest
    {
        public int GameId { get; init; }
        public int StartSquare { get; init; }
        public int TargetSquare { get; init; }
        public required string Fen { get; init; }
        public bool IsCastleMove { get; init; }
        public bool? IsKingCastle { get; init; }
    }
}