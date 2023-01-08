using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetHoplink.Commands
{
    public sealed record SetHoplinkCommand(Guid Id, string Hoplink) : IRequest<Result>;
}