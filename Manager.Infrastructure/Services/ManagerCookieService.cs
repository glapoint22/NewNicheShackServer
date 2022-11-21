using Manager.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Services;

namespace Manager.Infrastructure.Services
{
    public sealed class ManagerCookieService : CookieService, ICookieService
    {
        public ManagerCookieService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}