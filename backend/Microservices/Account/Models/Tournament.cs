namespace Account.Models
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Prize { get; set; }
        public Player? Winner { get; set; }
        public required List<Player> Players { get; set; }
    }
}