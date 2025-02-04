namespace Chess.GeneralModels
{
    public class GameInfo
    {
        public int Id { get; set; }
        public ICollection<FenEntry> Fens { get; set; }
        public List<string> moves { get; set; }
    }
}