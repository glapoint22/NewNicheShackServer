using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Infrastructure.Migrations
{
    public partial class Lists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListId",
                table: "Notifications",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ListId",
                table: "Notifications",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Lists_ListId",
                table: "Notifications",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Lists_ListId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ListId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Notifications");
        }
    }
}
