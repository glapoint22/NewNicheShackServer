using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.RemoveSelectedKeywordGroup.Commands
{
    public sealed record RemoveSelectedKeywordGroupCommand(Guid ProductId, Guid KeywordGroupId) : IRequest<Result>;
}