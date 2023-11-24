using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustructure.Data;

namespace SchoolProject.Infrustructure
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConStr"));
            });

            return services;
        }
    }
}