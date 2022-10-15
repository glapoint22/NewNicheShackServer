﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Infrastructure.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "EmailOnEmailChange",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnListNameChange",
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

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niches", x => x.Id);
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
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    AddToList = table.Column<bool>(type: "bit", nullable: false),
                    ShareList = table.Column<bool>(type: "bit", nullable: false),
                    EditList = table.Column<bool>(type: "bit", nullable: false),
                    InviteCollaborators = table.Column<bool>(type: "bit", nullable: false),
                    DeleteList = table.Column<bool>(type: "bit", nullable: false),
                    MoveItem = table.Column<bool>(type: "bit", nullable: false),
                    RemoveItem = table.Column<bool>(type: "bit", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NicheId = table.Column<int>(type: "int", nullable: false),
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubnicheId = table.Column<int>(type: "int", nullable: false),
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "CollaboratorProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CollaboratorId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorProducts", x => new { x.ProductId, x.CollaboratorId });
                    table.ForeignKey(
                        name: "FK_CollaboratorProducts_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorProducts_CollaboratorId",
                table: "CollaboratorProducts",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ListId",
                table: "Collaborators",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorProducts");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Subniches");

            migrationBuilder.DropTable(
                name: "Niches");

            migrationBuilder.DropColumn(
                name: "EmailOnAddedListItem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnDeletedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnEmailChange",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnListNameChange",
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
        }
    }
}