using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Gerontocracy.Data.Migrations
{
    public partial class Tasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aufgabe",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TaskType = table.Column<int>(nullable: false),
                    Beschreibung = table.Column<string>(nullable: true),
                    EingereichtAm = table.Column<DateTime>(nullable: false),
                    Erledigt = table.Column<bool>(nullable: false),
                    EinreicherId = table.Column<long>(nullable: false),
                    UebernommenId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aufgabe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aufgabe_AspNetUsers_EinreicherId",
                        column: x => x.EinreicherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aufgabe_AspNetUsers_UebernommenId",
                        column: x => x.UebernommenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aufgabe_EinreicherId",
                table: "Aufgabe",
                column: "EinreicherId");

            migrationBuilder.CreateIndex(
                name: "IX_Aufgabe_UebernommenId",
                table: "Aufgabe",
                column: "UebernommenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aufgabe");
        }
    }
}
