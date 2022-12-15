using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductKeyword.Commands
{
    public sealed record SetProductKeywordCommand(string ProductId, Guid KeywordId, bool Checked) : IRequest<Result>;
}