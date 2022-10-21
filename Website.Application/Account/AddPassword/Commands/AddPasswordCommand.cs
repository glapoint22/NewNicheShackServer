using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.AddPassword.Commands
{
    public sealed record AddPasswordCommand(string Password) : IRequest<Result>;
}