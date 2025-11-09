using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWAPI_Minimal.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTipoVar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Altura",
                table: "Personagens",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 150);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Altura",
                table: "Personagens",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
