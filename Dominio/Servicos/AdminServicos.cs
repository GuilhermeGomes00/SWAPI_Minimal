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
    
    public Administradores? Login(LoginDTO loginDTO)
    {
        var adm = _ctx.
            Administradores.
            FirstOrDefault(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        
        return adm;
    }

    public Administradores Create(Administradores admin)
    {
        throw new NotImplementedException();
    }

    public Administradores Delete(Administradores admin, LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public Administradores? GetId(int id)
    {
        throw new NotImplementedException();
    }

    public List<Administradores> Listar(int? pagina)
    {
        throw new NotImplementedException();
    }
}