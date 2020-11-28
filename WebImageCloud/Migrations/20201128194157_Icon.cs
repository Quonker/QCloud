using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebImageCloud.Migrations
{
    public partial class Icon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FolderViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DateOfCreate = table.Column<DateTime>(nullable: false),
                    DateOfChange = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderViewModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderViewModel_UserId",
                table: "FolderViewModel",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderViewModel");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Files");
        }
    }
}
