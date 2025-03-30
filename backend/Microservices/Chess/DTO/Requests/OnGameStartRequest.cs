namespace Chess.DTO.Requests
{
    public record OnGameStartRequest
    {
        public required int FirstPlayerId { get; init; }
        public int SecondePlayerId { get; init; } = 0;
    }
}