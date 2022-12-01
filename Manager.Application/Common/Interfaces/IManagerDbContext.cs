using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Common.Interfaces
{
    public interface IManagerDbContext
    {
        DbSet<FilterOption> FilterOptions { get; }
        DbSet<Filter> Filters { get; }
        DbSet<KeywordGroup> KeywordGroups { get; }
        DbSet<KeywordGroupBelongingToProduct> KeywordGroupsBelongingToProduct { get; }
        DbSet<Keyword> Keywords { get; }
        DbSet<KeywordInKeywordGroup> KeywordsInKeywordGroup { get; }
        DbSet<Media> Media { get; }
        DbSet<Niche> Niches { get; }
        DbSet<NotificationEmployeeNote> NotificationEmployeeNotes { get; }
        DbSet<Page> Pages { get; }
        DbSet<PricePoint> PricePoints { get; }
        DbSet<ProductFilter> ProductFilters { get; }
        DbSet<ProductGroup> ProductGroups { get; }
        DbSet<ProductKeyword> ProductKeywords { get; }
        DbSet<ProductMedia> ProductMedia { get; }
        DbSet<ProductPrice> ProductPrices { get; }
        DbSet<ProductInProductGroup> ProductsInProductGroup { get; }
        DbSet<Product> Products { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Subniche> Subniches { get; }
        DbSet<Subproduct> Subproducts { get; }
        DbSet<Vendor> Vendors { get; }

        Task<int> SaveChangesAsync();
    }
}