using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Infrastructure.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "BlockNotificationSending",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnAddedListItem",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnDeletedList",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnEditedList",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnEmailChange",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnMovedListItem",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnNameChange",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnNewCollaborator",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnPasswordChange",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnProfileImageChange",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnRemovedCollaborator",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnRemovedListItem",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnReview",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingCode",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BlockedNonAccountEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedNonAccountEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeywordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollaborateId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageSm = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageMd = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageLg = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageAnySize = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VideoId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    VideoType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Min = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilterId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterOptions_Filters_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordSearchVolumes",
                columns: table => new
                {
                    KeywordId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordSearchVolumes", x => new { x.KeywordId, x.Date });
                    table.ForeignKey(
                        name: "FK_KeywordSearchVolumes_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordsInKeywordGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeywordGroupId = table.Column<int>(type: "int", nullable: false),
                    KeywordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordsInKeywordGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeywordsInKeywordGroup_KeywordGroups_KeywordGroupId",
                        column: x => x.KeywordGroupId,
                        principalTable: "KeywordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordsInKeywordGroup_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    CanAddToList = table.Column<bool>(type: "bit", nullable: false),
                    CanShareList = table.Column<bool>(type: "bit", nullable: false),
                    CanEditList = table.Column<bool>(type: "bit", nullable: false),
                    CanInviteCollaborators = table.Column<bool>(type: "bit", nullable: false),
                    CanDeleteList = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoveFromList = table.Column<bool>(type: "bit", nullable: false),
                    CanManageCollaborators = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborators_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subniches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NicheId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subniches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subniches_Niches_NicheId",
                        column: x => x.NicheId,
                        principalTable: "Niches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageReferenceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SubnicheId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    KeywordGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageReferenceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageReferenceItems_KeywordGroups_KeywordGroupId",
                        column: x => x.KeywordGroupId,
                        principalTable: "KeywordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageReferenceItems_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageReferenceItems_Subniches_SubnicheId",
                        column: x => x.SubnicheId,
                        principalTable: "Subniches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SubnicheId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hoplink = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TotalReviews = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    OneStar = table.Column<int>(type: "int", nullable: false),
                    TwoStars = table.Column<int>(type: "int", nullable: false),
                    ThreeStars = table.Column<int>(type: "int", nullable: false),
                    FourStars = table.Column<int>(type: "int", nullable: false),
                    FiveStars = table.Column<int>(type: "int", nullable: false),
                    ShippingType = table.Column<int>(type: "int", nullable: false),
                    TrialPeriod = table.Column<int>(type: "int", nullable: false),
                    RecurringPrice = table.Column<double>(type: "float", nullable: false),
                    RebillFrequency = table.Column<int>(type: "int", nullable: false),
                    TimeFrameBetweenRebill = table.Column<int>(type: "int", nullable: false),
                    SubscriptionDuration = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    TrackingCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Media_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Subniches_SubnicheId",
                        column: x => x.SubnicheId,
                        principalTable: "Subniches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListProducts",
                columns: table => new
                {
                    ListId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListProducts", x => new { x.ListId, x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ListProducts_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFilters",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    FilterOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFilters", x => new { x.ProductId, x.FilterOptionId });
                    table.ForeignKey(
                        name: "FK_ProductFilters_FilterOptions_FilterOptionId",
                        column: x => x.FilterOptionId,
                        principalTable: "FilterOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFilters_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductKeywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    KeywordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductKeywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductKeywords_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductKeywords_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMedia_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductMedia_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<double>(type: "float", nullable: false),
                    ShippingHandling = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subproducts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subproducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subproducts_Media_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subproducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    RebillFrequency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RebillAmount = table.Column<double>(type: "float", nullable: false),
                    PaymentsRemaining = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_ProductOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ProductOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPriceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    Header = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UnitPrice = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    StrikethroughPrice = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ShippingType = table.Column<int>(type: "int", nullable: false),
                    TrialPeriod = table.Column<int>(type: "int", nullable: false),
                    RecurringPrice = table.Column<double>(type: "float", nullable: false),
                    RebillFrequency = table.Column<int>(type: "int", nullable: false),
                    TimeFrameBetweenRebill = table.Column<int>(type: "int", nullable: false),
                    SubscriptionDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricePoints_Media_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PricePoints_ProductPrices_ProductPriceId",
                        column: x => x.ProductPriceId,
                        principalTable: "ProductPrices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PricePoints_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NonAccountName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NonAccountEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationGroups_NotificationGroupId",
                        column: x => x.NotificationGroupId,
                        principalTable: "NotificationGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_ProductReviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "ProductReviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ListId",
                table: "Collaborators",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterOptions_FilterId",
                table: "FilterOptions",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordsInKeywordGroup_KeywordGroupId",
                table: "KeywordsInKeywordGroup",
                column: "KeywordGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordsInKeywordGroup_KeywordId",
                table: "KeywordsInKeywordGroup",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_ListProducts_ProductId",
                table: "ListProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ListProducts_UserId",
                table: "ListProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationGroupId",
                table: "Notifications",
                column: "NotificationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProductId",
                table: "Notifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReviewId",
                table: "Notifications",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PageReferenceItems_KeywordGroupId",
                table: "PageReferenceItems",
                column: "KeywordGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PageReferenceItems_PageId",
                table: "PageReferenceItems",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageReferenceItems_SubnicheId",
                table: "PageReferenceItems",
                column: "SubnicheId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePoints_ImageId",
                table: "PricePoints",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePoints_ProductId",
                table: "PricePoints",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePoints_ProductPriceId",
                table: "PricePoints",
                column: "ProductPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFilters_FilterOptionId",
                table: "ProductFilters",
                column: "FilterOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKeywords_KeywordId",
                table: "ProductKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKeywords_ProductId",
                table: "ProductKeywords",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_MediaId",
                table: "ProductMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_ProductId",
                table: "ProductMedia",
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
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubnicheId",
                table: "Products",
                column: "SubnicheId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subniches_NicheId",
                table: "Subniches",
                column: "NicheId");

            migrationBuilder.CreateIndex(
                name: "IX_Subproducts_ImageId",
                table: "Subproducts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Subproducts_ProductId",
                table: "Subproducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedNonAccountEmails");

            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "KeywordSearchVolumes");

            migrationBuilder.DropTable(
                name: "KeywordsInKeywordGroup");

            migrationBuilder.DropTable(
                name: "ListProducts");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "PageReferenceItems");

            migrationBuilder.DropTable(
                name: "PricePoints");

            migrationBuilder.DropTable(
                name: "PriceRanges");

            migrationBuilder.DropTable(
                name: "ProductFilters");

            migrationBuilder.DropTable(
                name: "ProductKeywords");

            migrationBuilder.DropTable(
                name: "ProductMedia");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Subproducts");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropTable(
                name: "NotificationGroups");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "KeywordGroups");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "FilterOptions");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Subniches");

            migrationBuilder.DropTable(
                name: "Niches");

            migrationBuilder.DropColumn(
                name: "BlockNotificationSending",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnAddedListItem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnDeletedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnEditedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnEmailChange",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnMovedListItem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnNameChange",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnNewCollaborator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnPasswordChange",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnProfileImageChange",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnRemovedCollaborator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnRemovedListItem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnReview",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TrackingCode",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);
        }
    }
}
