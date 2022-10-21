using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.LogOut.Commands
{
    public sealed record LogOutCommand() : IRequest<Result>;
}