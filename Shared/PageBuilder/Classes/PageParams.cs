using System.Text.RegularExpressions;
using System.Web;

namespace Shared.PageBuilder.Classes
{
    public sealed class PageParams
    {
        public string? SearchTerm { get; set; } = string.Empty;
        public int? NicheId { get; set; }
        public int? SubnicheId { get; set; }
        public int Page { get; set; }
        public string? SortBy { get; set; } = string.Empty;
        public int Limit { get; set; }
        public List<FilterParam> FilterParams { get; set; } = new List<FilterParam>();


        public PageParams(string? searchTerm, int? nicheId, int? subnicheId, int page, string? sortBy, int limit, string? filters)
        {
            SearchTerm = searchTerm;
            NicheId = nicheId;
            SubnicheId = subnicheId;
            Page = page;
            SortBy = sortBy;
            Limit = limit;

            if (filters != null)
            {
                FilterParams = GetFilterParams(filters);
            }
        }




        // -------------------------------------------------------------------- Get Filter Params --------------------------------------------------------------------
        private List<FilterParam> GetFilterParams(string filters)
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
                    Values = filterValues.Split(',')
                            .Select(x => Convert.ToInt32(x))
                            .ToList()
                });
            }

            return filterParams;
        }
    }
}