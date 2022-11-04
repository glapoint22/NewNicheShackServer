using MediatR;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.SignUp.Commands
{
    public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IWebsiteDbContext _dbContext;

        public SignUpCommandHandler(IUserService userService, IWebsiteDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.Password);

            if (user == null) return Result.Failed();

            user.AddDomainEvent(new UserCreatedEvent(user.Id));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}