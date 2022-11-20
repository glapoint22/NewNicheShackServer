using Shared.Common.Widgets;
using Website.Application.Common.Classes;

namespace Website.Infrastructure.Services.PageService.Widgetes
{
    public abstract class WebsiteWidget : Widget
    {
        public virtual Task SetData(PageParams pageParams)
        {
            return Task.CompletedTask;
        }
    }
}