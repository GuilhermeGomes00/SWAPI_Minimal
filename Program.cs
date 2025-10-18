using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SWAPI_Minimal.Controllers;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.ModelViews;
using SWAPI_Minimal.Dominio.Servicos;
using SWAPI_Minimal.Infra.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministrador, AdminServicos>();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseSqlite("Data Source=Administrador.Sqlite");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Json(new Home()));

app.Login();

app.Run();