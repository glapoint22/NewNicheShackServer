using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.CreateDeleteAccountOTP.Commands
{
    public sealed record CreateDeleteAccountOTPCommand() : IRequest<Result>;
}