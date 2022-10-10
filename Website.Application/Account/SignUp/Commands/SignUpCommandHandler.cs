using MediatR;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

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
            await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.Password);

            return Result.Succeeded();
        }
    }
}