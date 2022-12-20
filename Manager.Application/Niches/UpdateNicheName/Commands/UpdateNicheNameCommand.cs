using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.UpdateNicheName.Commands
{
    public sealed record UpdateNicheNameCommand(Guid Id, string Name) : IRequest<Result>;
}