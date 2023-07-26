using System.Linq.Expressions;

namespace Shared.Common.Interfaces
{
    public interface IRepository
    {
        IQueryable<IMedia> Media(Expression<Func<IMedia, bool>> predicate);
        IQueryable<IPage> Pages(Expression<Func<IPage, bool>> predicate);
        IQueryable<IProduct> Products(Expression<Func<IProduct, bool>> predicate);
        IQueryable<ISubniche> Subniches(Expression<Func<ISubniche, bool>> predicate);
    }
}