using Microsoft.AspNetCore.Mvc;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.ModelViews;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Controllers;

public static class PersonagensEndpoint
{
    public static void PersonagensEndpoints(this WebApplication app)
    {
        var personagemGroup = app.MapGroup("personagens");

        personagemGroup.MapPost("/create", async ([FromBody] PersonagemDTO personagemDTO, IPersonagem personagemServicos, DbContexto ctx) =>
        {
            var ErroValidacao = new ErroValidacao
            {
                Erros = new List<string>()
            };
            
            
            
        });
    }
}