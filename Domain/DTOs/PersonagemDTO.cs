namespace SWAPI_Minimal.Dominio.DTOs;

public record PersonagemDTO(string Nome, double Altura, string corCabelo, string corPele, string corOlhos, string dataNascimento, string Genero, int planetaId, List<int> filmesId);