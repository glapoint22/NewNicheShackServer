using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Infrastructure.Identity;
using Website.Infrastructure.Persistence;

namespace Website.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebsiteDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WebsiteDBConnection"));
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<WebsiteDbContext>()
            .AddDefaultTokenProviders();


            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IWebsiteDbContext>(provider => provider.GetRequiredService<WebsiteDbContext>());


            return services;
        }
    }
}
