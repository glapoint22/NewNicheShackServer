using Manager.Application.Common.Interfaces;
using Manager.Infrastructure.Services.PageService.Classes;
using Microsoft.Extensions.Configuration;
using Shared.Services;

namespace Manager.Infrastructure.Services
{
    public sealed class ManagerEmailService : EmailService, IEmailService
    {
        public ManagerEmailService(IConfiguration configuration, IManagerDbContext dbContext)
        {
            _configuration = configuration;
            _emailBuilder = new(new Repository(dbContext));
        }
    }
}