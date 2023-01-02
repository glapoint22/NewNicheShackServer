using Microsoft.Extensions.Configuration;
using Shared.Services;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Services.Common;

namespace Website.Infrastructure.Services
{
    public sealed class WebsiteEmailService : EmailService, IEmailService
    {
        public WebsiteEmailService(IConfiguration configuration, IWebsiteDbContext dbContext)
        {
            _configuration = configuration;
            _emailBuilder = new(new Repository(dbContext));
        }
    }
}