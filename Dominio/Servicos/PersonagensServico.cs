using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Dominio.Servicos;

public class PersonagensServico : IPersonagem
{
    private readonly DbContexto _ctx;

    public PersonagensServico(DbContexto ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<Personagem?> ObterPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Personagem?> ObterPorNome(string nome)
    {
        throw new NotImplementedException();
    }

    public async Task<Personagem?> ObterListaAsync(int? pagina = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Personagem?> CriarAsync(Personagem personagem)
    {
        await _ctx.Personagens.AddAsync(personagem);
        await _ctx.SaveChangesAsync();
        return personagem;
    }

    public async Task<Personagem?> AttAsync(int id, Personagem personagem)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}