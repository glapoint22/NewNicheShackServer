using Shared.PageBuilder.Classes;

namespace Website.Application.Common.Interfaces
{
    public interface IPageService
    {
        Task<WebPage> GetPage(string pageContent);

        Task<WebPage> GetPage(string pageContent, PageParams pageParams);

        Task<IGridData> GetGridData(PageParams pageParams);
    }
}