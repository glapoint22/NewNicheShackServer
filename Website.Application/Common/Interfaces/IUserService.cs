using Microsoft.AspNetCore.Identity;

namespace Website.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(string firstName, string lastName, string email, string password);
    }
}