namespace SWAPI_Minimal.Dominio.ModelViews;

public record PersonagemNoIdMV(string Nome, double Altura, string corCabelo, string corPele, string corOlhos, string dataNascimento, string Genero, int planetaId, List<int> filmesId);