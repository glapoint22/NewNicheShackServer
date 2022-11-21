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

        public DbSet<NotificationEmployeeNote> NotificationEmployeeNotes => Set<NotificationEmployeeNote>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


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