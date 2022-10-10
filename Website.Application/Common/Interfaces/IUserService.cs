using Microsoft.AspNetCore.Identity;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> AddPasswordAsync(User user, string password);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<User> CreateUserAsync(string firstName, string lastName, string email, string? password = null);

        Task<string> GenerateDeleteAccountTokenAsync(User user);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        string GetExternalLogInProviderFromClaims();

        Task<User> GetUserFromClaimsAsync();

        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByIdAsync(string id);

        string GetUserData(User user);

        string GetUserData(User user, string provider, bool hasPassword);

        string GetUserIdFromClaims();

        Task<bool> HasPasswordAsync(User user);

        Task<bool> VerifyDeleteAccountTokenAsync(User user, string token);
    }
}