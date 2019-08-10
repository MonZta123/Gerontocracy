using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerontocracy.Data.Migrations
{
    public partial class RemoveVoteFromQuelle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Quelle_QuelleId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_QuelleId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "QuelleId",
                table: "Vote");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuelleId",
                table: "Vote",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_QuelleId",
                table: "Vote",
                column: "QuelleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Quelle_QuelleId",
                table: "Vote",
                column: "QuelleId",
                principalTable: "Quelle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
