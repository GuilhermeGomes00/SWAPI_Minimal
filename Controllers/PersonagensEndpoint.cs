using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.ModelViews;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Controllers;

public static class PersonagensEndpoint
{
    public static void PersonagensEndpoints(this WebApplication app)
    {
        var personagemGroup = app.MapGroup("personagens");

        personagemGroup.MapPost("/create", async (
            [FromBody] PersonagemDTO personagemDTO, 
            [FromServices]IPersonagem personagemServicos, 
            [FromServices]DbContexto ctx) =>
        {
            bool checkPlaneta = await ctx.Planetas.AnyAsync(p => personagemDTO.planetaId == p.Id);
            bool checkNomePersonagem = await ctx.Personagens.AnyAsync(p => personagemDTO.Nome ==  p.Nome);
            var contagemIdFilme = personagemDTO.filmesId;
            var filmesIdUnicos = contagemIdFilme.Distinct().Count();
            var checkFilmes = await ctx.Filmes.CountAsync(f => contagemIdFilme.Contains(f.Id));
            var filmesPorPersonagem = await ctx.Filmes.Where(f => personagemDTO.filmesId.Contains(f.Id)).ToListAsync();
            var erroValidacao = new ErroValidacao
            {
                Erros = new List<string>()
            };
            
            if (string.IsNullOrEmpty(personagemDTO.Nome)) 
                erroValidacao.Erros.Add("O nome de personagem não pode ser nulo. ");
            if (checkNomePersonagem)
                erroValidacao.Erros.Add($"O personagem {personagemDTO.Nome} já existe.");
            if (personagemDTO.planetaId <= 0)
                erroValidacao.Erros.Add($"Insira o Id do planeta em que o personagem {personagemDTO.Nome} nasceu ou foi criado.");
            if (!checkPlaneta)
                erroValidacao.Erros.Add("O planeta inserido precisa estar salvo.");
            if (personagemDTO.filmesId.Count <= 0)
                erroValidacao.Erros.Add($"Insira o Id dos filmes em que o personagem {personagemDTO.Nome} apareceu.");
            if (filmesIdUnicos != checkFilmes)
                erroValidacao.Erros.Add("Um ou mais IDs são inválidos ou não foram encontrados");
                
            if (erroValidacao.Erros.Count > 0) 
                return Results.BadRequest(erroValidacao);

            var personagem = new Personagem
            {
                Nome = personagemDTO.Nome,
                Altura = personagemDTO.Altura,
                CorCabelo = personagemDTO.corCabelo,
                CorPele = personagemDTO.corPele,
                CorOlhos = personagemDTO.corOlhos,
                DataNascimento = personagemDTO.dataNascimento,
                Genero = personagemDTO.Genero,
                PlanetaID = personagemDTO.planetaId,
                Filmes = filmesPorPersonagem
            };
            await personagemServicos.CriarAsync(personagem);
            
            return Results.Created($"/personagens/{personagem.Id}", new PersonagemMV(
                personagemDTO.Nome,
                personagemDTO.Altura,
                personagemDTO.corCabelo,
                personagemDTO.corPele,
                personagemDTO.corOlhos,
                personagemDTO.dataNascimento,
                personagemDTO.Genero,
                personagemDTO.planetaId,
                personagemDTO.filmesId
                )); 
            

        }).WithTags("Personagens");
    }
}