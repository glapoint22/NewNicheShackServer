using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Infrastructure.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailOnReview",
                table: "Users",
                newName: "EmailOnUserUpdatedList");

            migrationBuilder.RenameColumn(
                name: "EmailOnRemovedListItem",
                table: "Users",
                newName: "EmailOnUserRemovedListItem");

            migrationBuilder.RenameColumn(
                name: "EmailOnRemovedCollaborator",
                table: "Users",
                newName: "EmailOnUserRemovedFromList");

            migrationBuilder.RenameColumn(
                name: "EmailOnProfileImageChange",
                table: "Users",
                newName: "EmailOnUserRemovedCollaborator");

            migrationBuilder.RenameColumn(
                name: "EmailOnPasswordChange",
                table: "Users",
                newName: "EmailOnUserMovedListItem");

            migrationBuilder.RenameColumn(
                name: "EmailOnNewCollaborator",
                table: "Users",
                newName: "EmailOnUserJoinedList");

            migrationBuilder.RenameColumn(
                name: "EmailOnNameChange",
                table: "Users",
                newName: "EmailOnUserDeletedList");

            migrationBuilder.RenameColumn(
                name: "EmailOnMovedListItem",
                table: "Users",
                newName: "EmailOnUserAddedListItem");

            migrationBuilder.RenameColumn(
                name: "EmailOnEmailChange",
                table: "Users",
                newName: "EmailOnProfileImageUpdated");

            migrationBuilder.RenameColumn(
                name: "EmailOnEditedList",
                table: "Users",
                newName: "EmailOnPasswordUpdated");

            migrationBuilder.RenameColumn(
                name: "EmailOnDeletedList",
                table: "Users",
                newName: "EmailOnNameUpdated");

            migrationBuilder.RenameColumn(
                name: "EmailOnAddedListItem",
                table: "Users",
                newName: "EmailOnItemReviewed");

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnCollaboratorAddedListItem",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnCollaboratorDeletedList",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnCollaboratorJoinedList",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnCollaboratorMovedListItem",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnCollaboratorRemovedFromList",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnCollaboratorRemovedListItem",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnCollaboratorUpdatedList",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailOnEmailUpdated",
                table: "Users",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Lists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOnCollaboratorAddedListItem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnCollaboratorDeletedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnCollaboratorJoinedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnCollaboratorMovedListItem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnCollaboratorRemovedFromList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnCollaboratorRemovedListItem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnCollaboratorUpdatedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOnEmailUpdated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Lists");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserUpdatedList",
                table: "Users",
                newName: "EmailOnReview");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserRemovedListItem",
                table: "Users",
                newName: "EmailOnRemovedListItem");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserRemovedFromList",
                table: "Users",
                newName: "EmailOnRemovedCollaborator");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserRemovedCollaborator",
                table: "Users",
                newName: "EmailOnProfileImageChange");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserMovedListItem",
                table: "Users",
                newName: "EmailOnPasswordChange");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserJoinedList",
                table: "Users",
                newName: "EmailOnNewCollaborator");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserDeletedList",
                table: "Users",
                newName: "EmailOnNameChange");

            migrationBuilder.RenameColumn(
                name: "EmailOnUserAddedListItem",
                table: "Users",
                newName: "EmailOnMovedListItem");

            migrationBuilder.RenameColumn(
                name: "EmailOnProfileImageUpdated",
                table: "Users",
                newName: "EmailOnEmailChange");

            migrationBuilder.RenameColumn(
                name: "EmailOnPasswordUpdated",
                table: "Users",
                newName: "EmailOnEditedList");

            migrationBuilder.RenameColumn(
                name: "EmailOnNameUpdated",
                table: "Users",
                newName: "EmailOnDeletedList");

            migrationBuilder.RenameColumn(
                name: "EmailOnItemReviewed",
                table: "Users",
                newName: "EmailOnAddedListItem");
        }
    }
}
