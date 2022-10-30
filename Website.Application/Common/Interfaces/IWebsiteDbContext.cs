using Microsoft.EntityFrameworkCore;

using Shared.Common.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IWebsiteDbContext
    {
        DbSet<CollaboratorProduct> CollaboratorProducts { get; }
        DbSet<Collaborator> Collaborators { get; }
        DbSet<FilterOption> FilterOptions { get; }
        DbSet<Filter> Filters { get; }
        DbSet<KeywordGroup> KeywordGroups { get; }
        DbSet<Keyword> Keywords { get; }
        DbSet<KeywordSearchVolume> KeywordSearchVolumes { get; }
        DbSet<KeywordInKeywordGroup> KeywordsInKeywordGroup { get; }
        DbSet<List> Lists { get; }
        DbSet<Media> Media { get; }
        DbSet<Niche> Niches { get; }
        DbSet<OrderProduct> OrderProducts { get; }
        DbSet<PageReferenceItem> PageReferenceItems { get; }
        DbSet<Page> Pages { get; }
        DbSet<PriceRange> PriceRanges { get; }
        DbSet<ProductFilter> ProductFilters { get; }
        DbSet<ProductOrder> ProductOrders { get; }
        DbSet<ProductPrice> ProductPrices { get; }
        DbSet<Product> Products { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Subniche> Subniches { get; }
        DbSet<User> Users { get; }



        Task<int> SaveChangesAsync();
    }
}