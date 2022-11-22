using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Account.DeleteRefreshToken.Commands
{
    public sealed record DeleteRefreshTokenCommand(string NewRefreshToken) : IRequest<Result>;
}
