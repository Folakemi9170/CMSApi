namespace CMSApi.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task SendEmailsAsync(List<string> toEmails, string subject, string message);

        //Task SendConfirmationEmailAsync(string toEmail, string confirmationLink);

        //Task SendPasswordResetEmailAsync(string toEmail, string resetLink);

        //Task SendNotificationEmailAsync(string toEmail, string subject, string message);

    }
}
