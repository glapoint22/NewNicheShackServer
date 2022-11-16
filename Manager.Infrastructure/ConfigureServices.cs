using Manager.Domain.Entities;
using Manager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Manager.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ManagerDbContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DBConnection"));
            });


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

            return services;
        }
    }
}