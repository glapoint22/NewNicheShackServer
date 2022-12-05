using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageKeywordGroup.Commands
{
    public sealed record DeletePageKeywordGroupCommand(string PageId, Guid KeywordGroupId) : IRequest<Result>;
}