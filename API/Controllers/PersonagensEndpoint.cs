using Microsoft.AspNetCore.Authorization;
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
    private static ErroValidacao ValidaDTOPersonagem(PersonagemDTO personagemDTO)
    {
        var erroValidacao = new  ErroValidacao {Erros = new List<string>()};
            
        if  (string.IsNullOrEmpty(personagemDTO.Nome))
            erroValidacao.Erros.Add("O nome não pode ser nulo.");
        if (personagemDTO.planetaId <= 0)
            erroValidacao.Erros.Add("O planeta inserido não pode ser menor que 0 (zero)");
        if (personagemDTO.Altura == 0 || double.IsNegative(personagemDTO.Altura))
            erroValidacao.Erros.Add("A altura não pode ser menor ou igual que 0 (zero)");
            
        return erroValidacao;
    }
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
            if (double.IsNegative(personagemDTO.Altura) || personagemDTO.Altura == 0)
                erroValidacao.Erros.Add("A altura não pode ser 0 (zero) ou menor que 0 (zero)");
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
            
            return Results.Created($"/personagens/{personagem.Id}", new PersonagemNoIdMV(
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
            

        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Personagem");

        personagemGroup.MapGet("/{id}", async (
            [FromRoute] int id, 
            [FromServices]IPersonagem personagemServicos, 
            [FromServices]DbContexto ctx) =>
        {
            var personagem = await personagemServicos.ObterPorIdAsync(id);
            if (personagem == null)
                            return Results.NotFound();
            
            // lembrar de usar o select pra pegar os IDs
            var filmes = personagem.Filmes.Select(filme => filme.Id).ToList(); 
            
            
            
            return Results.Ok(new PersonagemIdMV(
                personagem.Id,
                personagem.Nome,
                personagem.Altura,
                personagem.CorCabelo,
                personagem.CorPele,
                personagem.CorOlhos,
                personagem.DataNascimento,
                personagem.Genero,
                personagem.PlanetaID,
                filmes
                ));
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Viewer" }).WithTags("Personagem");

        personagemGroup.MapGet("/Nome{nome}", async(
            [FromRoute] string nome,
            [FromServices] IPersonagem personagemServicos,
            [FromServices] DbContexto ctx) =>
            {
                var personagem = await personagemServicos.ObterPorNome(nome);
                if (personagem == null)
                    return Results.NotFound();
                
                var filmes = personagem.Filmes.Select(filme => filme.Id).ToList(); 
            
                return Results.Ok(new PersonagemIdMV(
                    personagem.Id,
                    personagem.Nome,
                    personagem.Altura,
                    personagem.CorCabelo,
                    personagem.CorPele,
                    personagem.CorOlhos,
                    personagem.DataNascimento,
                    personagem.Genero,
                    personagem.PlanetaID,
                    filmes
                ));
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Viewer" }).WithTags("Personagem");
        
        personagemGroup.MapDelete("/delete/{id}", async (
            [FromRoute] int id, 
            [FromServices] IPersonagem personagemServicos) =>
            {
                var personagem =  await personagemServicos.ObterPorIdAsync(id);
                if (personagem == null)
                    return Results.NotFound();
                
                await personagemServicos.DeleteAsync(personagem);
                return Results.NoContent();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Personagem");

        personagemGroup.MapPut("/update/{id}", async (
            [FromRoute] int id, 
            [FromBody] PersonagemDTO personagemDTO,
            [FromServices] IPersonagem personagemServicos, 
            [FromServices] DbContexto ctx) =>
            {
                var erroValidacao = ValidaDTOPersonagem(personagemDTO);
                var personagem = await personagemServicos.ObterPorIdAsync(id);
                
                
                var filmes = await ctx.Filmes.
                    Where(f => personagemDTO.filmesId.Contains(f.Id)).
                    ToListAsync();
                
                var checkFilmes = await ctx.Filmes.
                    CountAsync(f => personagemDTO.filmesId.Contains(f.Id));
                
                bool checkPlaneta = await ctx.Planetas
                    .AnyAsync(p => personagemDTO.planetaId == p.Id);

                if (personagem == null)
                    return Results.NotFound("Personagem não encontrado");
                
                if (!checkPlaneta)
                    erroValidacao.Erros.Add("O novo planeta não existe.");
                
                if (checkFilmes != personagemDTO.filmesId.Count)
                    erroValidacao.Erros.Add("Um ou mais IDs de filmes são inválidos.");
                
                if (erroValidacao.Erros.Count > 0)
                    return Results.BadRequest(erroValidacao);
                
                
                
                personagem.Nome = personagemDTO.Nome;
                personagem.Altura = personagemDTO.Altura;
                personagem.CorCabelo = personagemDTO.corCabelo;
                personagem.CorPele = personagemDTO.corPele;
                personagem.CorOlhos = personagemDTO.corOlhos;
                personagem.DataNascimento = personagemDTO.dataNascimento;
                personagem.Genero = personagemDTO.Genero;
                personagem.PlanetaID = personagemDTO.planetaId;
                personagem.Filmes = filmes;
                
                await personagemServicos.AttAsync(personagem);

                var filmesLista = personagem.Filmes.Select(filme => filme.Id).ToList();
                return Results.Ok(new PersonagemNoIdMV(
                    personagem.Nome,
                    personagem.Altura,
                    personagem.CorCabelo,
                    personagem.CorPele,
                    personagem.CorOlhos,
                    personagem.DataNascimento,
                    personagem.Genero,
                    personagem.PlanetaID,
                    filmesLista
                ));

            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Personagem");
    }
}