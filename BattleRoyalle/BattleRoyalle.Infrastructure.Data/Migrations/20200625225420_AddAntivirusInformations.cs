using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyalle.Infrastructure.Data.Migrations
{
    public partial class AddAntivirusInformations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AntivirusName",
                table: "Machines",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAntivirus",
                table: "Machines",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntivirusName",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "HasAntivirus",
                table: "Machines");
        }
    }
}
