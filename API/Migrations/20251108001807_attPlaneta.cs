using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWAPI_Minimal.Migrations
{
    /// <inheritdoc />
    public partial class attPlaneta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Clima",
                table: "Planetas",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Terreno",
                table: "Planetas",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clima",
                table: "Planetas");

            migrationBuilder.DropColumn(
                name: "Terreno",
                table: "Planetas");
        }
    }
}
