using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.SignUp.Commands
{
    public record SignUpCommand : IRequest<Result>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}