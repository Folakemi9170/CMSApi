using System.ComponentModel.DataAnnotations;

namespace CMSApi.Application.DTO.AuthDto
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
