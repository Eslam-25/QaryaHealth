using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QaryaHealth.Core.IRepositories;
using QaryaHealth.Infrastructure;
using QaryaHealth.Infrastructure.Repositories;
using QaryaHealth.Service.Implementations;
using QaryaHealth.Service.Interfaces;

namespace QaryaHealth.API.Helpers
{
    public static class StartupHelper
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        public static void RegisterRepositories(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<QaryaHealthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default")));
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}
