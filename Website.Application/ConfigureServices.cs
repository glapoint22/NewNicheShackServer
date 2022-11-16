using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.PageBuilder.Classes;
using System.Reflection;
using Website.Application.Common.Behaviors;

namespace Website.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<PageBuilder>();
            services.AddScoped<GridData>();

            return services;
        }
    }
}
