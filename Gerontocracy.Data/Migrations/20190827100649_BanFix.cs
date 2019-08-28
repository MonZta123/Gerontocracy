using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerontocracy.Data.Migrations
{
    public partial class BanFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ban_AspNetUsers_UnbannedById",
                table: "Ban");

            migrationBuilder.AlterColumn<long>(
                name: "UnbannedById",
                table: "Ban",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Ban_AspNetUsers_UnbannedById",
                table: "Ban",
                column: "UnbannedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ban_AspNetUsers_UnbannedById",
                table: "Ban");

            migrationBuilder.AlterColumn<long>(
                name: "UnbannedById",
                table: "Ban",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ban_AspNetUsers_UnbannedById",
                table: "Ban",
                column: "UnbannedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
