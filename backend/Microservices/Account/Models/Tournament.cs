namespace Account.Models
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Prize { get; set; }
        public Player Winner { get; set; }
        public List<Player> Players { get; set; }
    }
}