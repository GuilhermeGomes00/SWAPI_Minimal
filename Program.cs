using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Controllers.Adminitracao;
using SWAPI_Minimal.Infra.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexto>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


app.Login();

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
