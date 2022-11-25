using System.Text.RegularExpressions;
using System.Web;

namespace Shared.PageBuilder.Widgets.GridWidget.Classes
{
    public sealed class PageParams
    {
        public string? SearchTerm { get; set; } = null;
        public string? NicheId { get; set; }
        public string? SubnicheId { get; set; }
        public string? SortBy { get; set; } = null;
        public List<FilterParam> FilterParams { get; set; } = new List<FilterParam>();
        public int Page { get; set; }


        public PageParams(string? searchTerm, string? nicheId, string? subnicheId, string? sortBy, string? filters, int page)
        {
            SearchTerm = searchTerm;
            NicheId = nicheId;
            SubnicheId = subnicheId;
            Page = page;
            SortBy = sortBy;

            if (filters != null)
            {
                FilterParams = GetFilterParams(filters);
            }
        }





        public PageParams(string? nicheId, string? subnicheId, string? sortBy, string? filters, int page)
        {
            NicheId = nicheId;
            SubnicheId = subnicheId;
            Page = page;
            SortBy = sortBy;

            if (filters != null)
            {
                FilterParams = GetFilterParams(filters);
            }
        }




        // -------------------------------------------------------------------- Get Filter Params --------------------------------------------------------------------
        private static List<FilterParam> GetFilterParams(string filters)
        {
            List<FilterParam> filterParams = new();

            filters = HttpUtility.UrlDecode(filters);

            Regex regex = new Regex(@".+?\|[0-9,\-]+\|");
            MatchCollection matches = regex.Matches(filters);


            foreach (Match match in matches)
            {
                regex = new Regex(@"(.+)?\|([0-9,\-]+)\|");
                Match filter = regex.Match(match.Value);

                string filterName = filter.Groups[1].Value;
                string filterValues = filter.Groups[2].Value;

                filterParams.Add(new FilterParam
                {
                    Name = filterName,
                    Values = filterValues.Split(',', '-')
                            .Select(x => Convert.ToInt32(x))
                            .ToList()
                });
            }

            return filterParams;
        }
    }
}