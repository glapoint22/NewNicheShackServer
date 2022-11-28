using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.Infrastructure.Migrations
{
    public partial class Media : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ThumbnailWidth = table.Column<int>(type: "int", nullable: false),
                    ThumbnailHeight = table.Column<int>(type: "int", nullable: false),
                    ImageSm = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageSmWidth = table.Column<int>(type: "int", nullable: false),
                    ImageSmHeight = table.Column<int>(type: "int", nullable: false),
                    ImageMd = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageMdWidth = table.Column<int>(type: "int", nullable: false),
                    ImageMdHeight = table.Column<int>(type: "int", nullable: false),
                    ImageLg = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageLgWidth = table.Column<int>(type: "int", nullable: false),
                    ImageLgHeight = table.Column<int>(type: "int", nullable: false),
                    ImageAnySize = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageAnySizeWidth = table.Column<int>(type: "int", nullable: false),
                    ImageAnySizeHeight = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VideoType = table.Column<int>(type: "int", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}
