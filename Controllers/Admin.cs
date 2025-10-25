using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.ENUMs;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.ModelViews;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Controllers;

public static class Admin
{
    public static void AdminEndPoints(this WebApplication app)
    {
        var adminGroup = app.MapGroup("Admin");


        adminGroup.MapPost("Create", async ([FromBody] AdminDTOs adminDTO, IAdministrador adminServicos, DbContexto ctx) =>
        {
            var validacao = new ErroValidacao
            {
                Erros = new List<string>()
            };
            
            var contaExistente = await ctx.Administradores.AnyAsync(a => adminDTO.Email == a.Email);
            if (contaExistente) 
                validacao.Erros.Add($"Email {adminDTO.Email} Já existente");
            
            
            if (string.IsNullOrEmpty(adminDTO.Email))
                validacao.Erros.Add("Email nao pode ser vazio");
            if (string.IsNullOrEmpty(adminDTO.Senha))
                validacao.Erros.Add("Senha nao pode ser vazia");
            if (adminDTO.Perfil == null)
                validacao.Erros.Add("Perfil nao pode ser vazio");

            if (validacao.Erros.Count > 0)
                return Results.BadRequest(validacao.Erros);

            var adm = new Administradores(
                adminDTO.Email,
                adminDTO.Senha,
                adminDTO.Perfil.ToString() ?? Perfil.Viewer.ToString()
            );

            await adminServicos.CreateAsync(adm);
            
            return Results.Created($"/admin/{adm.Id}", new AdminMV
            (
                adm.Id,
                adm.Email,
                adm.Perfil
            ));
        }).WithTags("Admin");


        adminGroup.MapGet("/{id}", async ([FromRoute] Guid id, IAdministrador adminServicos) =>
        {
            var admin = await adminServicos.GetIdAsync(id);
            if (admin == null) return Results.NotFound();

            return Results.Ok(new AdminMV(
                admin.Id,
                admin.Email,
                admin.Perfil
            ));

        }).WithTags("Admin");

        adminGroup.MapDelete("/delete{id}", async ([FromRoute] Guid id, IAdministrador adminServicos, DbContexto ctx) =>
        {
            var adminToDelete = await adminServicos.GetIdAsync(id);
            if (adminToDelete == null) return Results.NotFound();
            
            await adminServicos.DeleteAsync(adminToDelete);
            return Results.NoContent();
        }).WithTags("Admin");

        adminGroup.MapGet("/Users", async ([FromQuery] int? pagina, IAdministrador adminServicos) =>
        {
            var adms = new List<AdminMV>();
            var administradores = await adminServicos.ListarAsync(pagina);

            foreach (var admin in administradores)
            {
                adms.Add(new AdminMV(admin.Id, admin.Email, admin.Perfil));
            }
            return Results.Ok(adms);
        }).WithTags("Admin");
    }
}