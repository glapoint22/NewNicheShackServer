using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.Infrastructure.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsInProductGroup",
                table: "ProductsInProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInProductGroup_ProductId",
                table: "ProductsInProductGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductKeywords",
                table: "ProductKeywords");

            migrationBuilder.DropIndex(
                name: "IX_ProductKeywords_ProductId",
                table: "ProductKeywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.DropIndex(
                name: "IX_KeywordsInKeywordGroup_KeywordGroupId",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductsInProductGroup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductKeywords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsInProductGroup",
                table: "ProductsInProductGroup",
                columns: new[] { "ProductId", "ProductGroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductKeywords",
                table: "ProductKeywords",
                columns: new[] { "ProductId", "KeywordId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup",
                columns: new[] { "KeywordGroupId", "KeywordId" });

            migrationBuilder.CreateTable(
                name: "PageKeywords",
                columns: table => new
                {
                    PageId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    KeywordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageKeywords", x => new { x.PageId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_PageKeywords_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageKeywords_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageSubniches",
                columns: table => new
                {
                    PageId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SubnicheId = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSubniches", x => new { x.PageId, x.SubnicheId });
                    table.ForeignKey(
                        name: "FK_PageSubniches_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageSubniches_Subniches_SubnicheId",
                        column: x => x.SubnicheId,
                        principalTable: "Subniches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageKeywords_KeywordId",
                table: "PageKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_PageSubniches_SubnicheId",
                table: "PageSubniches",
                column: "SubnicheId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageKeywords");

            migrationBuilder.DropTable(
                name: "PageSubniches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsInProductGroup",
                table: "ProductsInProductGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductKeywords",
                table: "ProductKeywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductsInProductGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductKeywords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "KeywordsInKeywordGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsInProductGroup",
                table: "ProductsInProductGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductKeywords",
                table: "ProductKeywords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInProductGroup_ProductId",
                table: "ProductsInProductGroup",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductKeywords_ProductId",
                table: "ProductKeywords",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordsInKeywordGroup_KeywordGroupId",
                table: "KeywordsInKeywordGroup",
                column: "KeywordGroupId");
        }
    }
}
