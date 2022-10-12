using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        private readonly HttpContext _httpContext;
        private readonly IConfiguration _configuration;

        public CookieService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContext = httpContextAccessor.HttpContext!;
            _configuration = configuration;
        }

        // ----------------------------------------------------------------------- Delete Cookie -----------------------------------------------------------------------
        public void DeleteCookie(string cookie)
        {
            _httpContext.Response.Cookies.Delete(cookie);
        }





        // ------------------------------------------------------------------------- Get Cookie ------------------------------------------------------------------------
        public string? GetCookie(string cookie)
        {
            return _httpContext.Request.Cookies[cookie];
        }

        




        // ------------------------------------------------------------------------- Set Cookie ------------------------------------------------------------------------
        public void SetCookie(string cookie, string value, DateTimeOffset? expiration)
        {
            CookieOptions cookieOptions = new();

            if (expiration != null)
            {
                cookieOptions = new()
                {
                    Expires = expiration
                };
            }

            _httpContext.Response.Cookies.Append(cookie, value, cookieOptions);
        }
    }
}