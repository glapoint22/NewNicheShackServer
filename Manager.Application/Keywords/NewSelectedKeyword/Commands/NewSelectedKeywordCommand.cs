using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.NewSelectedKeyword.Commands
{
    public sealed record NewSelectedKeywordCommand(Guid ProductId, Guid KeywordGroupId, string Name) : IRequest<Result>;
}