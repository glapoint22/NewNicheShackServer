using MediatR;
using Microsoft.AspNetCore.Identity;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IPublisher _publisher;

        public UserService(UserManager<User> userManager, IPublisher publisher)
        {
            _userManager = userManager;
            _publisher = publisher;
        }

        public async Task CreateUserAsync(string firstName, string lastName, string email, string password)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _publisher.Publish(new SignUpSucceededEvent(user));
            }
        }
    }
}