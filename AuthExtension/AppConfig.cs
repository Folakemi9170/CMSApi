using CMSApi.Application.DTO.AuthDto;
using CMSApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMSApi.AuthExtension
{
    public static class AppConfig
    {
        public static WebApplication ConfigureCORS(
            this WebApplication app,
            IConfiguration configuration)
        {
            app.UseCors();
            return app;
        }

        public static IServiceCollection AddAppConfig(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            return services;
        }
    }
}
