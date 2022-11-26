using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Services
{
    public sealed class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ClaimsPrincipal? _user;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            HttpContext? httpContext = httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                _user = httpContext.User;
            }
        }



        // -------------------------------------------------------------------- Add Password Async ----------------------------------------------------------------------
        public async Task<IdentityResult> AddPasswordAsync(User user, string password)
        {
            return await _userManager.AddPasswordAsync(user, password);
        }





        // -------------------------------------------------------------------- Change Email Async ----------------------------------------------------------------------
        public Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
        {
            return _userManager.ChangeEmailAsync(user, newEmail, token);
        }





        // ------------------------------------------------------------------- Change Password Async --------------------------------------------------------------------
        public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
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
        public async Task<User?> CreateUserAsync(string firstName, string lastName, string email, string? password = null)
        {
            User user;
            IdentityResult result;

            user = User.Create(firstName, lastName, email.ToLower());

            if (password != null)
            {
                result = await _userManager.CreateAsync(user, password);
            }
            else
            {
                result = await _userManager.CreateAsync(user);
            }

            if (!result.Succeeded) return null;

            return user;
        }







        // ------------------------------------------------------------- Generate Change Email Token Async ------------------------------------------------------------
        public async Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
        {
            return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
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




        // ------------------------------------------------------------ Generate Password Reset Token Async --------------------------------------------------------------
        public Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
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





        // -------------------------------------------------------------------- Has Password Async ------------------------------------------------------------------------
        public async Task<bool> HasPasswordAsync(User user)
        {
            return await _userManager.HasPasswordAsync(user);
        }



        // ------------------------------------------------------------------- Reset Password Async ------------------------------------------------------------------------
        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }






        // ----------------------------------------------------------------------- Update Async -----------------------------------------------------------------------------
        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }







        // ------------------------------------------------------------ Verify Delete Account Token Async -----------------------------------------------------------------
        public async Task<bool> VerifyDeleteAccountTokenAsync(User user, string token)
        {
            return await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, "Delete Account", token);
        }
    }
}