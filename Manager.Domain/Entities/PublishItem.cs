using Manager.Domain.Enums;

namespace Manager.Domain.Entities
{
    public sealed class PublishItem
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? PageId { get; set; }
        public Guid? EmailId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public PublishType PublishType { get; set; }
        public PublishStatus PublishStatus { get; set; }

        public Product Product { get; set; } = null!;
        public Page Page { get; set; } = null!;
        public Email Email { get; set; } = null!;
        public User User { get; set; } = null!;

        public static PublishItem AddProduct(Guid productId, string userId, PublishStatus publishStatus)
        {
            PublishItem publishItem = new()
            {
                ProductId = productId,
                UserId = userId,
                PublishType = PublishType.Product,
                PublishStatus = publishStatus
            };

            return publishItem;
        }



        public static PublishItem AddPage(Guid pageId, string userId, PublishStatus publishStatus)
        {
            PublishItem publishItem = new()
            {
                PageId = pageId,
                UserId = userId,
                PublishType = PublishType.Page,
                PublishStatus = publishStatus
            };

            return publishItem;
        }




        public static PublishItem AddEmail(Guid emailId, string userId, PublishStatus publishStatus)
        {
            PublishItem publishItem = new()
            {
                PageId = emailId,
                UserId = userId,
                PublishType = PublishType.Email,
                PublishStatus = publishStatus
            };

            return publishItem;
        }
    }
}