using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.ENUMs;

namespace SWAPI_Minimal.Infra.DB;

public class DbContexto : DbContext
{
    public DbSet<Administradores> Administrador { get; set; }

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
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Administrador.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}