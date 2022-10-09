using Microsoft.AspNetCore.Identity;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> CheckPasswordAsync(User user, string password);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<User> CreateUserAsync(string firstName, string lastName, string email, string? password = null);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByIdAsync(string id);

        string GetUserData(User user);

        string GetUserData(User user, string provider, bool hasPassword);

        string? GetUserId();

        Task<bool> HasPasswordAsync(User user);
    }
}