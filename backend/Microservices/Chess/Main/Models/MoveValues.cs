namespace Chess.Main.Models
{
    public class MoveValues(int startSquare, int targetSquare)
    {
        public int StartSquare { get; set; } = startSquare;
        public int TargetSquare { get; set; } = targetSquare;
    }
}