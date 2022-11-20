using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.Refresh.Commands
{
    public sealed record RefreshCommand : IRequest<Result>;
}