using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.Servicos;

namespace SWAPI_Minimal.Controllers;
public static class LoginEndPoints
{
    
    public static void Login(this WebApplication app)
    {
        
        var loginGroup = app.MapGroup("Login");

        loginGroup.MapPost("", ([FromBody] LoginDTO loginDTO, IAdministrador adminServicos) =>
        {
            
            var adm = adminServicos.Login(loginDTO);
            if (adminServicos.Login(loginDTO) != null)
            {
                return Results.Ok("login feito com sucesso");
            }else 
                return Results.Unauthorized();
        });
    }
}