using System.Linq.Expressions;

namespace Website.Application.Common.Interfaces
{
    public interface IQueryService
    {
        Expression<Func<T, bool>> BuildQuery<T>(string? searchTerm = null, string? filters = null, int? nicheId = null, int? subnicheId = null);
    }
}