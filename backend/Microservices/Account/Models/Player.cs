namespace Account.Models
{
    public class Player : User
    {
        public int Elo { get; set; }
        public List<GameInfo> Games { get; set; }
        public List<Tournament> Tournaments { get; set; }
        public List<Player> Friends { get; set; }
    }
}