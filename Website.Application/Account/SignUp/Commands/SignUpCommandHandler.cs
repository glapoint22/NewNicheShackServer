using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Account.SignUp.Commands
{
    public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
    {
        private readonly IUserService _userService;

        public SignUpCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.Password);

            if (user == null) return Result.Failed();
            return Result.Succeeded();
        }
    }
}