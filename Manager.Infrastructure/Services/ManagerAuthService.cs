using Manager.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Services;

namespace Manager.Infrastructure.Services
{
    public sealed class ManagerAuthService : AuthService, IAuthService
    {
        public ManagerAuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
        }
    }
}