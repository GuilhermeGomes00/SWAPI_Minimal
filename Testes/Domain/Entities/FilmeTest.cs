using SWAPI_Minimal.Dominio.Entidades;

namespace Testes.Domain;

[TestClass]
public class FilmeTest
{
    [DataTestMethod]
    [DataRow(1, "A Ameaça Fantasma", "Filme", 1999, 5, 16)]
    [DataRow(2, "O Ataque dos Clones", "Filme", 2002, 5, 16)]
    [DataRow(3, "A Vingança dos Sith", "Filme", 2005, 5, 19)]
    [DataRow(4, "Uma Nova Esperança", "Filme", 1977, 5, 25)]
    [DataRow(5, "O Império Contra-Ataca", "Filme", 1980, 5, 21)]
    [DataRow(6, "O Retorno de Jedi", "Filme", 1983, 5, 25)]
    public void TestarFilmes(int id, string titulo, string tipo, int ano, int mes, int dia)
    {
        var filme = new Filme
        {
            Id = id,
            Titulo = titulo,
            Tipo = tipo,
            Ano = new DateTime(ano, mes, dia)
        };

        Assert.AreEqual(id, filme.Id);
        Assert.AreEqual(titulo, filme.Titulo);
        Assert.AreEqual(tipo, filme.Tipo);
        Assert.AreEqual(new DateTime(ano, mes, dia), filme.Ano);
    }
}