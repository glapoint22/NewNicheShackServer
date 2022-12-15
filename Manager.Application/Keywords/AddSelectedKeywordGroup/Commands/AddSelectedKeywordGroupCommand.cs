using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddSelectedKeywordGroup.Commands
{
    public sealed record AddSelectedKeywordGroupCommand(string ProductId, Guid KeywordGroupId) : IRequest<Result>;
}