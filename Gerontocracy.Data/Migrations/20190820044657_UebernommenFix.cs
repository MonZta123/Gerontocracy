using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerontocracy.Data.Migrations
{
    public partial class UebernommenFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aufgabe_AspNetUsers_UebernommenId",
                table: "Aufgabe");

            migrationBuilder.AlterColumn<long>(
                name: "UebernommenId",
                table: "Aufgabe",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Aufgabe_AspNetUsers_UebernommenId",
                table: "Aufgabe",
                column: "UebernommenId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aufgabe_AspNetUsers_UebernommenId",
                table: "Aufgabe");

            migrationBuilder.AlterColumn<long>(
                name: "UebernommenId",
                table: "Aufgabe",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aufgabe_AspNetUsers_UebernommenId",
                table: "Aufgabe",
                column: "UebernommenId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
