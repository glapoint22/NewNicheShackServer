using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Account.Refresh.Commands
{
    public sealed record RefreshCommand : IRequest<Result>;
}