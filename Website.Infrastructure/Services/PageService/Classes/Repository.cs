using Shared.Common.Interfaces;
using System.Linq.Expressions;

namespace Website.Infrastructure.Services.PageService.Classes
{
    public sealed class Repository : IRepository
    {
        public IQueryable<IMedia> Media(Expression<Func<IMedia, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IPage> Pages(Expression<Func<IPage, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IProduct> Products(Expression<Func<IProduct, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}