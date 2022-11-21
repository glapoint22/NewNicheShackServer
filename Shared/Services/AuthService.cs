using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Shared.Services
{
    public abstract class AuthService
    {
        protected readonly IConfiguration _configuration;
        protected readonly HttpContext _httpContext;
        protected readonly ClaimsPrincipal _claimsPrincipal;

        public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext!;
            _claimsPrincipal = _httpContext.User;
        }



        // -------------------------------------------------------------------- Generate Access Token ------------------------------------------------------------------
        public string GenerateAccessToken(List<Claim> claims)
        {
            JwtSecurityToken accessToken = new(
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["TokenValidation:AccessExpiresInMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenValidation:SigningKey"])),
                    SecurityAlgorithms.HmacSha256),
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }







        // ----------------------------------------------------------------- Get Access Token From Header --------------------------------------------------------------------------
        public string? GetAccessTokenFromHeader()
        {
            _httpContext.Request.Headers.TryGetValue("Authorization", out StringValues value);

            if (value.Count == 0) return null;

            Match result = Regex.Match(value, @"(?:Bearer\s)(.+)");
            return result.Groups[1].Value;
        }




        // -------------------------------------------------------------------------- Get Claims -----------------------------------------------------------------------
        public List<Claim> GenerateClaims(string userId, string role, bool isPersistent)
        {
            List<Claim> claims = new()
            {
                new Claim("AccountAccess", role),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenValidation:Site"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["TokenValidation:Site"]),
            };


            if (isPersistent)
            {
                string expiration = DateTimeOffset.UtcNow.AddDays(Convert.ToInt32(_configuration["TokenValidation:RefreshExpiresInDays"])).ToString();
                claims.Add(new Claim(ClaimTypes.Expiration, expiration));
            }

            return claims;
        }






        // ------------------------------------------------------------------- Get Principal From Token -----------------------------------------------------------------
        public ClaimsPrincipal? GetPrincipalFromToken(string accessToken)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _configuration["TokenValidation:Site"],
                ValidIssuer = _configuration["TokenValidation:Site"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenValidation:SigningKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal;

            try
            {
                principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
            }
            catch (Exception)
            {
                return null;
            }


            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }





        // ---------------------------------------------------------------- Get Expiration From Claims --------------------------------------------------------------
        public DateTimeOffset? GetExpirationFromClaims()
        {
            Claim? expiration = _claimsPrincipal.FindFirst(ClaimTypes.Expiration);

            if (expiration != null)
            {
                return DateTimeOffset.Parse(expiration.Value);
            }

            return null;
        }




        // ---------------------------------------------------------------- Get Expiration From Claims --------------------------------------------------------------
        public DateTimeOffset? GetExpirationFromClaims(List<Claim> claims)
        {
            Claim? expirationClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration);

            if (expirationClaim == null) return null;
            return DateTimeOffset.Parse(expirationClaim.Value);
        }
    }
}
