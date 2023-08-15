using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.Infrastructure.Migrations
{
    public partial class AddedDisabledPropertyToNicheSubnicheAndProductTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Subniches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Niches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Subniches");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Niches");
        }
    }
}
