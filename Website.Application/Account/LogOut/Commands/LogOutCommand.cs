using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.LogOut.Commands
{
    public record LogOutCommand() : IRequest<Result>;
}