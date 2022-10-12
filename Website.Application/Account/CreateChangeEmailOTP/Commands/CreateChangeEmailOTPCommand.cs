using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.CreateChangeEmailOTP.Commands
{
    public record CreateChangeEmailOTPCommand(string Email) : IRequest<Result>;
}