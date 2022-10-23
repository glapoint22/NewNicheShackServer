using Microsoft.Extensions.DependencyInjection;
using Shared.QueryService.Classes;

namespace Shared
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<Website.Application.Common.Interfaces.IQueryService, WebsiteQueryService>();
            services.AddTransient<Manager.Application.Common.Interfaces.IQueryService, ManagerQueryService>();

            return services;
        }
    }
}