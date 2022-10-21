using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ForgotPassword.Commands
{
    public sealed record ForgotPasswordCommand(string Email) : IRequest<Result>;
}