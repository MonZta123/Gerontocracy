using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerontocracy.Data.Migrations
{
    public partial class ParlamentsTyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotActive",
                table: "Partei");

            migrationBuilder.RenameColumn(
                name: "NotActive",
                table: "Politiker",
                newName: "IsRegierung");

            migrationBuilder.AlterColumn<string>(
                name: "Titel",
                table: "Vorfall",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Beschreibung",
                table: "Vorfall",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNationalrat",
                table: "Politiker",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNationalrat",
                table: "Politiker");

            migrationBuilder.RenameColumn(
                name: "IsRegierung",
                table: "Politiker",
                newName: "NotActive");

            migrationBuilder.AlterColumn<string>(
                name: "Titel",
                table: "Vorfall",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Beschreibung",
                table: "Vorfall",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000);

            migrationBuilder.AddColumn<bool>(
                name: "NotActive",
                table: "Partei",
                nullable: false,
                defaultValue: false);
        }
    }
}
