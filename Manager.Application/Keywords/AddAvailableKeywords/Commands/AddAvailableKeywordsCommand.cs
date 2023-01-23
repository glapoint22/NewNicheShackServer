using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.AddAvailableKeywords.Commands
{
    public sealed record AddAvailableKeywordsCommand(Guid KeywordGroupId, List<string> Keywords) : IRequest<Result>;
}