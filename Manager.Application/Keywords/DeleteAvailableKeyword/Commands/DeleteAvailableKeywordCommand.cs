using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.DeleteAvailableKeyword.Commands
{
    public sealed record DeleteAvailableKeywordCommand(Guid KeywordId, Guid KeywordGroupId) : IRequest<Result>;
}