namespace Account.Models
{
    public class GameInfo
    {
        public int Id { get; set; }
        public ICollection<FenEntry> Fens { get; set; }
        public List<string> Moves { get; set; }
        public bool IsActiveGame { get; set; } = true;

        public int FirstPlayerId { get; set; }
        public int SecondPlayerId { get; set; }
    }
}