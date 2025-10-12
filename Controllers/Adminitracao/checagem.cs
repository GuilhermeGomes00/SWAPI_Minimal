using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Controllers.Adminitracao;

public static class checagem
{
    public static void Login(this WebApplication app)
    {
        var loginEndPoint = app.MapGroup("Login");
        
        app.MapGet("/", () =>
        {
            var xD = "Tenta o /Login ai pls";
            return Results.Ok(xD);
        });

        loginEndPoint.MapPost("", async (LoginDTO loginDto, DbContexto CTX) =>
        {
            // if (loginDto.Email == "EmailTeste@email.com" && loginDto.Senha == "123456") return Results.Ok("login sucesso");
            // else return Results.NotFound();
        });

        

    } 
}