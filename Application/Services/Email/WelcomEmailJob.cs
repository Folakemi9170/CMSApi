//using CMSApi.Application.Interfaces;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Identity.UI.Services;

//namespace CMSApi.Application.Services.Email
//{
//    public class WelcomeEmailJob
//    {
//        private readonly IEmailService _emailService;

//        public WelcomeEmailJob(IEmailSender emailSender)
//        {
//            _emailService = emailService;
//        }

//        public async Task Run(string userEmail)
//        {
//            await _emailService.SendEmailAsync(
//                userEmail,
//                "Welcome to CMSApi 🎉",
//                "<p>We’re glad to have you on board!</p>");
//        }
//    }
//}
