using Microsoft.EntityFrameworkCore;
using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;
using SWAPI_Minimal.Dominio.Interfaces;
using SWAPI_Minimal.Infra.DB;

namespace SWAPI_Minimal.Dominio.Servicos;

public class AdminServicos : IAdministrador
{
    
    private readonly DbContexto _ctx;

    public AdminServicos(DbContexto ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<Administradores?> LoginAsync(LoginDTO loginDTO)
    {
        var adm = await _ctx.
            Administradores.
            FirstOrDefaultAsync(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        
        return adm;
    }

    public async Task<Administradores> CreateAsync(Administradores admin)
    {
        await _ctx.Administradores.AddAsync(admin);
        await _ctx.SaveChangesAsync();
        return admin;
    }

    public async Task DeleteAsync(Administradores admin)
    {
        _ctx.Administradores.Remove(admin);
        await  _ctx.SaveChangesAsync();
    }

    public async Task<Administradores?> GetIdAsync(Guid id)
    {
        return await _ctx.Administradores.Where(a => a.Id == id).FirstOrDefaultAsync();
        
    }

    public async Task<List<Administradores>> ListarAsync(int? pagina = null)
    {
        var query =  _ctx.Administradores.AsQueryable();
        
        int entidadesPorPagina = 10;
        if (pagina != null)
            query = query.Skip(((int)pagina - 1) * entidadesPorPagina).Take(entidadesPorPagina);
        
        return await query.ToListAsync();
    }
}