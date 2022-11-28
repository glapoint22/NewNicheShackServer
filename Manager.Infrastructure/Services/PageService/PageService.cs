using Manager.Application.Common.Interfaces;
using Manager.Infrastructure.Services.PageService.Classes;
using Shared.PageBuilder.Classes;

namespace Manager.Infrastructure.Services.PageService
{
    public sealed class PageService : IPageService
    {
        private readonly IManagerDbContext _dbContext;

        public PageService(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WebPage> GetPage(string pageContent)
        {
            var pageBuilder = new PageBuilder(new Repository(_dbContext));

            return await pageBuilder.BuildPage(pageContent);
        }
    }
}