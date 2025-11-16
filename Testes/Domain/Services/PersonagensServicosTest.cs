using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Servicos;
using SWAPI_Minimal.Infra.DB;

namespace Testes.Domain.Services;

[TestClass]
public class PersonagensServicosTest
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
    public async Task GetByIdPersonagens()
    {
        var servico = new PersonagensServico(_ctx);
        var personagem = await servico.ObterPorIdAsync(1);
        Assert.IsNotNull(personagem);
        Assert.AreEqual("Obi-Wan Kenobi", personagem.Nome);
    }

    [TestMethod]
    public async Task GetListPersonagens()
    {
        var servico = new PersonagensServico(_ctx);
        var Personagens = await servico.ObterListaAsync(1);
        var personagem1 = Personagens[0];
        var filmes1 = personagem1.Filmes;
        
        Assert.IsNotNull(Personagens);
        Assert.IsNotNull(personagem1.Filmes);
        Assert.IsNotNull(personagem1.Planeta);
        Assert.AreEqual(3, Personagens.Count);
        Assert.AreEqual("Obi-Wan Kenobi", personagem1.Nome);
        Assert.AreEqual("Stewjon", personagem1.Planeta.Nome);
        Assert.AreEqual(6, personagem1.Filmes.Count);
    }

    [TestMethod]
    public async Task CriarPersonagens()
    {
        var servico = new PersonagensServico(_ctx);
        var planeta = await _ctx.Planetas.FindAsync(2);
        List<int> IDs = new List<int> { 4, 5, 6 };
        var ListaFilme = await _ctx.Filmes
            .Where(f=> IDs.Contains(f.Id))
            .ToListAsync();

        var personagem = new Personagem
        {
            Nome = "Han Solo",
            Altura = 1.80,
            CorCabelo = "Castanho",
            CorPele = "Branco",
            CorOlhos = "Castanho",
            DataNascimento = "29BBY",
            Genero = "Masculino",
            PlanetaID = planeta.Id,
            Filmes = ListaFilme,
        };
        
        await servico.CriarAsync(personagem);
        
        int contagem = await _ctx.Personagens.CountAsync();
        Assert.AreEqual(4, contagem);
        Assert.AreEqual(2, personagem.PlanetaID);
        Assert.AreEqual(3, personagem.Filmes.Count);
    }

    [TestMethod]
    public async Task AttAsync()
    {
        var servico = new PersonagensServico(_ctx);
        var personagem = await _ctx.Personagens.FindAsync(1);
        var planeta = await _ctx.Planetas.FindAsync(3);
        personagem.Nome = "Ben Kenobi";
        personagem.Planeta = planeta;
        
        await servico.AttAsync(personagem);
        
        var personagemAtt = await _ctx.Personagens.FindAsync(1);
        Assert.AreEqual("Ben Kenobi", personagem.Nome);
        Assert.AreEqual("Alderaan", personagem.Planeta.Nome);
        Assert.AreEqual(3, personagem.PlanetaID);
    }

    [TestMethod]
    public async Task DeletarPersonagens()
    {
        var servico = new PersonagensServico(_ctx);
        var personagem = await _ctx.Personagens.FindAsync(3);
        Assert.IsNotNull(personagem, "Personagem não encontrado");
        
        await  servico.DeleteAsync(personagem);
        
        int contagem = await _ctx.Personagens.CountAsync();
        Assert.AreEqual(2, contagem);
        
        var personagemDeletado =  await _ctx.Personagens.FindAsync(3);
        Assert.IsNull(personagemDeletado);

        int idDeletado = 3;
        var contagemJoin = await _ctx.Database.SqlQuery<int>($"SELECT COUNT(*) AS Value FROM PersonagemFilme WHERE PersonagensId = {idDeletado}")
            .SingleAsync();
        Assert.AreEqual(0, contagemJoin);
        
    }
    
    [TestCleanup]
    public void TestCleanup()
    {
        _ctx.Dispose();
        _conn.Dispose();
    }
}