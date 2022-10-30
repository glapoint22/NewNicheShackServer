using Shared.Common.Classes;
using Shared.QueryBuilder.Enums;

namespace Shared.QueryBuilder.Classes
{
    public sealed class QueryRow
    {
        public QueryType? QueryType { get; set; }
        public LogicalOperatorType? LogicalOperatorType { get; set; }
        public ComparisonOperatorType? ComparisonOperatorType { get; set; }
        public Item? Item { get; set; }
        public int? IntValue { get; set; }
        public DateTime? Date { get; set; }
        public double? Price { get; set; }
        public AutoQueryType? Auto { get; set; }
        public string? StringValue { get; set; }
        public Tuple<double, double>? PriceRange { get; set; } = null!;
        public List<int> Filters { get; set; } = new List<int>();
    }
}