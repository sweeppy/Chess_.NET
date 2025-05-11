namespace Account.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
        public required string UserId { get; set; }
        public bool IsActive => DateTime.UtcNow <= Expires;

    }
}