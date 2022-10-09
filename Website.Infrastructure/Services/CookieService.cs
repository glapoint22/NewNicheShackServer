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
        public void SetCookie(string cookie, string value, bool isPersistent)
        {
            CookieOptions cookieOptions = new();

            if (isPersistent)
            {
                cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(Convert.ToInt32(_configuration["TokenValidation:RefreshExpiresInDays"]))
                };
            }

            _httpContext.Response.Cookies.Append(cookie, value, cookieOptions);
        }
    }
}