using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Enums;
using Manager.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Products.AddProduct.EventHandlers
{
    public sealed class ProductCreatedEventHandler : HierarchyItemCreated, INotificationHandler<ProductCreatedEvent>
    {
        public ProductCreatedEventHandler(IManagerDbContext dbContext) : base(dbContext) { }



        // ---------------------------------------------------------------------------------- Handle ------------------------------------------------------------------------------
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Add the publish item
            AddPublishItem(notification.ProductId, notification.UserId);

            // Create the Keyword Group
            KeywordGroup keywordGroup = await CreateKeywordGroup(notification.Name, true);


            // Create the Keyword
            Keyword keyword = await CreateKeyword(notification.Name);


            // Add keyword to group
            await AddKeywordToGroup(keyword, keywordGroup);


            // Add the keyword group to the product
            await AddKeywordGroupToProduct(notification.ProductId, keywordGroup);


            // Add the keyword to the product
            await AddKeywordToProduct(notification.ProductId, keyword);

            await _dbContext.SaveChangesAsync();

            // Add the niche keyword and group to the product
            string nicheName = await _dbContext.Products
                .Where(x => x.Id == notification.ProductId)
                .Select(x => x.Subniche.Niche.Name)
                .SingleAsync();

            await AddHierarchyItemToProduct(nicheName, notification.ProductId);


            await _dbContext.SaveChangesAsync();

            // Add the subniche keyword and group to the product
            string subnicheName = await _dbContext.Products
                .Where(x => x.Id == notification.ProductId)
                .Select(x => x.Subniche.Name)
                .SingleAsync();

            await AddHierarchyItemToProduct(subnicheName, notification.ProductId);



            // Save
            await _dbContext.SaveChangesAsync();
        }





        // ----------------------------------------------------------------------------- Add Publish Item -------------------------------------------------------------------------
        private void AddPublishItem(Guid productId, string userId)
        {
            PublishItem publishItem = PublishItem.AddProduct(productId, userId, PublishStatus.New);

            _dbContext.PublishItems.Add(publishItem);
        }





        // ----------------------------------------------------------------------- Add Keyword Group To Product -------------------------------------------------------------------
        private async Task AddKeywordGroupToProduct(Guid productId, KeywordGroup keywordGroup)
        {
            KeywordGroupBelongingToProduct? keywordGroupBelongingToProduct = await _dbContext.KeywordGroupsBelongingToProduct
                .Where(x => x.ProductId == productId && x.KeywordGroupId == keywordGroup.Id)
                .SingleOrDefaultAsync();

            if (keywordGroupBelongingToProduct == null)
            {
                keywordGroupBelongingToProduct = KeywordGroupBelongingToProduct.Create(keywordGroup, productId);
                _dbContext.KeywordGroupsBelongingToProduct.Add(keywordGroupBelongingToProduct);
            }
        }







        // -------------------------------------------------------------------------- Add Keyword To Product ----------------------------------------------------------------------
        private async Task AddKeywordToProduct(Guid productId, Keyword keyword)
        {
            ProductKeyword? productKeyword = await _dbContext.ProductKeywords
                .Where(x => x.ProductId == productId && x.KeywordId == keyword.Id)
                .SingleOrDefaultAsync();

            if (productKeyword == null)
            {
                productKeyword = ProductKeyword.Create(productId, keyword.Id);
                _dbContext.ProductKeywords.Add(productKeyword);
            }
        }





        // --------------------------------------------------------------- Add Hierarchy Item Keyword Group To Product ------------------------------------------------------------
        private async Task AddHierarchyItemToProduct(string hierarchyItemName, Guid productId)
        {
            Keyword? keyword = await _dbContext.Keywords
                .Where(x => x.Name.Trim().ToLower() == hierarchyItemName.Trim().ToLower())
            .SingleOrDefaultAsync();

            if (keyword != null)
            {
                KeywordGroup? keywordGroup = await _dbContext.KeywordGroups
                    .Where(x => x.Name.ToLower() == hierarchyItemName.ToLower())
                    .SingleOrDefaultAsync();

                if (keywordGroup != null)
                {
                    // Add the keyword group to the product
                    await AddKeywordGroupToProduct(productId, keywordGroup);

                    // Get all keywords in this keyword group and add them to the product
                    var keywordIds = await _dbContext.KeywordsInKeywordGroup
                        .Where(x => x.KeywordGroupId == keywordGroup.Id && !x.Keyword.ProductKeywords
                            .Any(z => z.ProductId == productId) && x.KeywordId != keyword.Id)
                        .Select(x => x.KeywordId)
                        .ToListAsync();

                    _dbContext.ProductKeywords.AddRange(keywordIds.Select(x => ProductKeyword.Create(productId, x)));

                    // Add the keyword to the product
                    await AddKeywordToProduct(productId, keyword);
                }
            }
        }
    }
}