using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.ENUMs;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.ModelViews;
using SWAPI_Minimal.Dominio.Servicos;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Controllers;

public static class FilmesEndpoint 
{
    public static void FilmesEndpoints(this WebApplication app)
    {
        var FilmesGroup = app.MapGroup("/Filmes");

        FilmesGroup.MapGet("/{id}", async ([FromRoute] int id, [FromServices] IFilmes filmesServicos) =>
        {
            var filmes = await filmesServicos.GetIdAsync(id);
            if (filmes == null) return Results.NotFound();

            return Results.Ok(new FilmesMV(filmes.Id, filmes.Titulo, filmes.Tipo, DateOnly.FromDateTime(filmes.Ano)));
        }).WithTags("Filme");

        FilmesGroup.MapGet("/", async ([FromQuery] int pagina, [FromServices] IFilmes filmesServicos) =>
        {
            var filmesLista = new List<Filme>();
            var Filmes = await filmesServicos.ListarAsync(pagina);

            foreach (var filme in Filmes)
            {
                filmesLista.Add(filme);
            }
            return Results.Ok(filmesLista);
            
        }).WithTags("Filme");

        FilmesGroup.MapPost("/create", async ([FromBody] FilmesDTOs filmesDTOs, IFilmes filmesServicos, DbContexto ctx) =>
        {
            var anoAtual = DateOnly.FromDateTime(DateTime.Now).Year;
            
            
            var erroValidacao = new ErroValidacao
            {
                Erros = new List<string>()
            };

            var filmesChecagem = await ctx.Filmes.AnyAsync(f => filmesDTOs.titulo == f.Titulo);
            if (filmesChecagem) erroValidacao.Erros.Add($"A obra {filmesDTOs.titulo} já existe, por favor, verifique o título.");

            if (string.IsNullOrEmpty(filmesDTOs.titulo)) 
                erroValidacao.Erros.Add("O Titulo não pode ser em branco");
            if (filmesDTOs.anoLancado == default(DateOnly))
                erroValidacao.Erros.Add("O Ano do filme não pode ser em branco");
            if (filmesDTOs.anoLancado.Year < 1977)
                erroValidacao.Erros.Add("Filme muito antigo");
            if (filmesDTOs.anoLancado.Year > anoAtual)
                erroValidacao.Erros.Add("Não é possível adicionar algo do futuro");
            
            if (erroValidacao.Erros.Count > 0) return Results.BadRequest(erroValidacao);

            var filme = new Filme
            {
                Titulo = filmesDTOs.titulo,
                Tipo = filmesDTOs.Tipo.ToString(),
                Ano = filmesDTOs.anoLancado.ToDateTime(TimeOnly.MinValue),
            };
            await filmesServicos.CriarAsync(filme);
            
            return Results.Created($"/filmes/{filme.Id}", new FilmesDTOs(filme.Id, filme.Titulo, filmesDTOs.Tipo, filmesDTOs.anoLancado));

        }).WithTags("Filme");

    }
}