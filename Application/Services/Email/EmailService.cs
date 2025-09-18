using CMSApi.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace CMSApi.Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;


        public EmailService(IConfiguration configuration) //IMapper mapper)
        {
            _configuration = configuration;
            //_mapper = mapper;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Smtp:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };
            
            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration["Smtp:Host"],
                int.Parse(_configuration["Smtp:Port"]),
                SecureSocketOptions.StartTls
            );
            //var username = _configuration["Smtp:Username"];
            //var password = _configuration["Smtp:Password"];

            //if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            //{
            //    throw new InvalidOperationException("SMTP credentials are missing in configuration.");
            //}

            await smtp.AuthenticateAsync( //username, password);
            _configuration["Smtp:Username"],
            _configuration["Smtp:Password"]
            );

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }


        public async Task SendEmailsAsync(List<string> toEmails, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Smtp:From"]));

            foreach (var toEmail in toEmails)
            {
                email.Bcc.Add(MailboxAddress.Parse(toEmail));
            }

            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration["Smtp:Host"],
                int.Parse(_configuration["Smtp:Port"]),
                SecureSocketOptions.StartTls
            );
            
            await smtp.AuthenticateAsync(
            _configuration["Smtp:Username"],
            _configuration["Smtp:Password"]
            );

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
