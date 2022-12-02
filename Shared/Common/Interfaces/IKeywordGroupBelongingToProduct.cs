namespace Shared.Common.Interfaces
{
    public interface IKeywordGroupBelongingToProduct
    {
        string ProductId { get; set; }
        Guid KeywordGroupId { get; set; }
    }
}