using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.Refresh.Commands
{
    public record RefreshCommand : IRequest<Result>;
}