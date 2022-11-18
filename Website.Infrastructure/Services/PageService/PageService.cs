using Shared.PageBuilder.Classes;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Services.PageService.Classes;

namespace Website.Infrastructure.Services.PageService
{
    public sealed class PageService : IPageService
    {
        private readonly WebsitePageBuilder _pageBuilder;
        private readonly GridData _gridData;

        public PageService(IWebsiteDbContext dbContext)
        {
            _pageBuilder = new WebsitePageBuilder(dbContext);
            _gridData = new GridData(dbContext);
        }

        public Task<IGridData> GetGridData(PageParams pageParams)
        {
            return _gridData.GetData(pageParams);
        }

        public async Task<WebPage> GetPage(string pageContent)
        {
            return await _pageBuilder.BuildPage(pageContent);
        }

        public async Task<WebPage> GetPage(string pageContent, PageParams pageParams)
        {
            return await _pageBuilder.BuildPage(pageContent, pageParams);
        }
    }
}