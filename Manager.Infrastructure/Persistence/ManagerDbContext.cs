using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Manager.Infrastructure.Persistence
{
    public sealed class ManagerDbContext : IdentityDbContext<User>, IManagerDbContext
    {
        public ManagerDbContext(DbContextOptions<ManagerDbContext> options) : base(options) { }

        public DbSet<FilterOption> FilterOptions => Set<FilterOption>();
        public DbSet<Filter> Filters => Set<Filter>();
        public DbSet<KeywordGroup> KeywordGroups => Set<KeywordGroup>();
        public DbSet<KeywordGroupBelongingToProduct> KeywordGroupsBelongingToProduct => Set<KeywordGroupBelongingToProduct>();
        public DbSet<Keyword> Keywords => Set<Keyword>();
        public DbSet<KeywordInKeywordGroup> KeywordsInKeywordGroup => Set<KeywordInKeywordGroup>();
        public DbSet<Media> Media => Set<Media>();
        public DbSet<Niche> Niches => Set<Niche>();
        public DbSet<NotificationEmployeeNote> NotificationEmployeeNotes => Set<NotificationEmployeeNote>();
        public DbSet<PageKeywordGroup> PageKeywordGroups => Set<PageKeywordGroup>();
        public DbSet<PageKeyword> PageKeywords => Set<PageKeyword>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<PageSubniche> PageSubniches => Set<PageSubniche>();
        public DbSet<PricePoint> PricePoints => Set<PricePoint>();
        public DbSet<ProductFilter> ProductFilters => Set<ProductFilter>();
        public DbSet<ProductGroup> ProductGroups => Set<ProductGroup>();
        public DbSet<ProductKeyword> ProductKeywords => Set<ProductKeyword>();
        public DbSet<ProductMedia> ProductMedia => Set<ProductMedia>();
        public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
        public DbSet<ProductInProductGroup> ProductsInProductGroup => Set<ProductInProductGroup>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Subniche> Subniches => Set<Subniche>();
        public DbSet<Subproduct> Subproducts => Set<Subproduct>();
        public DbSet<Vendor> Vendors => Set<Vendor>();

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