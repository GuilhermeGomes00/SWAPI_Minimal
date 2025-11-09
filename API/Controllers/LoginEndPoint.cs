using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;

namespace SWAPI_Minimal.Controllers;
public static class LoginEndPoints
{
    
    public static void Login(this WebApplication app, string key)
    {
        var loginGroup = app.MapGroup("Login");
        
        loginGroup.MapPost("", async ([FromBody] LoginDTO loginDTO, [FromServices] IAdministrador adminServicos) =>
        {
            var adm = await adminServicos.LoginAsync(loginDTO);
            
            if (adm != null) 
            {
                var token = GerarTokenJwt(adm, key);
                
                return Results.Ok(new
                {
                    Email = adm.Email, 
                    Perfil = adm.Perfil,
                    Token = token
                });
            }else 
                return Results.Unauthorized();
                
        }).WithTags("Login");
    }
    private static string GerarTokenJwt(Administradores administrador, string key)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim("Email", administrador.Email), 
            new Claim(ClaimTypes.Role, administrador.Perfil) 
        };
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credential
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}