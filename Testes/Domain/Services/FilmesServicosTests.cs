using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Servicos;
using SWAPI_Minimal.Infra.DB;

namespace Testes.Domain.Services;

[TestClass]
public class FilmesServicosTests
{
    private DbContexto _ctx = null!;
    private SqliteConnection _conn = null!;


    [TestInitialize]
    public void Inicializar()
    {
        _conn = new SqliteConnection("DataSource=:memory:");
        _conn.Open();
        
        var options = new DbContextOptionsBuilder<DbContexto>().
            UseSqlite(_conn).
            Options;
        _ctx = new DbContexto(options);
        _ctx.Database.EnsureCreated();
    }

    [TestMethod]
    public async Task CriarAsync()
    {
        var servico = new FilmesServicos(_ctx);
        var novoFilme = new Filme
        {
            Titulo = "Star Wars Rogue One",
            Ano = new DateTime(2016, 12, 16),
            Tipo = "Filme",
        };

        await servico.CriarAsync(novoFilme);
        
        int contagem = await _ctx.Filmes.CountAsync();
        Assert.AreEqual(7, contagem);
        
        var filme = await _ctx.Filmes.SingleOrDefaultAsync(f => f.Titulo == "Star Wars Rogue One");
        Assert.IsNotNull(filme);
        Assert.AreEqual("Star Wars Rogue One", filme.Titulo);
        Assert.AreEqual("Filme", filme.Tipo);
    }

    [TestMethod]
    public async Task CriarNullAsync()
    {
        var servico = new FilmesServicos(_ctx);
        var filmeNull = new Filme(
            "Star Wars Rogue One",
            null,
            new DateTime(2016, 12, 16)
        );
        
        await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => await servico.CriarAsync(filmeNull));
        
        int contagem =  await _ctx.Filmes.CountAsync();
        Assert.AreEqual(6, contagem);
    }

    [TestMethod]
    public async Task GetByIdAsync()
    {
        var servico = new FilmesServicos(_ctx);
        var filme = await servico.GetIdAsync(1);
        
        Assert.IsNotNull(filme);
        Assert.AreEqual("A Ameaça Fantasma", filme.Titulo);
    }

    [TestMethod]
    public async Task DeleteAsync()
    {
        var servico = new FilmesServicos(_ctx);
        var filme = await _ctx.Filmes.FindAsync(6);

        await servico.DeletarAsync(filme);
        
        int contagem = await _ctx.Filmes.CountAsync();
        Assert.AreEqual(5, contagem);
        var filmeDeletado = await _ctx.Filmes.FindAsync(6);
        Assert.IsNull(filmeDeletado);
    }
    
    [TestCleanup]
    public void TestCleanup()
    {
        _ctx.Dispose();
        _conn.Dispose();
    }
}