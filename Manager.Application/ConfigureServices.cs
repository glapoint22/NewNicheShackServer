using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Persistence;

namespace Manager.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddDbContext<WebsiteDbContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WebsiteDBConnection"));
            });

            services.AddScoped<IWebsiteDbContext>(provider => provider.GetRequiredService<WebsiteDbContext>());

            return services;
        }
    }
}