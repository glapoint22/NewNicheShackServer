using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddAvailableKeyword.Commands
{
    public sealed record AddAvailableKeywordCommand(Guid Id, string Name) : IRequest<Result>;
}