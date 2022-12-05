using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.Infrastructure.Migrations
{
    public partial class PageKeywords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "KeywordsInKeywordGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PageKeywords",
                columns: table => new
                {
                    PageId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    KeywordInKeywordGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageKeywords", x => new { x.PageId, x.KeywordInKeywordGroupId });
                    table.ForeignKey(
                        name: "FK_PageKeywords_KeywordsInKeywordGroup_KeywordInKeywordGroupId",
                        column: x => x.KeywordInKeywordGroupId,
                        principalTable: "KeywordsInKeywordGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageKeywords_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeywordsInKeywordGroup_KeywordGroupId",
                table: "KeywordsInKeywordGroup",
                column: "KeywordGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PageKeywords_KeywordInKeywordGroupId",
                table: "PageKeywords",
                column: "KeywordInKeywordGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageKeywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.DropIndex(
                name: "IX_KeywordsInKeywordGroup_KeywordGroupId",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "KeywordsInKeywordGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeywordsInKeywordGroup",
                table: "KeywordsInKeywordGroup",
                columns: new[] { "KeywordGroupId", "KeywordId" });
        }
    }
}
