using Microsoft.AspNetCore.Http;

namespace Shared.Services
{
    public abstract class CookieService
    {
        protected readonly HttpContext _httpContext;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext!;
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