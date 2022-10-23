using Shared.Common.Classes;
using Shared.QueryService.Enums;

namespace Shared.QueryService.Classes
{
    public sealed class QueryRow
    {
        public QueryType? QueryType { get; set; }
        public LogicalOperatorType? LogicalOperatorType { get; set; }
        public ComparisonOperatorType? ComparisonOperatorType { get; set; }
        public Item? Item { get; set; }
        public int? Integer { get; set; }
        public DateTime? Date { get; set; }
        public double? Price { get; set; }
        public AutoQueryType? Auto { get; set; }
        public string? StringValue { get; set; }
    }
}