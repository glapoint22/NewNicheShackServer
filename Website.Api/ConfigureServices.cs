using MediatR;
using Shared.Common.Classes;
using System.Reflection;

namespace Website.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection ApiServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new WidgetJsonConverter());
                });

            return services;
        }
    }
}
