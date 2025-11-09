using SWAPI_Minimal.Dominio.ENUMs;

namespace SWAPI_Minimal.Dominio.DTOs;

public record FilmesDTOs(string titulo, TipoMidiaFilmeSW? Tipo, DateOnly anoLancado);