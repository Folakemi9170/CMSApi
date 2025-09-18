using CMSApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMSApi.AuthExtension
{
    public static class EFCore
    {
        public static IServiceCollection InjectDbContext(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<CMSDbContext > (options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }


    }
}
