using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWAPI_Minimal.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelasStarWars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador");

            migrationBuilder.RenameTable(
                name: "Administrador",
                newName: "Administradores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Ano = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Altura = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CorCabelo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CorPele = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CorOlhos = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    DataNascimento = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Genero = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    PlanetaID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personagens_Planetas_PlanetaID",
                        column: x => x.PlanetaID,
                        principalTable: "Planetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemFilme",
                columns: table => new
                {
                    FilmesId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonagensId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemFilme", x => new { x.FilmesId, x.PersonagensId });
                    table.ForeignKey(
                        name: "FK_PersonagemFilme_Filmes_FilmesId",
                        column: x => x.FilmesId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemFilme_Personagens_PersonagensId",
                        column: x => x.PersonagensId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemFilme_PersonagensId",
                table: "PersonagemFilme",
                column: "PersonagensId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_PlanetaID",
                table: "Personagens",
                column: "PlanetaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonagemFilme");

            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "Personagens");

            migrationBuilder.DropTable(
                name: "Planetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores");

            migrationBuilder.RenameTable(
                name: "Administradores",
                newName: "Administrador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador",
                column: "Id");
        }
    }
}
