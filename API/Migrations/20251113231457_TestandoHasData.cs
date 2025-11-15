using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SWAPI_Minimal.Migrations
{
    /// <inheritdoc />
    public partial class TestandoHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "Id", "Email", "Perfil", "Senha" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111112"), "viewerteste@teste.com", "Viewer", "123456" });

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "Ano", "Tipo", "Titulo" },
                values: new object[,]
                {
                    { 1, new DateTime(1999, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filme", "A Ameaça Fantasma" },
                    { 2, new DateTime(2002, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filme", "O Ataque dos Clones" },
                    { 3, new DateTime(2005, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filme", "A Vingança dos Sith" },
                    { 4, new DateTime(1977, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filme", "Uma Nova Esperança" },
                    { 5, new DateTime(1980, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filme", "O Império Contra-Ataca" },
                    { 6, new DateTime(1983, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filme", "O Retorno de Jedi" }
                });

            migrationBuilder.InsertData(
                table: "Planetas",
                columns: new[] { "Id", "Clima", "Nome", "Terreno" },
                values: new object[,]
                {
                    { 1, "Árido", "Tatooine", "Desértico" },
                    { 2, "Temperado", "Stewjon", "Verdejante" },
                    { 3, "Temperado", "Alderaan", "Pastagem, cordilheira" }
                });

            migrationBuilder.InsertData(
                table: "Personagens",
                columns: new[] { "Id", "Altura", "CorCabelo", "CorOlhos", "CorPele", "DataNascimento", "Genero", "Nome", "PlanetaID" },
                values: new object[,]
                {
                    { 1, 1.8200000000000001, "Castanho", "Azul", "Clara", "57ABY", "Masculino", "Obi-Wan Kenobi", 2 },
                    { 2, 1.5, "Castanho", "Castanho", "Clara", "19ABY", "Feminino", "Princesa Leia Organa", 3 },
                    { 3, 1.8799999999999999, "Castanho", "Azul", "Clara", "41.9ABY", "Masculino", "Anakin Skywalker", 1 }
                });

            migrationBuilder.InsertData(
                table: "PersonagemFilme",
                columns: new[] { "FilmesId", "PersonagensId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 2 },
                    { 5, 1 },
                    { 5, 2 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administradores",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "PersonagemFilme",
                keyColumns: new[] { "FilmesId", "PersonagensId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Filmes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Planetas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Planetas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Planetas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
