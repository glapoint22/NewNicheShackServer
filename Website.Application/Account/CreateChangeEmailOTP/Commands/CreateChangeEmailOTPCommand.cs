using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.CreateChangeEmailOTP.Commands
{
    public sealed record CreateChangeEmailOTPCommand(string Email) : IRequest<Result>;
}