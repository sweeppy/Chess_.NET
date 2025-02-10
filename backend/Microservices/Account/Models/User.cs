namespace Account.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? ImagePath { get; set; }

        public required string VerifyCode { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
    }
}