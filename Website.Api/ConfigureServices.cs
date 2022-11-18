using MediatR;
using System.Reflection;
using Website.Infrastructure.Services.PageService.Classes;

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
                    options.JsonSerializerOptions.Converters.Add(new WebsiteWidgetJsonConverter());
                });

            return services;
        }
    }
}
