using Shared.QueryService.Enums;

namespace Shared.QueryService.Classes
{
    public sealed class QueryElement
    {
        public QueryRow QueryRow { get; set; } = null!;
        public QueryGroup QueryGroup { get; set; } = null!;
        public QueryElementType QueryElementType { get; set; }
    }
}