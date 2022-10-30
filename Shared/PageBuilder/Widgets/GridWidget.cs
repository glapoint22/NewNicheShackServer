using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;

namespace Shared.PageBuilder.Widgets
{
    public sealed class GridWidget : Widget
    {
        public GridData GridData { get; set; }


        public GridWidget(IRepository repository)
        {
            GridData = new GridData(repository);
        }


        public async override Task SetData(PageParams pageParams)
        {
            await GridData.SetData(pageParams);
        }
    }
}