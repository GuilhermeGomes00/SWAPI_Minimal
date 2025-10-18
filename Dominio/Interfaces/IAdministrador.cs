using SWAPI_Minimal.Dominio.DTOs;
using SWAPI_Minimal.Dominio.Entidades;

namespace SWAPI_Minimal.Dominio.Interfaces;

public interface IAdministrador
{
    Administradores? Login(LoginDTO loginDTO);
    Administradores Create(Administradores admin);
    Administradores Delete(Administradores admin, LoginDTO loginDTO);
    Administradores? GetId(int id);
    List<Administradores> Listar(int? pagina);
}