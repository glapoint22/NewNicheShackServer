using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence
{
    public sealed class WebsiteDbContext : IdentityDbContext<User>, IWebsiteDbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options) : base(options)
        {
        }

        public DbSet<CollaboratorProduct> CollaboratorProducts => Set<CollaboratorProduct>();
        public DbSet<Collaborator> Collaborators => Set<Collaborator>();
        public DbSet<List> Lists => Set<List>();
        public DbSet<Media> Media => Set<Media>();
        public DbSet<Niche> Niches => Set<Niche>();
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
        public DbSet<ProductOrder> ProductOrders => Set<ProductOrder>();
        public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Subniche> Subniches => Set<Subniche>();

        

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