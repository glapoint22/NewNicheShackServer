using Microsoft.EntityFrameworkCore;
using Quartz;
using Shared.Common.Dtos;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public sealed class SearchSuggestionsJob : IJob
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly ISearchSuggestionsService _searchSuggestionsService;

        public SearchSuggestionsJob(IWebsiteDbContext dbContext, ISearchSuggestionsService searchSuggestionsService)
        {
            _dbContext = dbContext;
            _searchSuggestionsService = searchSuggestionsService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<KeywordData> keywords = await _dbContext.ProductKeywords
                .Select(x => new KeywordData
                {
                    Name = x.Keyword.Name,
                    SearchVolume = x.Keyword.KeywordSearchVolumes
                        .Where(z => z.Date >= DateTime.UtcNow.Date
                            .AddMonths(-1) && z.KeywordId == x.KeywordId).Count(),
                    Niche = new NicheDto
                    {
                        Id = x.Product.Subniche.Niche.Id,
                        Name = x.Product.Subniche.Niche.Name,
                        UrlName = x.Product.Subniche.Niche.UrlName,
                    }
                })
                .ToListAsync();


            List<Guid> nicheIds = await _dbContext.Niches
                .Select(x => x.Id)
                .ToListAsync();

            nicheIds.Insert(0, Guid.Empty);


            _searchSuggestionsService.SetSearchSuggestions(keywords, nicheIds);
        }
    }
}