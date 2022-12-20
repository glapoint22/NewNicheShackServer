namespace Shared.Common.Interfaces
{
    public interface IKeywordGroupBelongingToProduct
    {
        Guid ProductId { get; set; }
        Guid KeywordGroupId { get; set; }
    }
}