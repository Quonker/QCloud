using Microsoft.EntityFrameworkCore.Migrations;

namespace WebImageCloud.Migrations
{
    public partial class Temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "FolderViewModel",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UseStorage",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "FolderViewModel");

            migrationBuilder.DropColumn(
                name: "UseStorage",
                table: "AspNetUsers");
        }
    }
}
