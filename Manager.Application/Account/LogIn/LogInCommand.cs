using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Account.LogIn
{
    public sealed record LogInCommand(string Email, string Password) : IRequest<Result>;
}