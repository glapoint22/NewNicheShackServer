using Microsoft.EntityFrameworkCore;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IWebsiteDbContext
    {
        DbSet<BlockedNonAccountUser> BlockedNonAccountUsers { get; }
        DbSet<Collaborator> Collaborators { get; }
        DbSet<Email> Emails { get; }
        DbSet<FilterOption> FilterOptions { get; }
        DbSet<Filter> Filters { get; }
        DbSet<Keyword> Keywords { get; }
        DbSet<KeywordSearchVolume> KeywordSearchVolumes { get; }
        DbSet<ListProduct> ListProducts { get; }
        DbSet<List> Lists { get; }
        DbSet<Media> Media { get; }
        DbSet<Niche> Niches { get; }
        DbSet<NotificationGroup> NotificationGroups { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<OrderProduct> OrderProducts { get; }
        DbSet<PageKeyword> PageKeywords { get; }
        //DbSet<PageNiche> PageNiches { get; }
        DbSet<Page> Pages { get; }
        DbSet<PageSubniche> PageSubniches { get; }
        DbSet<PricePoint> PricePoints { get; }
        DbSet<PriceRange> PriceRanges { get; }
        DbSet<ProductFilter> ProductFilters { get; }
        DbSet<ProductKeyword> ProductKeywords { get; }
        DbSet<ProductMedia> ProductMedia { get; }
        DbSet<ProductOrder> ProductOrders { get; }
        DbSet<ProductPrice> ProductPrices { get; }
        DbSet<ProductInProductGroup> ProductsInProductGroup { get; }
        DbSet<ProductReview> ProductReviews { get; }
        DbSet<Product> Products { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Subniche> Subniches { get; }
        DbSet<Subproduct> Subproducts { get; }
        DbSet<User> Users { get; }



        Task<int> SaveChangesAsync();
    }
}