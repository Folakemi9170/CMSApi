using CMSApi.Application.DTO.AuthDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CMSApi.Presentation.Controllers.Auth
{
    public static class AccountEndpoint
    {
        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/UserProfile", GetUserProfile); //.RequireAuthorization();
            return app;
        }
        [Authorize]
        private static async Task<IResult> GetUserProfile(ClaimsPrincipal user, UserManager<AppUser> userManager)
        {
            string userID = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userManager.FindByIdAsync(userID);
            return Results.Ok(
                new
                {
                    userDetails?.Email,
                    userDetails?.Fullname
                });
        }
    }
}
