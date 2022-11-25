using Shared.Common.Dtos;

namespace Shared.PageBuilder.Widgets.GridWidget.Classes
{
    public sealed class SubnicheFilters
    {
        public List<SubnicheFilter> Visible { get; set; } = new List<SubnicheFilter>();
        public List<SubnicheFilter> Hidden { get; set; } = new List<SubnicheFilter>();

        public SubnicheFilters(List<SubnicheDto> subniches)
        {
            for (int i = 0; i < subniches.Count; i++)
            {
                SubnicheDto subniche = subniches[i];
                SubnicheFilter subnicheFilter = new(subniche);

                if (subniches.Count >= 8)
                {
                    if (i < 4)
                    {
                        subnicheFilter.Visible = true;
                        Visible.Add(subnicheFilter);
                    }
                    else
                    {
                        Hidden.Add(subnicheFilter);
                    }
                }
                else
                {
                    subnicheFilter.Visible = true;
                    Visible.Add(subnicheFilter);
                }
            }
        }
    }
}