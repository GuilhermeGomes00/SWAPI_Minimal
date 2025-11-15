using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Infra.DB;

namespace Testes.Domain.Services;

[TestClass]
public class AdminServicosTests
{
    private DbContexto _ctx = null!;
    private SqliteConnection _conn = null!;

    [TestInitialize]
    public void Inicializar()
    {
        _conn = new SqliteConnection("DataSource=:memory:");
        _conn.Open();
        
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseSqlite(_conn)
            .Options;
        
        _ctx = new DbContexto(options);
        _ctx.Database.EnsureCreated();
    }    
    [TestMethod]
    public async Task TentandoSalvarAdmin()
    {
        var adm = new Administradores(
            "admteste3@teste.com",
            "123456",
            "adm"
        );
        
        await _ctx.Administradores.AddAsync(adm);
        await  _ctx.SaveChangesAsync();
        
        var contagem = await _ctx.Administradores.CountAsync();
        Assert.AreEqual(3, contagem);
        
        var admin = await _ctx.Administradores.FirstOrDefaultAsync(a => a.Email == "admteste3@teste.com");
        Assert.IsNotNull(admin);
        Assert.AreEqual("adm", admin.Perfil);

    }

    [TestCleanup]
    public void TestCleanup()
    {
        _ctx.Dispose();
        _conn.Dispose();
    }
}