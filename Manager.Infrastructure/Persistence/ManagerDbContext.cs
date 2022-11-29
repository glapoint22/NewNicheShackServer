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

        public DbSet<Media> Media => Set<Media>();
        public DbSet<NotificationEmployeeNote> NotificationEmployeeNotes => Set<NotificationEmployeeNote>();
        public DbSet<Page> Pages => Set<Page>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Niche> Niches => Set<Niche>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Subniche> Subniches => Set<Subniche>();
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