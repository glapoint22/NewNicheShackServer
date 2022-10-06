using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ActivateAccount.Commands
{
    public record ResendAccountActivationEmailCommand(string Email) : IRequest<Result>;
}