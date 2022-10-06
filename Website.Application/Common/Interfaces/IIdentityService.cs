using Microsoft.AspNetCore.Identity;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<User> CreateUserAsync(string firstName, string lastName, string email, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task LogInAsync(User user, bool isPersistent);
    }
}