﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Enums;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Pages.GetBrowsePage.Queries
{
    public sealed class GetBrowsePageQueryHandler : IRequestHandler<GetBrowsePageQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly PageBuilder _pageBuilder;

        public GetBrowsePageQueryHandler(IWebsiteDbContext dbContext, PageBuilder pageBuilder)
        {
            _dbContext = dbContext;
            _pageBuilder = pageBuilder;
        }



        public async Task<Result> Handle(GetBrowsePageQuery request, CancellationToken cancellationToken)
        {
            string pageContent = null!;

            // Set the params
            PageParams pageParams = new(
                request.NicheId,
                request.SubnicheId,
                request.SortBy,
                request.Filters,
                request.Page);

            // See if we have a custom browse page
            string? pageId = await _dbContext.PageReferenceItems
                .Where(x => x.SubnicheId == request.SubnicheId)
                .Select(x => x.PageId)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (pageId != null)
            {
                // Get the page content for the custom browse page
                pageContent = await _dbContext.Pages
                    .Where(x => x.Id == pageId)
                    .Select(x => x.Content)
                    .SingleAsync(cancellationToken: cancellationToken);
            }
            else
            {
                // Get the default grid page
                pageContent = await _dbContext.Pages
                        .Where(x => x.PageType == (int)PageType.Grid)
                        .Select(x => x.Content)
                        .SingleAsync(cancellationToken: cancellationToken);
            }

            WebPage page = await _pageBuilder.BuildPage(pageContent, pageParams);

            return Result.Succeeded(page);
        }
    }
}