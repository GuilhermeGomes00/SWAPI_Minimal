using SWAPI_Minimal.Dominio.Entidades;

namespace SWAPI_Minimal.Dominio.Interfaces;

public interface IPlanetas
{
    Task<Planetas?> GetIdAsync(int id);
    Task<List<Planetas>> ListarAsync(int? pagina = null);
    Task<Planetas> CriarAsync (Planetas planetas);
    Task DeletarAsync(Planetas planetas);
}