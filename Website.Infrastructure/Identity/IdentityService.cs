using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IWebsiteDbContext _context;
        private readonly HttpContext _httpContext;

        public IdentityService(UserManager<User> userManager, IConfiguration configuration, IWebsiteDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _httpContext = httpContextAccessor.HttpContext!;
        }




        // ------------------------------------------------------------------- Check Password Async ---------------------------------------------------------------------
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }




        // ------------------------------------------------------------------- Confirm Email Async ---------------------------------------------------------------------
        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }




        // --------------------------------------------------------------------- Create User Async ---------------------------------------------------------------------
        public async Task<User> CreateUserAsync(string firstName, string lastName, string email, string password)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) return null!;

            return user;
        }



        // -------------------------------------------------------------------- Generate Access Token ------------------------------------------------------------------
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            JwtSecurityToken accessToken = new(
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["TokenValidation:AccessExpiresInMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenValidation:SigningKey"])),
                    SecurityAlgorithms.HmacSha256),
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }




        // ----------------------------------------------------------- Generate Email Confirmation Token Async ---------------------------------------------------------
        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }






        // ---------------------------------------------------------------- Generate Refresh Token Async ---------------------------------------------------------------
        private async Task<string> GenerateRefreshTokenAsync(string userId)
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

            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync();

            return refreshToken.Id;
        }






        // -------------------------------------------------------------------- Generate Tokens Async ------------------------------------------------------------------
        private async Task<Tuple<string, string>> GenerateTokensAsync(User user, List<Claim> claims)
        {
            string accessToken = GenerateAccessToken(claims);
            string refreshToken = await GenerateRefreshTokenAsync(user.Id);

            return new Tuple<string, string>(accessToken, refreshToken);
        }





        // -------------------------------------------------------------------------- Get Claims -----------------------------------------------------------------------
        private List<Claim> GetClaims(User user, bool isPersistent)
        {
            List<Claim> claims = new()
            {
                new Claim("AccountAccess", "user"),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenValidation:Site"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["TokenValidation:Site"]),
                new Claim(ClaimTypes.IsPersistent, isPersistent.ToString())
            };

            return claims;
        }





        
        // ----------------------------------------------------------------- Get User By Email Async ---------------------------------------------------------------------
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }








        // ---------------------------------------------------------------------- Get User Data --------------------------------------------------------------------------
        private string GetUserData(User user)
        {
            return user.FirstName + "," + user.LastName + "," + user.Email + "," + user.Image;
        }







        // -------------------------------------------------------------------------- Log In -----------------------------------------------------------------------------
        public async Task LogInAsync(User user, bool isPersistent)
        {
            List<Claim> claims = GetClaims(user, isPersistent);
            Tuple<string, string> tokens = await GenerateTokensAsync(user, claims);
            SetCookies(tokens, user, isPersistent);
        }




        // ------------------------------------------------------------------------- Set Cookies -----------------------------------------------------------------------------
        private void SetCookies(Tuple<string, string> tokens, User user, bool isPersistent)
        {
            CookieOptions cookieOptions = new CookieOptions();

            if (isPersistent)
            {
                cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(Convert.ToInt32(_configuration["TokenValidation:RefreshExpiresInDays"]))
                };
            }

            _httpContext.Response.Cookies.Append("access", tokens.Item1, cookieOptions);
            _httpContext.Response.Cookies.Append("refresh", tokens.Item2, cookieOptions);
            _httpContext.Response.Cookies.Append("user", GetUserData(user), cookieOptions);
        }
    }
}