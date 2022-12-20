using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddSelectedKeywordGroup.Commands
{
    public sealed record AddSelectedKeywordGroupCommand(Guid ProductId, Guid KeywordGroupId) : IRequest<Result>;
}