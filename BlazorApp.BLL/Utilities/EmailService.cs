using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace BlazorApp.BLL.Utilities
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    _config["Email:Username"],
                    _config["Email:Password"]),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_config["Email:Username"] ?? throw new InvalidOperationException("Email username is not configured."), "Your App"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mail.To.Add(toEmail);
            await client.SendMailAsync(mail);
        }
    }
}