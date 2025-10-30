using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Dominio.Servicos;

public class FilmesServicos : IFilmes
{
    private readonly DbContexto _ctx;

    public FilmesServicos(DbContexto ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<Filme?> GetIdAsync(int id)
    {
        var Filme = await _ctx.Filmes.Where(f => f.Id == id).FirstOrDefaultAsync();
        return Filme;
    }

    public async Task<List<Filme>> ListarAsync(int? pagina = null)
    {
        var query =  _ctx.Filmes.AsQueryable();
        
        int entidadesPorPagina = 10;
        if (pagina != null)
            query = query.Skip(((int)pagina - 1) * entidadesPorPagina).Take(entidadesPorPagina);
        
        return await query.ToListAsync();
    }

    public async Task<Filme> CriarAsync(Filme filme)
    {
        await _ctx.Filmes.AddAsync(filme);
        await _ctx.SaveChangesAsync();
        return filme;
    }

    public async Task DeletarAsync(Filme filme)
    {
        _ctx.Filmes.Remove(filme);
        await _ctx.SaveChangesAsync();
    }
}