using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Persistence
{
    public class WebsiteDbContext : IdentityDbContext<User>, IWebsiteDbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options) : base(options) { }

        public DbSet<Collaborator> Collaborators => Set<Collaborator>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<List> Lists => Set<List>();

        

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