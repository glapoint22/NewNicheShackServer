using Manager.Application.Common.Interfaces;
using Manager.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application._Publish.PublishPage.Commands
{
    public sealed class PublishPageCommandHandler : IRequestHandler<PublishPageCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public PublishPageCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
        }


        // ---------------------------------------------------------------------------------- Handle -------------------------------------------------------------------------------
        public async Task<Result> Handle(PublishPageCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.PublishItem? publishItem = await _managerDbContext.PublishItems
                .Where(x => x.PageId == request.PageId)
                .SingleOrDefaultAsync();

            if (publishItem != null)
            {
                Domain.Entities.Page page = await _managerDbContext.Pages
                .Where(x => x.Id == request.PageId)
                .Include(x => x.PageKeywords)
                    .ThenInclude(x => x.KeywordInKeywordGroup.Keyword)
                .Include(x => x.PageSubniches)
                    .ThenInclude(x => x.Subniche)
                .SingleAsync();


                if (publishItem.PublishStatus == PublishStatus.New)
                {
                    await PublishPage(page);
                }
                else
                {
                    await UpdatePage(page);
                }

                // Remove the publish item
                _managerDbContext.PublishItems.Remove(publishItem);
                await _managerDbContext.SaveChangesAsync();

                // Upadate the website
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }










        // ---------------------------------------------------------------------------- Publish Keywords ---------------------------------------------------------------------------
        private async Task PublishKeywords(Domain.Entities.Page page)
        {
            // Get the keyword ids from website that this page is using
            List<Guid> websiteKeywordIds = await _websiteDbContext.Keywords
                .Where(x => page.PageKeywords
                    .Select(z => z.KeywordInKeywordGroup.KeywordId)
                    .Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();


            // Get the keyword ids from manager that website does not have
            List<Guid> managerKeywordIds = page.PageKeywords
                .Where(x => !websiteKeywordIds
                    .Contains(x.KeywordInKeywordGroup.KeywordId))
                .Select(x => x.KeywordInKeywordGroup.KeywordId)
                .ToList();


            // If we have keyword ids from manager that website does not have
            if (managerKeywordIds.Count > 0)
            {
                // Get the keywords from manager and add it to website
                List<Website.Domain.Entities.Keyword> keywords = await _managerDbContext.Keywords
                .Where(x => managerKeywordIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.Keyword
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                _websiteDbContext.Keywords.AddRange(keywords);
            }
        }










        // ----------------------------------------------------------------------------- Publish Niches ----------------------------------------------------------------------------
        private async Task PublishNiches(Domain.Entities.Page page)
        {
            // Get the niche ids from website that this page is using
            List<Guid> websiteNicheIds = await _websiteDbContext.Niches
                .Where(x => page.PageSubniches
                    .Select(z => z.Subniche.NicheId)
                    .Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();


            // Get the niche ids from manager that website does not have
            List<Guid> managerNicheIds = page.PageSubniches
                .Where(x => !websiteNicheIds
                    .Contains(x.Subniche.NicheId))
                .Select(x => x.Subniche.NicheId)
                .ToList();




            // If we have niche ids from manager that website does not have
            if (managerNicheIds.Count > 0)
            {
                // Get the niches from manager and add it to website
                List<Website.Domain.Entities.Niche> niches = await _managerDbContext.Niches
                .Where(x => managerNicheIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.Niche
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlName = x.UrlName
                }).ToListAsync();

                _websiteDbContext.Niches.AddRange(niches);
            }
        }












        // ------------------------------------------------------------------------------ Publish Page -----------------------------------------------------------------------------
        private async Task PublishPage(Domain.Entities.Page page)
        {
            // Add the page to website
            Website.Domain.Entities.Page websitePage = new()
            {
                Id = page.Id,
                Name = page.Name,
                UrlName = page.UrlName,
                Content = page.Content,
                PageType = page.PageType
            };

            _websiteDbContext.Pages.Add(websitePage);


            // If we have page keywords
            if (page.PageKeywords.Count > 0)
            {
                await PublishKeywords(page);

                // Add the page keywords
                List<Website.Domain.Entities.PageKeyword> pageKeywords = page.PageKeywords
                    .Select(x => new Website.Domain.Entities.PageKeyword
                    {
                        PageId = x.PageId,
                        KeywordId = x.KeywordInKeywordGroup.KeywordId
                    }).ToList();

                _websiteDbContext.PageKeywords.AddRange(pageKeywords);
            }



            // If we have page subniches
            else if (page.PageSubniches.Count > 0)
            {
                await PublishNiches(page);
                await PublishSubniches(page);

                // Add the page subniches
                List<Website.Domain.Entities.PageSubniche> pageSubniches = page.PageSubniches
                    .Select(x => new Website.Domain.Entities.PageSubniche
                    {
                        PageId = x.PageId,
                        SubnicheId = x.SubnicheId
                    }).ToList();

                _websiteDbContext.PageSubniches.AddRange(pageSubniches);
            }
        }

















        // --------------------------------------------------------------------------- Publish Subniches ---------------------------------------------------------------------------
        private async Task PublishSubniches(Domain.Entities.Page page)
        {
            // Get the subniche ids from website that this page is using
            List<Guid> websiteSubnicheIds = await _websiteDbContext.Subniches
                .Where(x => page.PageSubniches
                    .Select(z => z.SubnicheId)
                    .Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();


            // Get the subniche ids from manager that website does not have
            List<Guid> managerSubnicheIds = page.PageSubniches
                .Where(x => !websiteSubnicheIds
                    .Contains(x.SubnicheId))
                .Select(x => x.SubnicheId)
                .ToList();




            // If we have subniche ids from manager that website does not have
            if (managerSubnicheIds.Count > 0)
            {
                // Get the subniches from manager and add it to website
                List<Website.Domain.Entities.Subniche> subniches = await _managerDbContext.Subniches
                .Where(x => managerSubnicheIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.Subniche
                {
                    Id = x.Id,
                    NicheId = x.NicheId,
                    Name = x.Name,
                    UrlName = x.UrlName
                }).ToListAsync();

                _websiteDbContext.Subniches.AddRange(subniches);
            }
        }













        // ------------------------------------------------------------------------------- Update Page -----------------------------------------------------------------------------
        private async Task UpdatePage(Domain.Entities.Page page)
        {
            await PublishNiches(page);
            await PublishSubniches(page);
            await PublishKeywords(page);

            await UpdatePageKeywords(page);
            await UpdatePageSubniches(page);

            Website.Domain.Entities.Page websitePage = await _websiteDbContext.Pages
                .Where(x => x.Id == page.Id)
                .SingleAsync();

            websitePage.Name = page.Name;
            websitePage.UrlName = page.UrlName;
            websitePage.Content = page.Content;
            websitePage.PageType = page.PageType;
        }






        








        // --------------------------------------------------------------------------- Update Page Keywords ------------------------------------------------------------------------
        private async Task UpdatePageKeywords(Domain.Entities.Page page)
        {
            // Get website page keywords that this page is using
            List<Website.Domain.Entities.PageKeyword> websitePageKeywords = await _websiteDbContext.PageKeywords
                .Where(x => x.PageId == page.Id)
                .ToListAsync();


            // Get page keywords that website does not have
            var missingWebsitePageKeywords = page.PageKeywords
                .Where(x => !websitePageKeywords
                    .Select(z => z.KeywordId)
                    .Contains(x.KeywordInKeywordGroup.KeywordId))
                .ToList();


            // If we have page keywords that we need to add to website
            if (missingWebsitePageKeywords.Count > 0)
            {
                foreach (var missingWebsitePageKeyword in missingWebsitePageKeywords)
                {
                    _websiteDbContext.PageKeywords.Add(new Website.Domain.Entities.PageKeyword
                    {
                        PageId = page.Id,
                        KeywordId = missingWebsitePageKeyword.KeywordInKeywordGroup.KeywordId
                    });
                }
            }


            // Get page keywords that manager does not have
            List<Website.Domain.Entities.PageKeyword> missingManagerPageKeywords = websitePageKeywords
                .Where(x => !page.PageKeywords
                    .Select(z => z.KeywordInKeywordGroup.KeywordId)
                    .Contains(x.KeywordId))
                .ToList();



            // If we have page keywords that we need to remove from website
            if (missingManagerPageKeywords.Count > 0)
            {
                foreach (Website.Domain.Entities.PageKeyword pageKeyword in missingManagerPageKeywords)
                {
                    _websiteDbContext.PageKeywords.Remove(pageKeyword);
                }
            }
        }










        // -------------------------------------------------------------------------- Update Page Subniches ------------------------------------------------------------------------
        private async Task UpdatePageSubniches(Domain.Entities.Page page)
        {
            // Get website page subniches that this page is using
            List<Website.Domain.Entities.PageSubniche> websitePageSubniches = await _websiteDbContext.PageSubniches
                .Where(x => x.PageId == page.Id)
                .ToListAsync();


            // Get page Subniches that website does not have
            var missingWebsitePageSubniches = page.PageSubniches
                .Where(x => !websitePageSubniches
                    .Select(z => z.SubnicheId)
                    .Contains(x.SubnicheId))
                .ToList();


            // If we have page subniches that we need to add to website
            if (missingWebsitePageSubniches.Count > 0)
            {
                foreach (var missingWebsitePageSubniche in missingWebsitePageSubniches)
                {
                    _websiteDbContext.PageSubniches.Add(new Website.Domain.Entities.PageSubniche
                    {
                        PageId = page.Id,
                        SubnicheId = missingWebsitePageSubniche.SubnicheId
                    });
                }
            }


            // Get page subniches that manager does not have
            List<Website.Domain.Entities.PageSubniche> missingManagerPageSubniches = websitePageSubniches
                .Where(x => !page.PageSubniches
                    .Select(z => z.SubnicheId)
                    .Contains(x.SubnicheId))
                .ToList();



            // If we have page subniches that we need to remove from website
            if (missingManagerPageSubniches.Count > 0)
            {
                foreach (Website.Domain.Entities.PageSubniche pageSubniche in missingManagerPageSubniches)
                {
                    _websiteDbContext.PageSubniches.Remove(pageSubniche);
                }
            }
        }
    }
}