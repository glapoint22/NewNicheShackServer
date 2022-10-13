using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ResendAccountActivationEmail.Commands
{
    public record ResendAccountActivationEmailCommand(string Email) : IRequest<Result>;
}