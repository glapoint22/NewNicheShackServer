using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.ForgotPassword.Commands
{
    public sealed record ForgotPasswordCommand(string Email) : IRequest<Result>;
}