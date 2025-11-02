using SWAPI_Minimal.Dominio.Entidades;

namespace SWAPI_Minimal.Dominio.Interfaces;

public interface IPersonagem
{
    Task<Personagem?> ObterPorIdAsync(int id);
    Task<Personagem?> ObterPorNome(string nome);
    Task<List<Personagem>> ObterListaAsync(int? pagina = null);
    Task<Personagem?> CriarAsync(Personagem personagem);
    Task AttAsync(Personagem personagem);
    Task DeleteAsync(Personagem personagem);
    
}