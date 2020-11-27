using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebImageCloud.Migrations
{
    public partial class ByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_IFile_ExtualyFileId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "IFile");

            migrationBuilder.DropIndex(
                name: "IX_Files_ExtualyFileId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ExtualyFileId",
                table: "Files");

            migrationBuilder.AddColumn<byte[]>(
                name: "ExtualyFile",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtualyFile",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "ExtualyFileId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_ExtualyFileId",
                table: "Files",
                column: "ExtualyFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_IFile_ExtualyFileId",
                table: "Files",
                column: "ExtualyFileId",
                principalTable: "IFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
