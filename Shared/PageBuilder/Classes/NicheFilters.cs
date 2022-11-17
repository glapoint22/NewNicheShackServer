using Shared.Common.Entities;

namespace Shared.PageBuilder.Classes
{
    public sealed class NicheFilters
    {
        public List<NicheFilter> Visible { get; set; } = new List<NicheFilter>();
        public List<NicheFilter> Hidden { get; set; } = new List<NicheFilter>();

        public NicheFilters(List<NicheDto> niches)
        {
            for (int i = 0; i < niches.Count; i++)
            {
                NicheDto niche = niches[i];
                NicheFilter nicheFilter = new(niche);

                if (niches.Count >= 8)
                {
                    if (i < 4)
                    {
                        nicheFilter.Visible = true;
                        Visible.Add(nicheFilter);
                    }
                    else
                    {
                        Hidden.Add(nicheFilter);
                    }
                }
                else
                {
                    nicheFilter.Visible = true;
                    Visible.Add(nicheFilter);
                }
            }
        }
    }
}