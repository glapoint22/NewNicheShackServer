using System;
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

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ListId",
                table: "Collaborators",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Lists");

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
