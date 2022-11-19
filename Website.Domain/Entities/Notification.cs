using Shared.Common.Enums;
using System.Text.Json;

namespace Website.Domain.Entities
{
    public sealed class Notification
    {
        public Guid Id { get; set; }
        public Guid NotificationGroupId { get; set; }
        public string? UserId { get; set; }
        public string? ProductId { get; set; }
        public string? ListId { get; set; }
        public Guid? ReviewId { get; set; }
        public int Type { get; set; }
        public string? Name { get; set; }
        public string? UserImage { get; set; }
        public string? Text { get; set; }
        public string? NonAccountName { get; set; }
        public string? NonAccountEmail { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreationDate { get; set; }


        public NotificationGroup NotificationGroup { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
        public ProductReview ProductReview { get; set; } = null!;
        public List List { get; set; } = null!;




        public static Notification CreateNonAccountMessageNotification(Guid notificationGroupId, string name, string email, string text)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                Type = (int)NotificationType.Message,
                NonAccountName = name,
                NonAccountEmail = email,
                Text = text,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }




        public static Notification CreateMessageNotification(Guid notificationGroupId, string userId, string text)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                Type = (int)NotificationType.Message,
                UserId = userId,
                Text = text,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }



        public static Notification CreateProductNotification(Guid notificationGroupId, string userId, string productId, int type, string? text)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                UserId = userId,
                ProductId = productId,
                Type = type,
                Text = text,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }




        public static Notification CreateReviewComplaintNotification(Guid notificationGroupId, string userId, string productId, Guid reviewId, string text)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                UserId = userId,
                ProductId = productId,
                Type = (int)NotificationType.ReviewComplaint,
                ReviewId = reviewId,
                Text = text,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }




        public static Notification CreateNewUserNameNotification(Guid notificationGroupId, string userId, string userName)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                Type = (int)NotificationType.UserName,
                Name = userName,
                UserId = userId,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }




        public static Notification CreateNewUserImageNotification(Guid notificationGroupId, string userId, string userImage)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                Type = (int)NotificationType.UserImage,
                UserImage = userImage,
                UserId = userId,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }




        public static Notification CreateListNotification(Guid notificationGroupId, string listId, string name, string? description, string userId)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                Type = (int)NotificationType.List,
                ListId = listId,
                Name = name,
                Text = description,
                UserId = userId,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }




        public static Notification CreateReviewNotification(Guid notificationGroupId, string userId, string productId, Guid reviewId)
        {
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = notificationGroupId,
                Type = (int)NotificationType.Review,
                ProductId = productId,
                ReviewId = reviewId,
                UserId = userId,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }





        public static Notification CreateErrorNotification(Exception error)
        {
            var errorObject = new
            {
                error.Message,
                error.InnerException,
                error.Source
            };

            var serializedError = JsonSerializer.Serialize(errorObject);

            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                NotificationGroupId = Guid.Empty,
                Type = (int)NotificationType.Error,
                Text = serializedError,
                CreationDate = DateTime.UtcNow
            };

            SetNotificationGroupId(notification);

            return notification;
        }



        private static void SetNotificationGroupId(Notification notification)
        {
            // If we don't have a notification group id
            if (notification.NotificationGroupId == Guid.Empty)
            {
                // Then create a new notification group
                NotificationGroup notificationGroup = NotificationGroup.Create();

                notification.NotificationGroupId = notificationGroup.Id;
                notification.NotificationGroup = notificationGroup;
            }
        }
    }
}