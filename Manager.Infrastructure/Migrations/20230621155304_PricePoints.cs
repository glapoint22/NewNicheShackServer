using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.Infrastructure.Migrations
{
    public partial class PricePoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RebillFrequency",
                table: "PricePoints");

            migrationBuilder.DropColumn(
                name: "RecurringPrice",
                table: "PricePoints");

            migrationBuilder.DropColumn(
                name: "StrikethroughPrice",
                table: "PricePoints");

            migrationBuilder.DropColumn(
                name: "SubscriptionDuration",
                table: "PricePoints");

            migrationBuilder.DropColumn(
                name: "TimeFrameBetweenRebill",
                table: "PricePoints");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "PricePoints");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "PricePoints",
                newName: "ShippingValue");

            migrationBuilder.RenameColumn(
                name: "TrialPeriod",
                table: "PricePoints",
                newName: "Info");

            migrationBuilder.AddColumn<string>(
                name: "Subheader",
                table: "PricePoints",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "PricePoints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subheader",
                table: "PricePoints");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "PricePoints");

            migrationBuilder.RenameColumn(
                name: "ShippingValue",
                table: "PricePoints",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "PricePoints",
                newName: "TrialPeriod");

            migrationBuilder.AddColumn<int>(
                name: "RebillFrequency",
                table: "PricePoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RecurringPrice",
                table: "PricePoints",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StrikethroughPrice",
                table: "PricePoints",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionDuration",
                table: "PricePoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeFrameBetweenRebill",
                table: "PricePoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "PricePoints",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }
    }
}
