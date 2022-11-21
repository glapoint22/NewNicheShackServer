using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Services;
using System.Security.Claims;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Services
{
    public sealed class WebsiteAuthService : AuthService, IAuthService
    {
        public WebsiteAuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor) { }


        // -------------------------------------------------------------------------- Get Claims -----------------------------------------------------------------------
        public List<Claim> GenerateClaims(string userId, string role, string provider, bool hasPassword)
        {
            List<Claim> claims = GenerateClaims(userId, role, true);
            claims.Add(new Claim("externalLoginProvider", provider));
            claims.Add(new Claim("hasPassword", hasPassword.ToString()));

            return claims;
        }





        // --------------------------------------------------------- Get External Log In Prodvider From Claims -----------------------------------------------------------
        public string GetExternalLogInProviderFromClaims()
        {
            return _claimsPrincipal.FindFirstValue("externalLoginProvider");
        }
    }
}