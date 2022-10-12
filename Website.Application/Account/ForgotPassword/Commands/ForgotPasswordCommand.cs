using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ForgotPassword.Commands
{
    public record ForgotPasswordCommand(string Email) : IRequest<Result>;
}