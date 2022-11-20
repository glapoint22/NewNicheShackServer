using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.SignUp.Commands
{
    public sealed record SignUpCommand : IRequest<Result>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}