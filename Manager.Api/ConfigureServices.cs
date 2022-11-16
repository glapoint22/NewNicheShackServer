namespace Manager.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection ApiServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
    }
}