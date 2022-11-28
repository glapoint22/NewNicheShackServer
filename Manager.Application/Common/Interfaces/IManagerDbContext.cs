using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Common.Interfaces
{
    public interface IManagerDbContext
    {
        DbSet<Media> Media { get; }
        DbSet<NotificationEmployeeNote> NotificationEmployeeNotes { get; }
        DbSet<Page> Pages { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync();
    }
}