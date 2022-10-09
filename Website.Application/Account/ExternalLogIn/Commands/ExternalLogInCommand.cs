using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ExternalLogIn.Commands
{
    public record ExternalLogInCommand(
        string FirstName,
        string LastName,
        string Email,
        string Provider) : IRequest<Result>;
}