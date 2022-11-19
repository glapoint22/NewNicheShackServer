﻿using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Services.PageService.Widgetes
{
    public sealed class GridWidget : Widget
    {
        public GridData GridData { get; set; }


        public GridWidget(IWebsiteDbContext dbContext)
        {
            GridData = new GridData(dbContext);
        }

        public async override Task SetData(PageParams? pageParams)
        {
            await GridData.SetData(pageParams!);
        }
    }
}