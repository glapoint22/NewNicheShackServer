using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.LogOut.Commands
{
    public sealed record LogOutCommand() : IRequest<Result>;
}