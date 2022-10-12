using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Infrastructure.Persistence;
using Website.Infrastructure.Services;

namespace Website.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebsiteDbContext>((options) =>
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
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddEntityFrameworkStores<WebsiteDbContext>()
            .AddDefaultTokenProviders();


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
                    policy.RequireClaim("AccountAccess", "user", "admin");
                    policy.RequireClaim(JwtRegisteredClaimNames.Iss, configuration["TokenValidation:Site"]);
                    policy.RequireClaim(JwtRegisteredClaimNames.Aud, configuration["TokenValidation:Site"]);
                });
            });


            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddScoped<IWebsiteDbContext>(provider => provider.GetRequiredService<WebsiteDbContext>());
            services.AddHttpContextAccessor();


            return services;
        }
    }
}
