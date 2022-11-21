using Microsoft.AspNetCore.Http;
using Shared.Services;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Services
{
    public sealed class WebsiteCookieService : CookieService, ICookieService
    {
        public WebsiteCookieService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}