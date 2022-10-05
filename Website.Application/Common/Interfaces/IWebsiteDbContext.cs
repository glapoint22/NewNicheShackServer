using Microsoft.EntityFrameworkCore;
using Website.Domain.Entities;

namespace Website.Application.Common.Interfaces
{
    public interface IWebsiteDbContext
    {
        DbSet<User> Users { get; }
    }
}
