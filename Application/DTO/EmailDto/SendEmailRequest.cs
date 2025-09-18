using System.ComponentModel.DataAnnotations;

namespace CMSApi.Application.DTO.EmailDt
{
    public class SendEmailRequest
    {
        public string ToEmail { get; set; }

        public List<string> ToEmails { get; set; } = new();

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}