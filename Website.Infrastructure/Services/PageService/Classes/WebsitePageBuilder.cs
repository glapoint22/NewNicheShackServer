using Shared.Common.Enums;
using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Widgets.GridWidget;
using Shared.PageBuilder.Widgets.GridWidget.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Services.PageService.Classes
{
    public sealed class WebsitePageBuilder : PageBuilder
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly PageParams _pageParams = null!;

        public WebsitePageBuilder(IWebsiteDbContext dbContext, IRepository repository) : base(repository)
        {
            _dbContext = dbContext;
        }


        public WebsitePageBuilder(IWebsiteDbContext dbContext, IRepository repository, PageParams pageParams) : base(repository)
        {
            _dbContext = dbContext;
            _pageParams = pageParams;
        }




        // --------------------------------------------------------------------- Set Widget Data ----------------------------------------------------------------------
        protected override async Task SetWidgetData(Widget widget)
        {
            if (widget.WidgetType == WidgetType.Grid)
            {
                var websiteGridWidget = (widget as GridWidget)!;
                WebsiteGridData gridData = new(_dbContext);
                websiteGridWidget.GridData = await gridData.GetData(_pageParams);
            }
            else
            {
                await base.SetWidgetData(widget);
            }
        }
    }
}