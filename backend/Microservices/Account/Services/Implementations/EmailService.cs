using Account.DTO.VerificationCodeRequests;
using Account.Services.Interfaces;
using MimeKit;

namespace Account.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "notificationwebsite14@gmail.com";

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(SendEmailRequest request)
        {
            string SenderEmailPassword = _configuration["SenderPassword"];

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("PixelChess", _smtpUsername));
            email.To.Add(new MailboxAddress("", request.ToEmail));
            email.Subject = request.Subject;

            email.Body = new TextPart("plain")
            {
                Text = request.Body
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, false);
                await client.AuthenticateAsync(_smtpUsername, SenderEmailPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }
    }
}