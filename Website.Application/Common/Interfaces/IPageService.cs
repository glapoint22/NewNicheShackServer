using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Widgets.GridWidget.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface IPageService
    {
        Task<PageContent> GetPage(string pageContent);

        Task<PageContent> GetPage(string pageContent, PageParams pageParams);

        Task<GridData> GetGridData(PageParams pageParams);
    }
}