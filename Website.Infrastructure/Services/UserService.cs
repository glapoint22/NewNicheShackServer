using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ClaimsPrincipal _user;

        public UserService(UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _user = httpContextAccessor!.HttpContext!.User;
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
        public async Task<User> CreateUserAsync(string firstName, string lastName, string email, string? password = null)
        {
            IdentityResult result;

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email
            };

            if (password != null)
            {
                result = await _userManager.CreateAsync(user, password);
            }
            else
            {
                user.EmailConfirmed = true;
                result = await _userManager.CreateAsync(user);
            }


            if (!result.Succeeded) return null!;

            return user;
        }




        // ---------------------------------------------------------- Generate Email Confirmation Token Async ------------------------------------------------------------
        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }



        
        // ----------------------------------------------------------------- Get User By Email Async ---------------------------------------------------------------------
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }





        // ------------------------------------------------------------------ Get User By Id Async ---------------------------------------------------------------------
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }







        // ---------------------------------------------------------------------- Get User Data --------------------------------------------------------------------------
        public string GetUserData(User user)
        {
            return user.FirstName + "," + user.LastName + "," + user.Email + "," + user.Image;
        }




        // ---------------------------------------------------------------------- Get User Data --------------------------------------------------------------------------
        public string GetUserData(User user, string provider, bool hasPassword)
        {
            string userData = GetUserData(user);
            userData += "," + provider + "," + hasPassword;
            return userData;
        }






        // ------------------------------------------------------------------------ Get User Id --------------------------------------------------------------------------
        public string? GetUserId()
        {
            return _user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }






        // --------------------------------------------------------------------- Has Password Async ----------------------------------------------------------------------
        public async Task<bool> HasPasswordAsync(User user)
        {
            return await _userManager.HasPasswordAsync(user);
        }
    }
}