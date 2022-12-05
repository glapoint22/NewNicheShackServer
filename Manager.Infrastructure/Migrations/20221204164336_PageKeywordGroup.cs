using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.Infrastructure.Migrations
{
    public partial class PageKeywordGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageKeywords");

            migrationBuilder.CreateTable(
                name: "PageKeywordGroups",
                columns: table => new
                {
                    PageId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    KeywordGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageKeywordGroups", x => new { x.PageId, x.KeywordGroupId });
                    table.ForeignKey(
                        name: "FK_PageKeywordGroups_KeywordGroups_KeywordGroupId",
                        column: x => x.KeywordGroupId,
                        principalTable: "KeywordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageKeywordGroups_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageKeywordGroups_KeywordGroupId",
                table: "PageKeywordGroups",
                column: "KeywordGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageKeywordGroups");

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

            migrationBuilder.CreateIndex(
                name: "IX_PageKeywords_KeywordId",
                table: "PageKeywords",
                column: "KeywordId");
        }
    }
}
