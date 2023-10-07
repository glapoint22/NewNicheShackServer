using MediatR;
using Shared.Common.Classes;

namespace Manager.Application._Publish.PublishPriceRanges.Commands
{
    public sealed record PublishPriceRangesCommand() : IRequest<Result>;
}