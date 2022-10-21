using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.LogIn.Commands
{
    public sealed record LogInCommand(string Email, string Password, bool IsPersistent) : IRequest<Result>;
}