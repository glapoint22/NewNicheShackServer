using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ResendAccountActivationEmail.Commands
{
    public sealed record ResendAccountActivationEmailCommand(string Email) : IRequest<Result>;
}