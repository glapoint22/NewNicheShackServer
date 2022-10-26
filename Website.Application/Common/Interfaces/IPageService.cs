﻿using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Enums;

namespace Website.Application.Common.Interfaces
{
    public interface IPageService
    {
        Task<WebPage> GetPage(string searchTerm, int? nicheId, int? subnicheId, string? filters);
        Task<WebPage> GetPage(int? nicheId, int? subnicheId, string? filters);
        Task<WebPage> GetPage(string pageId);
        Task<WebPage> GetPage(PageType pageType);
    }
}