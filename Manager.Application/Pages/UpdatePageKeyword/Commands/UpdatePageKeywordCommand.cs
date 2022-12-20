using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.UpdatePageKeyword.Commands
{
    public sealed record UpdatePageKeywordCommand(Guid PageId, Guid KeywordGroupId, Guid KeywordId, bool Checked) : IRequest<Result>;
}