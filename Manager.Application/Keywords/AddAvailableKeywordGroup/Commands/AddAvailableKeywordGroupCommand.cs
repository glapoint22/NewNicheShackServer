using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddAvailableKeywordGroup.Commands
{
    public sealed record AddAvailableKeywordGroupCommand(string Name) : IRequest<Result>;
}