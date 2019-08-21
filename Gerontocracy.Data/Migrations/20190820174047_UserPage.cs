using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerontocracy.Data.Migrations
{
    public partial class UserPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aufgabe_AspNetUsers_UebernommenId",
                table: "Aufgabe");

            migrationBuilder.RenameColumn(
                name: "UebernommenId",
                table: "Aufgabe",
                newName: "BearbeiterId");

            migrationBuilder.RenameIndex(
                name: "IX_Aufgabe_UebernommenId",
                table: "Aufgabe",
                newName: "IX_Aufgabe_BearbeiterId");

            migrationBuilder.AddColumn<string>(
                name: "MetaData",
                table: "Aufgabe",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aufgabe_AspNetUsers_BearbeiterId",
                table: "Aufgabe",
                column: "BearbeiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aufgabe_AspNetUsers_BearbeiterId",
                table: "Aufgabe");

            migrationBuilder.DropColumn(
                name: "MetaData",
                table: "Aufgabe");

            migrationBuilder.RenameColumn(
                name: "BearbeiterId",
                table: "Aufgabe",
                newName: "UebernommenId");

            migrationBuilder.RenameIndex(
                name: "IX_Aufgabe_BearbeiterId",
                table: "Aufgabe",
                newName: "IX_Aufgabe_UebernommenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aufgabe_AspNetUsers_UebernommenId",
                table: "Aufgabe",
                column: "UebernommenId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
