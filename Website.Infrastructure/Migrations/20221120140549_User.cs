using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Infrastructure.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoncompliantStrikes",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Suspended",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoncompliantStrikes",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Suspended",
                table: "Users");
        }
    }
}
