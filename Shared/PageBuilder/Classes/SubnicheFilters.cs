﻿using Shared.Common.Entities;

namespace Shared.PageBuilder.Classes
{
    public sealed class SubnicheFilters
    {
        public List<SubnicheFilter> Visible { get; set; } = new List<SubnicheFilter>();
        public List<SubnicheFilter> Hidden { get; set; } = new List<SubnicheFilter>();

        public SubnicheFilters(List<Subniche> subniches)
        {
            for (int i = 0; i < subniches.Count; i++)
            {
                Subniche subniche = subniches[i];
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