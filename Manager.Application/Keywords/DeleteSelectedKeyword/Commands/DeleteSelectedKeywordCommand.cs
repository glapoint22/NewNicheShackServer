using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.DeleteSelectedKeyword.Commands
{
    public sealed record DeleteSelectedKeywordCommand(Guid KeywordId, Guid KeywordGroupId, Guid ProductId) : IRequest<Result>;
}