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
        
        modelBuilder.Entity<Personagem>()
            .HasOne(p => p.Planeta)
            .WithMany(pl => pl.Personagens)
            .HasForeignKey(p => p.PlanetaID);
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Personagem>()
            .HasMany(p => p.Filmes)
            .WithMany(p => p.Personagens)
            .UsingEntity(juncao => juncao.ToTable("PersonagemFilme"));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Swapi.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}