using Shared.PageBuilder.Classes;

namespace Manager.Application.Common.Interfaces
{
    public interface IPageService
    {
        Task<PageContent> GetPage(string pageContent);
    }
}