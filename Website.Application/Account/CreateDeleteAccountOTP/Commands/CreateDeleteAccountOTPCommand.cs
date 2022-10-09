using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.CreateDeleteAccountOTP.Commands
{
    public record CreateDeleteAccountOTPCommand() : IRequest<Result>;
}