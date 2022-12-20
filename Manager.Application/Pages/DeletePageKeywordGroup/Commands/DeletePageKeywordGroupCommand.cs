using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.DeletePageKeywordGroup.Commands
{
    public sealed record DeletePageKeywordGroupCommand(Guid PageId, Guid KeywordGroupId) : IRequest<Result>;
}