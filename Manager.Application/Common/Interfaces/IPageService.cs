using Shared.PageBuilder.Classes;

namespace Manager.Application.Common.Interfaces
{
    public interface IPageService
    {
        Task<WebPage> GetPage(string pageContent);
    }
}