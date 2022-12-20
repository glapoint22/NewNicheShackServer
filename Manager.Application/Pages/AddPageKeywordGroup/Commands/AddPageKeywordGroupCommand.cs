using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.AddPageKeywordGroup.Commands
{
    public sealed record AddPageKeywordGroupCommand(Guid PageId, Guid KeywordGroupId) : IRequest<Result>;
}