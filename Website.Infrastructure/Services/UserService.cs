using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ClaimsPrincipal _user;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _user = httpContextAccessor!.HttpContext!.User;
        }



        // -------------------------------------------------------------------- Add Password Async ----------------------------------------------------------------------
        public async Task<IdentityResult> AddPasswordAsync(User user, string password)
        {
            return await _userManager.AddPasswordAsync(user, password);
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

            user.AddDomainEvent(new UserCreatedEvent(user));

            return user;
        }



        // ------------------------------------------------------------ Generate Delete Account Token Async ------------------------------------------------------------
        public async Task<string> GenerateDeleteAccountTokenAsync(User user)
        {
            return await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, "Delete Account");
        }







        // ---------------------------------------------------------- Generate Email Confirmation Token Async ------------------------------------------------------------
        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }





        // --------------------------------------------------------- Get External Log In Prodvider From Claims -----------------------------------------------------------
        public string GetExternalLogInProviderFromClaims()
        {
            return _user.FindFirstValue("externalLoginProvider");
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




        // ----------------------------------------------------------------- Get User From Claims Async ------------------------------------------------------------------
        public async Task<User> GetUserFromClaimsAsync()
        {
            return await _userManager.GetUserAsync(_user);
        }






        // ------------------------------------------------------------------ Get User Id From Claims --------------------------------------------------------------------
        public string GetUserIdFromClaims()
        {
            return _user.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }






        // -------------------------------------------------------------------- Has Password Async ------------------------------------------------------------------------
        public async Task<bool> HasPasswordAsync(User user)
        {
            return await _userManager.HasPasswordAsync(user);
        }






        
        // ------------------------------------------------------------ Verify Delete Account Token Async -----------------------------------------------------------------
        public async Task<bool> VerifyDeleteAccountTokenAsync(User user, string token)
        {
            return await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, "Delete Account", token);
        }
    }
}