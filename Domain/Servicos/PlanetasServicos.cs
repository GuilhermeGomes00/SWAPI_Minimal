using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Dominio.Servicos;

public class PlanetasServicos : IPlanetas
{
    private readonly DbContexto _ctx;

    public PlanetasServicos(DbContexto ctx)
    {
        _ctx = ctx;
    }


    public async Task<Planetas?> GetIdAsync(int id)
    {
        return await _ctx.Planetas.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Planetas>> ListarAsync(int? pagina = null)
    {
        var query =  _ctx.Planetas.AsQueryable();
        
        int entidadesPorPagina = 10;
        if (pagina != null)
            query = query.Skip(((int)pagina - 1) * entidadesPorPagina).Take(entidadesPorPagina);
        
        return await query.ToListAsync();
    }

    public async Task<Planetas> CriarAsync(Planetas planetas)
    {
        await _ctx.Planetas.AddAsync(planetas);
        await _ctx.SaveChangesAsync();
        return planetas;
    }

    public async Task DeletarAsync(Planetas planetas)
    {
         _ctx.Planetas.Remove(planetas);
         await _ctx.SaveChangesAsync();
    }
}