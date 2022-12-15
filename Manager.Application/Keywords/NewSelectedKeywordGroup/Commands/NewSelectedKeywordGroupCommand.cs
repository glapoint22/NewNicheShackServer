using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.NewSelectedKeywordGroup.Commands
{
    public sealed record NewSelectedKeywordGroupCommand(string ProductId, string Name) : IRequest<Result>;
}