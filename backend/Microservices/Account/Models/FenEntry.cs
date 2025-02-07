using System.ComponentModel.DataAnnotations;

namespace Account.Models
{
    public class FenEntry
    {
        public int Id { get; set; }
        [MaxLength(89)]
        public string Fen { get; set; }

        public int GameInfoId { get; set; }
        public GameInfo GameInfo { get; set; }
    }
}