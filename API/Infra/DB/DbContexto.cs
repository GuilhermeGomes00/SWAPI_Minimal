using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.ENUMs;

namespace SWAPI_Minimal.Infra.DB;

public class DbContexto : DbContext
{
    public DbSet<Administradores> Administradores { get; set; }
    public DbSet<Personagem> Personagens { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Planetas>  Planetas { get; set; }

    public DbContexto(DbContextOptions<DbContexto> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Anotações para estudos: 
        // Garantindo a criação de duas contas para testes de token.
        modelBuilder.Entity<Administradores>().HasData(
            new Administradores(
                "admteste@teste.com",
                "123456",
                nameof(Perfil.Adm)
            )
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            }
        );
        modelBuilder.Entity<Administradores>().HasData(
            new Administradores(
                "viewerteste@teste.com",
                "123456",
                nameof(Perfil.Viewer)
            )
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
            }
        );
        
        // Garatindo a criação de planetas.
        modelBuilder.Entity<Planetas>().HasData(
            new Planetas
            {
                Id = 1,
                Nome = "Tatooine",
                Terreno = "Desértico",
                Clima = "Árido"
            },
            new Planetas
            {
                Id = 2,
                Nome = "Stewjon",
                Terreno = "Verdejante",
                Clima = "Temperado"
            },
            new Planetas
            {
                Id = 3,
                Nome = "Alderaan",
                Terreno = "Pastagem, cordilheira",
                Clima = "Temperado"
            }
        );
        
        // Garatindo a criação de Filmes
        modelBuilder.Entity<Filme>().HasData(
            new Filme
            {
                Id = 1,
                Titulo = "A Ameaça Fantasma",
                Tipo = nameof(TipoMidiaFilmeSW.Filme),
                Ano = new DateTime(1999, 5, 16)
            },
            new Filme
            {
                Id = 2,
                Titulo = "O Ataque dos Clones",
                Tipo = nameof(TipoMidiaFilmeSW.Filme),
                Ano = new DateTime(2002, 5, 16)
            },
            new Filme
            {
                Id = 3,
                Titulo = "A Vingança dos Sith",
                Tipo = nameof(TipoMidiaFilmeSW.Filme),
                Ano = new DateTime(2005, 5, 19)
            },
            new Filme
            {
                Id = 4,
                Titulo = "Uma Nova Esperança",
                Tipo = nameof(TipoMidiaFilmeSW.Filme),
                Ano = new DateTime(1977, 5, 25)
            },
            new Filme
            {
                Id = 5,
                Titulo = "O Império Contra-Ataca",
                Tipo = nameof(TipoMidiaFilmeSW.Filme),
                Ano = new DateTime(1980, 5, 21)
            },
            new Filme
            {
                Id = 6,
                Titulo = "O Retorno de Jedi",
                Tipo = nameof(TipoMidiaFilmeSW.Filme),
                Ano = new DateTime(1983, 5, 25)
            }
        );
        
        // Garatindo a criação de Personagens
        modelBuilder.Entity<Personagem>().HasData(
            new Personagem
            {
                Id = 1,
                Nome = "Obi-Wan Kenobi",
                Altura = 1.82,
                CorCabelo = "Castanho",
                CorPele = "Clara",
                CorOlhos = "Azul",
                DataNascimento = "57ABY",
                Genero = "Masculino",
                PlanetaID = 2 
            },
            new Personagem
            {
                Id = 2,
                Nome = "Princesa Leia Organa",
                Altura = 1.50,
                CorCabelo = "Castanho",
                CorPele = "Clara",
                CorOlhos = "Castanho",
                DataNascimento = "19ABY",
                Genero = "Feminino",
                PlanetaID = 3 
            },
            new Personagem
            {
                Id = 3,
                Nome = "Anakin Skywalker",
                Altura = 1.88,
                CorCabelo = "Castanho",
                CorPele = "Clara",
                CorOlhos = "Azul",
                DataNascimento = "41.9ABY",
                Genero = "Masculino",
                PlanetaID = 1 
            }
        );


            
        modelBuilder.Entity<Personagem>()
            .HasOne(p => p.Planeta)
            .WithMany(pl => pl.Personagens)
            .HasForeignKey(p => p.PlanetaID);
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Personagem>()
            .HasMany(p => p.Filmes)
            .WithMany(p => p.Personagens)
            .UsingEntity(juncao =>
            {
                juncao.ToTable("PersonagemFilme");
                
                juncao.HasData(
                    new { FilmesId = 1, PersonagensId = 1 },
                    new { FilmesId = 2, PersonagensId = 1 },
                    new { FilmesId = 3, PersonagensId = 1 },
                    new { FilmesId = 4, PersonagensId = 1 },
                    new { FilmesId = 5, PersonagensId = 1 },
                    new { FilmesId = 6, PersonagensId = 1 },

                    new { FilmesId = 4, PersonagensId = 2 },
                    new { FilmesId = 5, PersonagensId = 2 },
                    new { FilmesId = 6, PersonagensId = 2 },

                    new { FilmesId = 1, PersonagensId = 3 },
                    new { FilmesId = 2, PersonagensId = 3 },
                    new { FilmesId = 3, PersonagensId = 3 },
                    new { FilmesId = 6, PersonagensId = 3 }
                );
            });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Swapi.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}