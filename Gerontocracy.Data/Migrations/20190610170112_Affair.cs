using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Gerontocracy.Data.Migrations
{
    public partial class Affair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vorfall",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Titel = table.Column<string>(nullable: true),
                    Beschreibung = table.Column<string>(nullable: true),
                    ErstelltAm = table.Column<DateTime>(nullable: false),
                    ReputationType = table.Column<int>(nullable: false),
                    PolitikerId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorfall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vorfall_Politiker_PolitikerId",
                        column: x => x.PolitikerId,
                        principalTable: "Politiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vorfall_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quelle",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Url = table.Column<string>(nullable: true),
                    Zusatz = table.Column<string>(nullable: true),
                    VorfallId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quelle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quelle_Vorfall_VorfallId",
                        column: x => x.VorfallId,
                        principalTable: "Vorfall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VoteType = table.Column<int>(nullable: false),
                    VorfallId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    QuelleId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_Quelle_QuelleId",
                        column: x => x.QuelleId,
                        principalTable: "Quelle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vote_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vote_Vorfall_VorfallId",
                        column: x => x.VorfallId,
                        principalTable: "Vorfall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quelle_VorfallId",
                table: "Quelle",
                column: "VorfallId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorfall_PolitikerId",
                table: "Vorfall",
                column: "PolitikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorfall_UserId",
                table: "Vorfall",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_QuelleId",
                table: "Vote",
                column: "QuelleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_UserId",
                table: "Vote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VorfallId",
                table: "Vote",
                column: "VorfallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Quelle");

            migrationBuilder.DropTable(
                name: "Vorfall");
        }
    }
}
