using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWAPI_Minimal.Migrations
{
    /// <inheritdoc />
    public partial class InitialADM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Perfil = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "Id", "Email", "Perfil", "Senha" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "admteste@teste.com", "Adm", "123456" });

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
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "PersonagemFilme");

            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "Personagens");

            migrationBuilder.DropTable(
                name: "Planetas");
        }
    }
}
