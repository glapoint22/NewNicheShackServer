namespace Manager.Application.Common.Interfaces
{
    public interface IManagerDbContext
    {
        Task<int> SaveChangesAsync();
    }
}