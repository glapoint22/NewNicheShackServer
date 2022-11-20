using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.ExternalLogIn.Commands
{
    public sealed record ExternalLogInCommand(
        string FirstName,
        string LastName,
        string Email,
        string Provider) : IRequest<Result>;
}