using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;
using System.Linq.Expressions;

namespace Shared.PageBuilder.Widgets
{
    public sealed class GridWidget : Widget
    {
        public GridData GridData { get; set; }


        public GridWidget(IRepository repository)
        {
            GridData = new GridData(repository);
        }


        public async override Task SetData<T>(Expression<Func<T, bool>> query)
        {
            await GridData.SetData(query);
        }
    }
}