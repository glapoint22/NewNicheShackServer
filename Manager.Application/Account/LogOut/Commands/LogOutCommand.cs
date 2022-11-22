using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Account.LogOut.Commands
{
    public sealed record LogOutCommand() : IRequest<Result>;
}