using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductKeyword.Commands
{
    public sealed class SetProductKeywordCommandHandler : IRequestHandler<SetProductKeywordCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetProductKeywordCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetProductKeywordCommand request, CancellationToken cancellationToken)
        {
            ProductKeyword productKeyword;

            if (request.Checked)
            {
                productKeyword = ProductKeyword.Create(request.ProductId, request.KeywordId);
                _dbContext.ProductKeywords.Add(productKeyword);
            }
            else
            {
                productKeyword = await _dbContext.ProductKeywords
                    .Where(x => x.ProductId == request.ProductId && x.KeywordId == request.KeywordId)
                .SingleAsync();

                _dbContext.ProductKeywords.Remove(productKeyword);
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}