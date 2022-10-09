using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;
using Website.Domain.Events;

namespace Website.Application.Account.SignUp.Commands
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
    {
        private readonly IUserService _userService;

        public SignUpCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.Password);

            // If we were unable to create a new user
            if (user == null) throw new Exception("Error while trying to create a new user.");

            return Result.Succeeded();
        }
    }
}