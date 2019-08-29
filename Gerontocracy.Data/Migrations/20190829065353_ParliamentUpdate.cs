using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Gerontocracy.Data.Migrations
{
    public partial class ParliamentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AkadGradPost",
                table: "Politiker");

            migrationBuilder.DropColumn(
                name: "AkadGradPre",
                table: "Politiker");

            migrationBuilder.DropColumn(
                name: "IsNationalrat",
                table: "Politiker");

            migrationBuilder.DropColumn(
                name: "IsRegierung",
                table: "Politiker");

            migrationBuilder.DropColumn(
                name: "Nachname",
                table: "Politiker");

            migrationBuilder.RenameColumn(
                name: "Vorname",
                table: "Politiker",
                newName: "Name");

            migrationBuilder.AddColumn<long>(
                name: "ParlamentId",
                table: "Politiker",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParlamentId",
                table: "Partei",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RssSourceId",
                table: "Artikel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Parlament",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    Langtext = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parlament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RssSource",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Url = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    ParlamentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssSource_Parlament_ParlamentId",
                        column: x => x.ParlamentId,
                        principalTable: "Parlament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Politiker_ParlamentId",
                table: "Politiker",
                column: "ParlamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Partei_ParlamentId",
                table: "Partei",
                column: "ParlamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikel_RssSourceId",
                table: "Artikel",
                column: "RssSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RssSource_ParlamentId",
                table: "RssSource",
                column: "ParlamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artikel_RssSource_RssSourceId",
                table: "Artikel",
                column: "RssSourceId",
                principalTable: "RssSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partei_Parlament_ParlamentId",
                table: "Partei",
                column: "ParlamentId",
                principalTable: "Parlament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Politiker_Parlament_ParlamentId",
                table: "Politiker",
                column: "ParlamentId",
                principalTable: "Parlament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artikel_RssSource_RssSourceId",
                table: "Artikel");

            migrationBuilder.DropForeignKey(
                name: "FK_Partei_Parlament_ParlamentId",
                table: "Partei");

            migrationBuilder.DropForeignKey(
                name: "FK_Politiker_Parlament_ParlamentId",
                table: "Politiker");

            migrationBuilder.DropTable(
                name: "RssSource");

            migrationBuilder.DropTable(
                name: "Parlament");

            migrationBuilder.DropIndex(
                name: "IX_Politiker_ParlamentId",
                table: "Politiker");

            migrationBuilder.DropIndex(
                name: "IX_Partei_ParlamentId",
                table: "Partei");

            migrationBuilder.DropIndex(
                name: "IX_Artikel_RssSourceId",
                table: "Artikel");

            migrationBuilder.DropColumn(
                name: "ParlamentId",
                table: "Politiker");

            migrationBuilder.DropColumn(
                name: "ParlamentId",
                table: "Partei");

            migrationBuilder.DropColumn(
                name: "RssSourceId",
                table: "Artikel");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Politiker",
                newName: "Vorname");

            migrationBuilder.AddColumn<string>(
                name: "AkadGradPost",
                table: "Politiker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AkadGradPre",
                table: "Politiker",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNationalrat",
                table: "Politiker",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegierung",
                table: "Politiker",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nachname",
                table: "Politiker",
                nullable: true);
        }
    }
}
