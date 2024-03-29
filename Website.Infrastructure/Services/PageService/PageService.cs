﻿using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Widgets.GridWidget.Classes;
using Website.Application.Common.Interfaces;
using Website.Infrastructure.Services.Common;
using Website.Infrastructure.Services.PageService.Classes;

namespace Website.Infrastructure.Services.PageService
{
    public sealed class PageService : IPageService
    {
        private readonly IWebsiteDbContext _dbContext;

        public PageService(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<GridData> GetGridData(PageParams pageParams)
        {
            var gridData = new WebsiteGridData(_dbContext);

            return gridData.GetData(pageParams);
        }

        public async Task<PageContent> GetPage(string pageContent)
        {
            var pageBuilder = new WebsitePageBuilder(_dbContext, new Repository(_dbContext));

            return await pageBuilder.BuildPage(pageContent);
        }

        public async Task<PageContent> GetPage(string pageContent, PageParams pageParams)
        {
            var pageBuilder = new WebsitePageBuilder(_dbContext, new Repository(_dbContext), pageParams);

            return await pageBuilder.BuildPage(pageContent);
        }
    }
}