using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Infrastructure.Migrations
{
    public partial class Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Subproducts_ProductId",
                table: "Subproducts");

            migrationBuilder.DropIndex(
                name: "IX_Subniches_NicheId",
                table: "Subniches");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubnicheId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_UserId",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_PricePoints_ProductId",
                table: "PricePoints");

            migrationBuilder.DropIndex(
                name: "IX_PageKeywords_KeywordId",
                table: "PageKeywords");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ListId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_ListId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderProducts",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "AccessFailedCount", "BlockNotificationSending", "ConcurrencyStamp", "Email", "EmailConfirmed", "EmailOnCollaboratorAddedListItem", "EmailOnCollaboratorDeletedList", "EmailOnCollaboratorJoinedList", "EmailOnCollaboratorMovedListItem", "EmailOnCollaboratorRemovedFromList", "EmailOnCollaboratorRemovedListItem", "EmailOnCollaboratorUpdatedList", "EmailOnEmailUpdated", "EmailOnItemReviewed", "EmailOnNameUpdated", "EmailOnPasswordUpdated", "EmailOnProfileImageUpdated", "EmailOnUserAddedListItem", "EmailOnUserDeletedList", "EmailOnUserJoinedList", "EmailOnUserMovedListItem", "EmailOnUserRemovedCollaborator", "EmailOnUserRemovedFromList", "EmailOnUserRemovedListItem", "EmailOnUserUpdatedList", "FirstName", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "NoncompliantStrikes", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Suspended", "TrackingCode", "TwoFactorEnabled", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TrackingCode",
                table: "Users",
                column: "TrackingCode")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Subproducts_ProductId",
                table: "Subproducts",
                column: "ProductId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "Name", "Description", "ImageId", "Value", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Subniches_NicheId_Disabled_Name",
                table: "Subniches",
                columns: new[] { "NicheId", "Disabled", "Name" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "UrlName" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Id_UserId_DeviceId",
                table: "RefreshTokens",
                columns: new[] { "Id", "UserId", "DeviceId" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Expiration" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Disabled",
                table: "Products",
                column: "Disabled")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "SubnicheId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubnicheId_Disabled",
                table: "Products",
                columns: new[] { "SubnicheId", "Disabled" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "ImageId", "Name", "UrlName", "Description", "Hoplink", "TotalReviews", "Rating", "OneStar", "TwoStars", "ThreeStars", "FourStars", "FiveStars", "ShippingType", "Date", "TrackingCode", "Currency" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_TrackingCode",
                table: "Products",
                column: "TrackingCode")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId_Likes_Rating_Deleted",
                table: "ProductReviews",
                columns: new[] { "ProductId", "Likes", "Rating", "Deleted" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "UserId", "Title", "Date", "Text", "Dislikes" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "Price" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductId_UserId",
                table: "ProductOrders",
                columns: new[] { "ProductId", "UserId" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_UserId",
                table: "ProductOrders",
                column: "UserId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_UserId_Date",
                table: "ProductOrders",
                columns: new[] { "UserId", "Date" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "ProductId", "PaymentMethod", "Subtotal", "ShippingHandling", "Discount", "Tax", "Total", "IsUpsell" });

            migrationBuilder.CreateIndex(
                name: "IX_PricePoints_ProductId",
                table: "PricePoints",
                column: "ProductId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "ProductPriceId", "ImageId", "Header", "Quantity", "ShippingValue", "ShippingType", "Info", "Subheader", "Text" });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PageType",
                table: "Pages",
                column: "PageType")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Content" });

            migrationBuilder.CreateIndex(
                name: "IX_PageKeywords_KeywordId",
                table: "PageKeywords",
                column: "KeywordId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "PageId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_Name",
                table: "OrderProducts",
                column: "Name")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "OrderId", "LineItemType" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "Name", "Quantity", "Price", "LineItemType", "RebillFrequency", "RebillAmount" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ListId",
                table: "Notifications",
                column: "ListId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "ProductId", "UserId", "ReviewId", "Name", "UserImage", "Text", "NonAccountName", "NonAccountEmail", "IsArchived", "CreationDate", "NotificationGroupId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Type",
                table: "Notifications",
                column: "Type")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "NotificationGroupId", "Text" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Type_NonAccountEmail",
                table: "Notifications",
                columns: new[] { "Type", "NonAccountEmail" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "NotificationGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Type_ProductId",
                table: "Notifications",
                columns: new[] { "Type", "ProductId" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "NotificationGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Type_UserId",
                table: "Notifications",
                columns: new[] { "Type", "UserId" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "NotificationGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "ProductId", "ListId", "ReviewId", "Name", "UserImage", "Text", "NonAccountName", "NonAccountEmail", "IsArchived", "CreationDate", "NotificationGroupId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Niches_Disabled_Name",
                table: "Niches",
                columns: new[] { "Disabled", "Name" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "UrlName" });

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CollaborateId",
                table: "Lists",
                column: "CollaborateId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "Name", "Description", "CreationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Lists_Id",
                table: "Lists",
                column: "Id")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Name", "Description", "CollaborateId", "CreationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_Name",
                table: "Keywords",
                column: "Name")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Filters_Name",
                table: "Filters",
                column: "Name")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_FilterOptions_ParamValue",
                table: "FilterOptions",
                column: "ParamValue")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Type",
                table: "Emails",
                column: "Type")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Name", "Content" });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ListId",
                table: "Collaborators",
                column: "ListId")
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "UserId", "IsOwner", "CanAddToList", "CanShareList", "CanEditList", "CanInviteCollaborators", "CanDeleteList", "CanRemoveFromList", "CanManageCollaborators" });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ListId_IsOwner",
                table: "Collaborators",
                columns: new[] { "ListId", "IsOwner" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ListId_UserId",
                table: "Collaborators",
                columns: new[] { "ListId", "UserId" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId_IsOwner",
                table: "Collaborators",
                columns: new[] { "UserId", "IsOwner" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "ListId", "Id", "CanAddToList", "CanShareList", "CanEditList", "CanDeleteList", "CanInviteCollaborators", "CanRemoveFromList", "CanManageCollaborators" });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId_ListId",
                table: "Collaborators",
                columns: new[] { "UserId", "ListId" })
                .Annotation("SqlServer:Clustered", false)
                .Annotation("SqlServer:Include", new[] { "Id", "IsOwner", "CanAddToList", "CanShareList", "CanEditList", "CanInviteCollaborators", "CanDeleteList", "CanRemoveFromList", "CanManageCollaborators" });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedNonAccountUsers_Email",
                table: "BlockedNonAccountUsers",
                column: "Email")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TrackingCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Subproducts_ProductId",
                table: "Subproducts");

            migrationBuilder.DropIndex(
                name: "IX_Subniches_NicheId_Disabled_Name",
                table: "Subniches");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Id_UserId_DeviceId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Products_Disabled",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubnicheId_Disabled",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TrackingCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductId_Likes_Rating_Deleted",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_ProductId_UserId",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_UserId",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_UserId_Date",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_PricePoints_ProductId",
                table: "PricePoints");

            migrationBuilder.DropIndex(
                name: "IX_Pages_PageType",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_PageKeywords_KeywordId",
                table: "PageKeywords");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_Name",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ListId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Type",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Type_NonAccountEmail",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Type_ProductId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Type_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Niches_Disabled_Name",
                table: "Niches");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CollaborateId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_Id",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Keywords_Name",
                table: "Keywords");

            migrationBuilder.DropIndex(
                name: "IX_Filters_Name",
                table: "Filters");

            migrationBuilder.DropIndex(
                name: "IX_FilterOptions_ParamValue",
                table: "FilterOptions");

            migrationBuilder.DropIndex(
                name: "IX_Emails_Type",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_ListId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_ListId_IsOwner",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_ListId_UserId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_UserId_IsOwner",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_UserId_ListId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_BlockedNonAccountUsers_Email",
                table: "BlockedNonAccountUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Subproducts_ProductId",
                table: "Subproducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Subniches_NicheId",
                table: "Subniches",
                column: "NicheId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubnicheId",
                table: "Products",
                column: "SubnicheId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_UserId",
                table: "ProductOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePoints_ProductId",
                table: "PricePoints",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PageKeywords_KeywordId",
                table: "PageKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ListId",
                table: "Notifications",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ListId",
                table: "Collaborators",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");
        }
    }
}
