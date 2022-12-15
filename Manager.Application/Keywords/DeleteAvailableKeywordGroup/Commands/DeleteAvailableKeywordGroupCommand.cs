using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.DeleteAvailableKeywordGroup.Commands
{
    public sealed record DeleteAvailableKeywordGroupCommand(Guid KeywordGroupId) : IRequest<Result>;
}