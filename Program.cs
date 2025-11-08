using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SWAPI_Minimal.Controllers;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Dominio.ModelViews;
using SWAPI_Minimal.Dominio.Servicos;
using SWAPI_Minimal.Infra.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministrador, AdminServicos>();
builder.Services.AddScoped<IPlanetas, PlanetasServicos>();
builder.Services.AddScoped<IFilmes, FilmesServicos>();
builder.Services.AddScoped<IPersonagem, PersonagensServico>();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseSqlite("Data Source=Swapi.Sqlite");
});

var key = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(key)) 
    throw new InvalidOperationException("A chave JWT (Jwt:Key) não foi configurada.");

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false, 
        ValidateAudience = false, 
    };
});

builder.Services.AddAuthorization(); //
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira seu token JWT: "
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Json(new Home()));

app.Login(key);
app.AdminEndPoints();
app.PlanetasEndpoints();
app.FilmesEndpoints();
app.PersonagensEndpoints();

app.Run();