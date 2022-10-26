using Shared.QueryBuilder.Enums;

namespace Shared.QueryBuilder.Classes
{
    public sealed class QueryElement
    {
        public QueryRow QueryRow { get; set; } = null!;
        public QueryGroup QueryGroup { get; set; } = null!;
        public QueryElementType QueryElementType { get; set; }
    }
}