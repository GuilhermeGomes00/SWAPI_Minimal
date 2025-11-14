using SWAPI_Minimal.Dominio.Entidades;

namespace Testes.Domain;

[TestClass]
public class PersonagemTest
{
    [TestMethod]
    [DataRow(1, "Obi-Wan Kenobi", 1.82, "Castanho", "Clara", "Azul", "57ABY", "Masculino", 2)]
    [DataRow(2, "Princesa Leia Organa", 1.50, "Castanho", "Clara", "Castanho", "19ABY", "Feminino", 3)]
    [DataRow(3, "Anakin Skywalker", 1.88, "Castanho", "Clara", "Azul", "41.9ABY", "Masculino", 1)]
    public void PersonagensTest(int id, string nome, double altura, string corCabelo, string corPele, string corOlhos, string dataNascimento, string genero, int planetaId)
    {
        var personagem = new Personagem
        {
            Id = id,
            Nome = nome,
            Altura = altura,
            CorCabelo = corCabelo,
            CorPele =  corPele,
            CorOlhos = corOlhos,
            DataNascimento = dataNascimento,
            Genero = genero,
            PlanetaID = planetaId
        };
        
        Assert.AreEqual(nome, personagem.Nome);
        Assert.AreEqual(altura, personagem.Altura);
        Assert.AreEqual(corCabelo, personagem.CorCabelo);
        Assert.AreEqual(corOlhos, personagem.CorOlhos);
        Assert.AreEqual(dataNascimento, personagem.DataNascimento);
        Assert.AreEqual(genero, personagem.Genero);
        Assert.AreEqual(planetaId, personagem.PlanetaID);
    }
}