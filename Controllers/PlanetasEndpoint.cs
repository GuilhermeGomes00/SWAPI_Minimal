using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.ModelViews;
using SWAPI_Minimal.Dominio.Servicos;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Controllers;

public static class PlanetasEndpoint
{
    public static void PlanetasEndpoints(this WebApplication app)
    {
        var planetasGroup = app.MapGroup("Planetas");

        planetasGroup.MapGet("/", async ([FromQuery] int? pagina, [FromServices]IPlanetas planetasServicos) =>
        {
            var listaPlanetas = new List<PlanetasMV>();
            var planetas = await planetasServicos.ListarAsync(pagina);

            foreach (var p in planetas)
            {
                listaPlanetas.Add(new PlanetasMV(p.Id, p.Nome));
            }
            return Results.Ok(listaPlanetas);
            
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Viewer" }).WithTags("Planeta");

        planetasGroup.MapGet("/{id}", async ([FromRoute] int id, IPlanetas planetasServicos) =>
        {
            var planeta = await planetasServicos.GetIdAsync(id);
            if  (planeta == null) return Results.NotFound();
            
            return Results.Ok(new PlanetasMV(planeta.Id, planeta.Nome));
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Viewer" }).WithTags("Planeta");

        planetasGroup.MapPost("/Create", async ([FromBody] PlanetasDTOs planetasDTOs, IPlanetas planetasServicos, DbContexto ctx) =>
        {
            var validacao = new ErroValidacao
            {
                Erros = new List<string>()
            };

            var planetaChecagem = await ctx.Planetas.AnyAsync(p => planetasDTOs.Id == p.Id || planetasDTOs.Nome == p.Nome);
            if (planetaChecagem) validacao.Erros.Add($"O planeta {planetasDTOs.Nome} já está salvo.");
            
            if(string.IsNullOrEmpty(planetasDTOs.Nome)) 
                validacao.Erros.Add("Nome do planeta não pode ser vazio");
            
            if(validacao.Erros.Count > 0)
                return Results.BadRequest(validacao.Erros);

            var planeta = new Planetas { Nome = planetasDTOs.Nome };

            await planetasServicos.CriarAsync(planeta);
            
            return Results.Created($"/planetas/{planeta.Id}", new PlanetasDTOs(
                planeta.Id, planeta.Nome)); 
            
            
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Planeta");

        planetasGroup.MapDelete("/Delete{id}", async ([FromRoute] int id, IPlanetas planetasServicos) =>
        {
            var planeta =  await planetasServicos.GetIdAsync(id);
            if (planeta == null) return Results.NotFound();
            
            await planetasServicos.DeletarAsync(planeta);
            return Results.NoContent();
            
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Planeta");

    }
}