using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

namespace Website.Infrastructure.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly IWebsiteDbContext _dbContext;

        public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebsiteDbContext dbContext)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext!;
            _dbContext = dbContext;
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



        // -------------------------------------------------------------------- Generate Refresh Token -----------------------------------------------------------------
        public string GenerateRefreshToken(string userId)
        {
            var randomNumber = new byte[32];
            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            RefreshToken refreshToken = new()
            {
                Id = Convert.ToBase64String(randomNumber),
                UserId = userId,
                Expiration = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["TokenValidation:RefreshExpiresInDays"]))
            };

            _dbContext.RefreshTokens.Add(refreshToken);

            return refreshToken.Id;
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
        public List<Claim> GetClaims(User user, bool isPersistent)
        {
            List<Claim> claims = new()
            {
                new Claim("AccountAccess", "user"),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
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





        // -------------------------------------------------------------------------- Get Claims -----------------------------------------------------------------------
        public List<Claim> GetClaims(User user, string provider, bool hasPassword)
        {
            List<Claim> claims = GetClaims(user, true);
            claims.Add(new Claim("externalLoginProvider", provider));
            claims.Add(new Claim("hasPassword", hasPassword.ToString()));

            return claims;
        }




        // ------------------------------------------------------------------- Get Order Notification Key --------------------------------------------------------------
        public string GetOrderNotificationKey()
        {
            return _configuration["OrderNotification:Key"];
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
    }
}