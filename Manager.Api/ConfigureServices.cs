using MediatR;
using System.Reflection;

namespace Manager.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection ApiServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddControllers();

            return services;
        }
    }
}