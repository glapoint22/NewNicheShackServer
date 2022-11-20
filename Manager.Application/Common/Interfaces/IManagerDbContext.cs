using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Common.Interfaces
{
    public interface IManagerDbContext
    {
        DbSet<NotificationEmployeeNote> NotificationEmployeeNotes { get; }
        Task<int> SaveChangesAsync();
    }
}