using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.DeleteFilterOption.Commands
{
    public sealed record DeleteFilterOptionCommand(Guid Id) : IRequest<Result>;
}