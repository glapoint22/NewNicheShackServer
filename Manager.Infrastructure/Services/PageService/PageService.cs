using Manager.Application.Common.Interfaces;
using Manager.Infrastructure.Services.PageService.Classes;
using Shared.PageBuilder.Classes;

namespace Manager.Infrastructure.Services.PageService
{
    public sealed class PageService : IPageService
    {
        public async Task<WebPage> GetPage(string pageContent)
        {
            var pageBuilder = new PageBuilder(new Repository());

            return await pageBuilder.BuildPage(pageContent);
        }
    }
}