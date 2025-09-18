//using AutoMapper;
//using CMSApi.Application.DTO;
//using CMSApi.Application.DTO.AuthDto;
//using CMSApi.Application.DTO.EmployeeDto;
//using CMSApi.Application.Interfaces;
//using CMSApi.Domain.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq.Expressions;
//using System.Security.Claims;
//using System.Text;


//namespace CMSApi.Application.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly UserManager<AppUser> _userManager;
//        private readonly IConfiguration _configuration;

//        public AuthService(
//            Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager,
//            IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _configuration = configuration;
//        }

//        public async Task<IResult> SignUp(RegisterModel userRegistrationModel)
//        {
//            var user = new AppUser()
//            {
//                UserName = userRegistrationModel.Email,
//                Email = userRegistrationModel.Email,
//                Fullname = userRegistrationModel.Fullname,
//            };
//            var result = await _userManager.CreateAsync(user, userRegistrationModel.Password);

//            if (result.Succeeded)
//                return Results.Ok(new { message = "User registered successfully" });

//            return Results.BadRequest(new
//            {
//                message = "Registration failed",
//                errors = result.Errors.Select(e => e.Description)
//            });
//        }

//        public async Task<IResult> SignIn(LoginModel loginModel)
//        {

//            var user = await _userManager.FindByEmailAsync(loginModel.Email);
//            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
//            {
//                var signInKey = new SymmetricSecurityKey(
//                    Encoding.UTF8.GetBytes(
//                        _configuration["JwtSettings:SecretKey"]!));
//                var tokenDescriptor = new SecurityTokenDescriptor
//                {
//                    Subject = new ClaimsIdentity(new Claim[]
//                        {
//                            new Claim("UserId", user.Id.ToString())
//                        }),
//                    Expires = DateTime.UtcNow.AddMinutes(10),
//                    SigningCredentials = new SigningCredentials(signInKey,
//                        SecurityAlgorithms.HmacSha256Signature
//                        )
//                };
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
//                var token = tokenHandler.WriteToken(securityToken);
//                return Results.Ok(new { token });
//            }
//            else
//            {
//                return Results.BadRequest(new
//                {
//                    message = "Username or password is incorrect."
//                });
//            }
//        }
//    }
//}
