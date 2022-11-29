using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Common.Interfaces
{
    public interface IManagerDbContext
    {
        DbSet<Media> Media { get; }
        DbSet<Niche> Niches { get; }
        DbSet<NotificationEmployeeNote> NotificationEmployeeNotes { get; }
        DbSet<Page> Pages { get; }
        DbSet<Product> Products { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Subniche> Subniches { get; }
        DbSet<Vendor> Vendors { get; }

        Task<int> SaveChangesAsync();
    }
}