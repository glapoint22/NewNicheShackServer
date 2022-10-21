using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.Refresh.Commands
{
    public sealed record RefreshCommand : IRequest<Result>;
}