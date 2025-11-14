using SWAPI_Minimal.Dominio.Entidades;

namespace Testes.Domain;

[TestClass]
public class PlanetaTest
{
    [TestMethod]
    [DataRow(1, "Tatooine", "Desértico", "Árido")]
    [DataRow(2, "Stewjon", "Verdejante", "Temperado")]
    [DataRow(3, "Alderaan", "Pastagem, cordilheira", "Temperado")]
    public void PlanetaTestes(int id, string nome, string terreno, string clima)
    {
        var planetas = new Planetas
        {
            Id = id,
            Nome = nome,
            Terreno = terreno,
            Clima = clima
        };
        
        Assert.AreEqual(id, planetas.Id);
        Assert.AreEqual(nome, planetas.Nome);
        Assert.AreEqual(terreno, planetas.Terreno);
        Assert.AreEqual(clima, planetas.Clima);
    }
}