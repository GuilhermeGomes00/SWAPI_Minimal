using SWAPI_Minimal.Dominio.Entidades;

namespace SWAPI_Minimal.Dominio.Interfaces;

public interface IFilmes
{
    Task<Filme?> GetIdAsync(int id);
    Task<List<Filme>> ListarAsync(int? pagina = null);
    Task<Filme> CriarAsync (Filme filme);
    Task DeletarAsync(Filme filme);
}