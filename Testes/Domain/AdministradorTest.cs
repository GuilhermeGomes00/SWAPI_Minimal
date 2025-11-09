using SWAPI_Minimal.Dominio.Entidades;

namespace Testes.Domain;

[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestarGetProp()
    {
        var adm = new Administradores();

        Guid idAdmin = Guid.Parse("11111111-1111-1111-1111-111111111111"); 
        
        adm.Email = "admteste@teste.com";
        adm.Senha = "123456";
        adm.Senha = "adm";
        
        Assert.AreEqual(idAdmin, adm.Id);
        Assert.AreEqual("admteste@teste.com", adm.Email);
        Assert.AreEqual("123456", adm.Senha);
        Assert.AreEqual("adm", adm.Senha);

    }
}