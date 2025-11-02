using Microsoft.EntityFrameworkCore;
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
        var personagem = await _ctx.Personagens.Where(p => p.Id == id).Include(p => p.Filmes).FirstOrDefaultAsync();
        return  personagem;
    }

    public async Task<Personagem?> ObterPorNome(string nome)
    {
        var personagem = await _ctx.Personagens.Where(p => p.Nome == nome).Include(p => p.Filmes).FirstOrDefaultAsync();
        return  personagem;
    }

    // CORRETO
    public async Task<List<Personagem>> ObterListaAsync(int? pagina = null)
    {
        var query = _ctx.Personagens
            .Include(p => p.Filmes)   
            .Include(p => p.Planeta)  
            .AsQueryable();
    
        int entidadesPorPagina = 10;
        if (pagina != null)
        {
            query = query.OrderBy(p => p.Id); 
            query = query.Skip(((int)pagina - 1) * entidadesPorPagina).Take(entidadesPorPagina);
        }
        return await query.ToListAsync();
    }

    public async Task<Personagem?> CriarAsync(Personagem personagem)
    {
        await _ctx.Personagens.AddAsync(personagem);
        await _ctx.SaveChangesAsync();
        return personagem;
    }

    public async Task AttAsync(Personagem personagem)
    {
        _ctx.Personagens.Update(personagem);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(Personagem personagem)
    {
        _ctx.Personagens.Remove(personagem);
        await _ctx.SaveChangesAsync();
    }
}