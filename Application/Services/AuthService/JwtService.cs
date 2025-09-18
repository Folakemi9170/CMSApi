//using CMSApi.Application.Interfaces;
//using CMSApi.Domain.Entities;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace CMSApi.Application.Services
//{
//    public class JwtService : IJwtService
//    {
//        private readonly IConfiguration _configuration;

//        public JwtService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public string GenerateToken(Employee employee)
//        {
//            var jwtSettings = _configuration.GetSection("JwtSettings");
//            var secretKey = jwtSettings["SecretKey"];
//            var issuer = jwtSettings["Issuer"];
//            var audience = jwtSettings["Audience"];
//            var expiryInMinutes = int.Parse(jwtSettings["ExpiryInMinutes"]);

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
//                new Claim(JwtRegisteredClaimNames.Email, employee.Email),
//                new Claim(ClaimTypes.Role, employee.Role ?? "User"),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };
//            var token = new JwtSecurityToken(
//                issuer: issuer,
//                audience: audience,
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
//                signingCredentials: credentials
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        public string GenerateRefreshToken()
//        {
//            var randomNumber = new byte[32];
//            using (var rng = RandomNumberGenerator.Create())
//            {
//                rng.GetBytes(randomNumber);
//                return Convert.ToBase64String(randomNumber);
//            }
//        }

//    }

//        //public ClaimsPrincipal ValidateToken(string token)
//        //{
//        //    // ... validation logic here ...
//        //}

//}

