using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Infrastructure.BackgroundJobs;
using Manager.Infrastructure.Persistence;
using Manager.Infrastructure.Services;
using Manager.Infrastructure.Services.PageService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Shared.Common.Interceptors;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Manager.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DomainEventsInterceptor>();

            services.AddDbContext<ManagerDbContext>((sp, options) =>
            {
                var interceptor = sp.GetService<DomainEventsInterceptor>();
                options.UseSqlServer(configuration.GetConnectionString("DBConnection"))
                    .AddInterceptors(interceptor!);
            });



            services.AddQuartz(configure =>
            {
                JobKey processDomainEventsJobKey = new(nameof(ProcessDomainEventsJob));

                configure
                    .AddJob<ProcessDomainEventsJob>(processDomainEventsJobKey)
                    .AddTrigger(trigger => trigger.ForJob(processDomainEventsJobKey)
                        .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10)
                            .RepeatForever()));


                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();





            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ManagerDbContext>();




            // Add authentication using JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Set what we will be validating in the token
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["TokenValidation:Site"],
                    ValidIssuer = configuration["TokenValidation:Site"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenValidation:SigningKey"]))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Account", policy =>
                {
                    policy.RequireClaim("AccountAccess", "admin");
                    policy.RequireClaim(JwtRegisteredClaimNames.Iss, configuration["TokenValidation:Site"]);
                    policy.RequireClaim(JwtRegisteredClaimNames.Aud, configuration["TokenValidation:Site"]);
                });
            });


            services.AddScoped<IManagerDbContext>(provider => provider.GetRequiredService<ManagerDbContext>());
            services.AddTransient<ICookieService, ManagerCookieService>();
            services.AddTransient<IAuthService, ManagerAuthService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IMediaService, ManagerMediaService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IEmailService, ManagerEmailService>();

            return services;
        }
    }
}