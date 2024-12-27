namespace account_service.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string ImagePath { get; set; }

        public string VerifyCode { get; set; }
    }
}