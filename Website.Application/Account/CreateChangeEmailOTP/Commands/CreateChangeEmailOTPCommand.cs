using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.CreateChangeEmailOTP.Commands
{
    public sealed record CreateChangeEmailOTPCommand(string Email) : IRequest<Result>;
}