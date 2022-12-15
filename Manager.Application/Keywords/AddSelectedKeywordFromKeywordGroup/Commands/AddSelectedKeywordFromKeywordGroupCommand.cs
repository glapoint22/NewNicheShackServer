using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddSelectedKeywordFromKeywordGroup.Commands
{
    public sealed record AddSelectedKeywordFromKeywordGroupCommand(string ProductId, Guid KeywordGroupId, Guid KeywordId) : IRequest<Result>;
}