using Shared.Common.Interfaces;
using Shared.Common.Widgets;
using Shared.PageBuilder.Classes;

namespace Shared.PageBuilder.Widgets
{
    public sealed class GridWidget : Widget
    {
        private readonly IRepository _repository;

        public GridWidget(IRepository repository)
        {
            _repository = repository;
        }

        public GridData GridData { get; set; } = null!;


        
    }
}