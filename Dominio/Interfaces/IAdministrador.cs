using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;

namespace SWAPI_Minimal.Dominio.Interfaces;

public interface IAdministrador
{
    Task<Administradores?> LoginAsync(LoginDTO loginDTO);
    Task<Administradores> CreateAsync(Administradores admin);
    void Delete(Administradores admin);
    Task<Administradores?> GetIdAsync(Guid id);
    Task<List<Administradores>> ListarAsync(int? pagina = null);
}
