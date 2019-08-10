using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerontocracy.Data.Migrations
{
    public partial class AffairChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vorfall_Politiker_PolitikerId",
                table: "Vorfall");

            migrationBuilder.AlterColumn<int>(
                name: "ReputationType",
                table: "Vorfall",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "PolitikerId",
                table: "Vorfall",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Vorfall_Politiker_PolitikerId",
                table: "Vorfall",
                column: "PolitikerId",
                principalTable: "Politiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vorfall_Politiker_PolitikerId",
                table: "Vorfall");

            migrationBuilder.AlterColumn<int>(
                name: "ReputationType",
                table: "Vorfall",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PolitikerId",
                table: "Vorfall",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vorfall_Politiker_PolitikerId",
                table: "Vorfall",
                column: "PolitikerId",
                principalTable: "Politiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
