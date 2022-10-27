using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.DeleteRefreshToken.Commands
{
    public sealed record DeleteRefreshTokenCommand(string NewRefreshToken) : IRequest<Result>;
}
