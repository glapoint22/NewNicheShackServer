using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.CreateDeleteAccountOTP.Commands
{
    public sealed record CreateDeleteAccountOTPCommand() : IRequest<Result>;
}