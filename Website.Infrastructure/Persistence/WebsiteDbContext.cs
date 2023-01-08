using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence
{
    public sealed class WebsiteDbContext : IdentityDbContext<User>, IWebsiteDbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options) : base(options) { }
        public DbSet<BlockedNonAccountEmail> BlockedNonAccountEmails => Set<BlockedNonAccountEmail>();
        public DbSet<Collaborator> Collaborators => Set<Collaborator>();
        public DbSet<Email> Emails => Set<Email>();
        public DbSet<FilterOption> FilterOptions => Set<FilterOption>();
        public DbSet<Filter> Filters => Set<Filter>();
        public DbSet<Keyword> Keywords => Set<Keyword>();
        public DbSet<KeywordSearchVolume> KeywordSearchVolumes => Set<KeywordSearchVolume>();
        public DbSet<ListProduct> ListProducts => Set<ListProduct>();
        public DbSet<List> Lists => Set<List>();
        public DbSet<Media> Media => Set<Media>();
        public DbSet<Niche> Niches => Set<Niche>();
        public DbSet<NotificationGroup> NotificationGroups => Set<NotificationGroup>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
        public DbSet<PageKeyword> PageKeywords => Set<PageKeyword>();
        //public DbSet<PageNiche> PageNiches => Set<PageNiche>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<PageSubniche> PageSubniches => Set<PageSubniche>();
        public DbSet<PricePoint> PricePoints => Set<PricePoint>();
        public DbSet<PriceRange> PriceRanges => Set<PriceRange>();
        public DbSet<ProductFilter> ProductFilters => Set<ProductFilter>();
        public DbSet<ProductKeyword> ProductKeywords => Set<ProductKeyword>();
        public DbSet<ProductMedia> ProductMedia => Set<ProductMedia>();
        public DbSet<ProductOrder> ProductOrders => Set<ProductOrder>();
        public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
        public DbSet<ProductInProductGroup> ProductsInProductGroup => Set<ProductInProductGroup>();
        public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Subniche> Subniches => Set<Subniche>();
        public DbSet<Subproduct> Subproducts => Set<Subproduct>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}