using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductKeyword.Commands
{
    public sealed record SetProductKeywordCommand(Guid ProductId, Guid KeywordId, bool Checked) : IRequest<Result>;
}