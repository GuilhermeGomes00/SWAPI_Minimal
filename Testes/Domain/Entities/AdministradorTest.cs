using SWAPI_Minimal.Dominio.Entidades;

namespace Testes.Domain;

[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestarGetProp()
    {
        var adm = new Administradores();
        
        adm.Email = "admteste@teste.com";
        adm.Senha = "123456";
        adm.Perfil = "adm";
        
        Assert.AreEqual("admteste@teste.com", adm.Email);
        Assert.AreEqual("123456", adm.Senha);
        Assert.AreEqual("adm", adm.Perfil);

    }
}