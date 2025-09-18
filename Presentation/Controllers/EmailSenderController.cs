using CMSApi.Application.DTO.EmailDt;
using CMSApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailSenderController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [HttpPost("send")]
        public async Task<ActionResult> SendEmail([FromBody] SendEmailRequest request)
        {

            if (!string.IsNullOrEmpty(request.ToEmail))
            {
                // Send to one person
                await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Message);
                return Ok("Email sent to one recipient.");
            }
            else if (request.ToEmails != null && request.ToEmails.Any())
            {
                // Send to multiple people
                await _emailService.SendEmailsAsync(request.ToEmails, request.Subject, request.Message);
                return Ok("Email sent to multiple recipients.");
            }

            return BadRequest("Please provide at least one recipient.");
        }
    }
}
