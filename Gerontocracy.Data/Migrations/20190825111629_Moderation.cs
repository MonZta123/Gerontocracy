using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Gerontocracy.Data.Migrations
{
    public partial class Moderation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Thread",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Post",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Ban",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BanDate = table.Column<DateTime>(nullable: false),
                    BanEnd = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    BanLifted = table.Column<DateTime>(nullable: true),
                    BanLiftReason = table.Column<string>(nullable: true),
                    BannedUserId = table.Column<long>(nullable: false),
                    BannedById = table.Column<long>(nullable: false),
                    UnbannedById = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ban_AspNetUsers_BannedById",
                        column: x => x.BannedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ban_AspNetUsers_BannedUserId",
                        column: x => x.BannedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ban_AspNetUsers_UnbannedById",
                        column: x => x.UnbannedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ban_BannedById",
                table: "Ban",
                column: "BannedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ban_BannedUserId",
                table: "Ban",
                column: "BannedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ban_UnbannedById",
                table: "Ban",
                column: "UnbannedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ban");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Thread");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Post");
        }
    }
}
