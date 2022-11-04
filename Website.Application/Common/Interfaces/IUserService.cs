using Microsoft.AspNetCore.Identity;

using System.Security.Claims;
using Shared.Common.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> AddPasswordAsync(User user, string password);

        Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<User?> CreateUserAsync(string firstName, string lastName, string email, string? password = null);

        Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);

        Task<string> GenerateDeleteAccountTokenAsync(User user);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        DateTimeOffset? GetExpirationFromClaims();

        DateTimeOffset? GetExpirationFromClaims(List<Claim> claims);

        string? GetExternalLogInProviderFromClaims();

        Task<User> GetUserFromClaimsAsync();

        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByIdAsync(string id);

        string GetUserData(User user);

        string GetUserData(User user, string provider, bool hasPassword);

        string GetUserIdFromClaims();

        Task<bool> HasPasswordAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

        Task<IdentityResult> UpdateAsync(User user);

        Task<bool> VerifyDeleteAccountTokenAsync(User user, string token);
    }
}