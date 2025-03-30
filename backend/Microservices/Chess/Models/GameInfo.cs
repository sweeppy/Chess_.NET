namespace Chess.Models
{
    public class GameInfo
    {
        public int Id { get; set; }
        public required ICollection<string> Fens { get; set; }
        public required ICollection<string> Moves { get; set; }
        public bool IsActiveGame { get; set; } = true;

        public int FirstPlayerId { get; set; }
        public int SecondPlayerId { get; set; }
    }
}