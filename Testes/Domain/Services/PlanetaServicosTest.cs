using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Servicos;
using SWAPI_Minimal.Infra.DB;

namespace Testes.Domain.Services;

[TestClass]
public class PlanetaServicosTest
{
    private DbContexto _ctx = null!;
    private SqliteConnection _conn = null!;

    [TestInitialize]
    public void Inicializar()
    {
        _conn = new SqliteConnection("DataSource=:memory:");
        _conn.Open();
        
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseSqlite(_conn).
            Options;
        
        _ctx = new DbContexto(options);
        _ctx.Database.EnsureCreated();
    }

    [TestMethod]
    public async Task CriarAsync()
    {
        var servico = new PlanetasServicos(_ctx);
        var novoPlaneta = new Planetas
        {
            Nome = "Hoth",
            Terreno = "Tundra, cavernas de gelo",
            Clima = "Congelado"
        };
        
        await servico.CriarAsync(novoPlaneta);
        
        int contagemPlanetas = await _ctx.Planetas.CountAsync();
        Assert.AreEqual(4, contagemPlanetas);
        
        var planetaCriado = await _ctx.Planetas.FirstOrDefaultAsync(p => p.Nome == "Hoth");
        Assert.IsNotNull(planetaCriado);
        Assert.AreEqual("Congelado", planetaCriado.Clima);
    }
    
    [TestMethod]
    public async Task CriarAsyncNull()
    {
        var servico = new PlanetasServicos(_ctx);
        var novoPlaneta = new Planetas
        {
            Nome = "Hoth",
            Terreno = "Tundra, cavernas de gelo",
            Clima = null
        };
        
        await Assert.ThrowsExceptionAsync<DbUpdateException>(async () =>
            await servico.CriarAsync(novoPlaneta));
        
        int contagem = await _ctx.Planetas.CountAsync();
        Assert.AreEqual(3, contagem);
    }
    
    [TestMethod]
    public async Task GetIdAsync()
    {
        var servico = new PlanetasServicos(_ctx);
        
        var planetaEscolhido = await servico.GetIdAsync(1);
        Assert.IsNotNull(planetaEscolhido);
        Assert.AreEqual("Tatooine", planetaEscolhido.Nome);
    }
    
    [TestMethod]
    public async Task DeleteAsync()
    {
        var servico = new PlanetasServicos(_ctx);
        var planetaEscolhido = await _ctx.Planetas.FindAsync(1);
        
        await servico.DeletarAsync(planetaEscolhido!);
        
        int contagem = await  _ctx.Planetas.CountAsync();
        Assert.AreEqual(2, contagem);
        var deletado = await _ctx.Planetas.FindAsync(1);
        Assert.IsNull(deletado);
    }
    
    [TestCleanup]
    public void TestCleanup()
    {
        _ctx.Dispose();
        _conn.Dispose();
    }
}