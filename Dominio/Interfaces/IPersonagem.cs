using SWAPI_Minimal.Dominio.Entidades;

namespace SWAPI_Minimal.Dominio.Interfaces;

public interface IPersonagem
{
    Task<Personagem?> ObterPorIdAsync(int id);
    Task<Personagem?> ObterPorNome(string nome);
    Task<Personagem?> ObterListaAsync(int? pagina = null);
    Task<Personagem?> CriarAsync(Personagem personagem);
    Task<Personagem?> AttAsync(int id, Personagem personagem);
    Task DeleteAsync(int id);
    
}