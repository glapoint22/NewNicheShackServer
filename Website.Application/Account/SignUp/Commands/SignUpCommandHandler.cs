using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.SignUp.Commands
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
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
            User user = await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.Password);

            if (user != null)
            {
                // This is used to trigger the domain event
                await _dbContext.SaveChangesAsync();
                return Result.Succeeded();
            }

            throw new Exception("Error while trying to create user.");
        }
    }
}