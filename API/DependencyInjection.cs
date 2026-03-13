using Contract.Repositories.Entity;
using Contract.Repositories.Interface;
using Contract.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.payOS;
using Repositories;
using Repositories.Base;
using Repositories.UOW;
using Services.Mappings;
using Services.Service;

namespace API
{
    public static class DependencyInjection
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigRoute();
            services.AddInfrastructure(configuration);
            services.AddServices();
            services.AddAutoMapper(typeof(MappingProfile));
            services.ConfigureServices();

        }
        public static void ConfigRoute(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
        }
        public static void AddPayment(this IServiceCollection services, IConfiguration configuration)
        {
            PayOS payOS = new PayOS(configuration["PayOS:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["PayOS:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["PayOS:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
            services.AddSingleton(payOS);
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEmailSender, EmailSender>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRoomService, RoomService>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            
            services.AddMemoryCache();
        }
    }
}
